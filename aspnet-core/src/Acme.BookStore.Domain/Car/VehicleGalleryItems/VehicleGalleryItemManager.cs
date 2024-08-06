using ImportSample.VehicleBrands;
using ImportSample.VehicleModels;
using ImportSample.VehicleYearModels;
using ImportSample.VehicleModelStyles;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleGalleryItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleGalleryItems
{
    public abstract class VehicleGalleryItemManagerBase : DomainService
    {
        protected IVehicleGalleryItemRepository _vehicleGalleryItemRepository;
        protected IRepository<VehicleBrand, Guid> _vehicleBrandRepository;
        protected IRepository<VehicleModel, Guid> _vehicleModelRepository;
        protected IRepository<VehicleYearModel, Guid> _vehicleYearModelRepository;
        protected IRepository<VehicleModelStyle, Guid> _vehicleModelStyleRepository;
        protected IRepository<VehicleBodyStyle, Guid> _vehicleBodyStyleRepository;

        public VehicleGalleryItemManagerBase(IVehicleGalleryItemRepository vehicleGalleryItemRepository,
        IRepository<VehicleBrand, Guid> vehicleBrandRepository,
        IRepository<VehicleModel, Guid> vehicleModelRepository,
        IRepository<VehicleYearModel, Guid> vehicleYearModelRepository,
        IRepository<VehicleModelStyle, Guid> vehicleModelStyleRepository,
        IRepository<VehicleBodyStyle, Guid> vehicleBodyStyleRepository)
        {
            _vehicleGalleryItemRepository = vehicleGalleryItemRepository;
            _vehicleBrandRepository = vehicleBrandRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _vehicleYearModelRepository = vehicleYearModelRepository;
            _vehicleModelStyleRepository = vehicleModelStyleRepository;
            _vehicleBodyStyleRepository = vehicleBodyStyleRepository;
        }

        public virtual async Task<VehicleGalleryItem> CreateAsync(
        List<Guid> vehicleBrandIds,
        List<Guid> vehicleModelIds,
        List<Guid> vehicleYearModelIds,
        List<Guid> vehicleModelStyleIds,
        List<Guid> vehicleBodyStyleIds,
        int order, string assetPath, VehicleGalleryItemType type)
        {
            Check.NotNullOrWhiteSpace(assetPath, nameof(assetPath));
            Check.NotNull(type, nameof(type));

            var vehicleGalleryItem = new VehicleGalleryItem(
             GuidGenerator.Create(),
             order, assetPath, type
             );

            await SetVehicleBrandsAsync(vehicleGalleryItem, vehicleBrandIds);
            await SetVehicleModelsAsync(vehicleGalleryItem, vehicleModelIds);
            await SetVehicleYearModelsAsync(vehicleGalleryItem, vehicleYearModelIds);
            await SetVehicleModelStylesAsync(vehicleGalleryItem, vehicleModelStyleIds);
            await SetVehicleBodyStylesAsync(vehicleGalleryItem, vehicleBodyStyleIds);

            return await _vehicleGalleryItemRepository.InsertAsync(vehicleGalleryItem);
        }

        public virtual async Task<VehicleGalleryItem> UpdateAsync(
            Guid id,
            List<Guid> vehicleBrandIds,
        List<Guid> vehicleModelIds,
        List<Guid> vehicleYearModelIds,
        List<Guid> vehicleModelStyleIds,
        List<Guid> vehicleBodyStyleIds,
        int order, string assetPath, VehicleGalleryItemType type, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(assetPath, nameof(assetPath));
            Check.NotNull(type, nameof(type));

            var queryable = await _vehicleGalleryItemRepository.WithDetailsAsync(x => x.VehicleBrands, x => x.VehicleModels, x => x.VehicleYearModels, x => x.VehicleModelStyles, x => x.VehicleBodyStyles);
            var query = queryable.Where(x => x.Id == id);

            var vehicleGalleryItem = await AsyncExecuter.FirstOrDefaultAsync(query);

            vehicleGalleryItem.Order = order;
            vehicleGalleryItem.AssetPath = assetPath;
            vehicleGalleryItem.Type = type;

            await SetVehicleBrandsAsync(vehicleGalleryItem, vehicleBrandIds);
            await SetVehicleModelsAsync(vehicleGalleryItem, vehicleModelIds);
            await SetVehicleYearModelsAsync(vehicleGalleryItem, vehicleYearModelIds);
            await SetVehicleModelStylesAsync(vehicleGalleryItem, vehicleModelStyleIds);
            await SetVehicleBodyStylesAsync(vehicleGalleryItem, vehicleBodyStyleIds);

            vehicleGalleryItem.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleGalleryItemRepository.UpdateAsync(vehicleGalleryItem);
        }

        private async Task SetVehicleBrandsAsync(VehicleGalleryItem vehicleGalleryItem, List<Guid> vehicleBrandIds)
        {
            if (vehicleBrandIds == null || !vehicleBrandIds.Any())
            {
                vehicleGalleryItem.RemoveAllVehicleBrands();
                return;
            }

            var query = (await _vehicleBrandRepository.GetQueryableAsync())
                .Where(x => vehicleBrandIds.Contains(x.Id))
                .Select(x => x.Id);

            var vehicleBrandIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!vehicleBrandIdsInDb.Any())
            {
                return;
            }

            vehicleGalleryItem.RemoveAllVehicleBrandsExceptGivenIds(vehicleBrandIdsInDb);

            foreach (var vehicleBrandId in vehicleBrandIdsInDb)
            {
                vehicleGalleryItem.AddVehicleBrand(vehicleBrandId);
            }
        }

        private async Task SetVehicleModelsAsync(VehicleGalleryItem vehicleGalleryItem, List<Guid> vehicleModelIds)
        {
            if (vehicleModelIds == null || !vehicleModelIds.Any())
            {
                vehicleGalleryItem.RemoveAllVehicleModels();
                return;
            }

            var query = (await _vehicleModelRepository.GetQueryableAsync())
                .Where(x => vehicleModelIds.Contains(x.Id))
                .Select(x => x.Id);

            var vehicleModelIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!vehicleModelIdsInDb.Any())
            {
                return;
            }

            vehicleGalleryItem.RemoveAllVehicleModelsExceptGivenIds(vehicleModelIdsInDb);

            foreach (var vehicleModelId in vehicleModelIdsInDb)
            {
                vehicleGalleryItem.AddVehicleModel(vehicleModelId);
            }
        }

        private async Task SetVehicleYearModelsAsync(VehicleGalleryItem vehicleGalleryItem, List<Guid> vehicleYearModelIds)
        {
            if (vehicleYearModelIds == null || !vehicleYearModelIds.Any())
            {
                vehicleGalleryItem.RemoveAllVehicleYearModels();
                return;
            }

            var query = (await _vehicleYearModelRepository.GetQueryableAsync())
                .Where(x => vehicleYearModelIds.Contains(x.Id))
                .Select(x => x.Id);

            var vehicleYearModelIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!vehicleYearModelIdsInDb.Any())
            {
                return;
            }

            vehicleGalleryItem.RemoveAllVehicleYearModelsExceptGivenIds(vehicleYearModelIdsInDb);

            foreach (var vehicleYearModelId in vehicleYearModelIdsInDb)
            {
                vehicleGalleryItem.AddVehicleYearModel(vehicleYearModelId);
            }
        }

        private async Task SetVehicleModelStylesAsync(VehicleGalleryItem vehicleGalleryItem, List<Guid> vehicleModelStyleIds)
        {
            if (vehicleModelStyleIds == null || !vehicleModelStyleIds.Any())
            {
                vehicleGalleryItem.RemoveAllVehicleModelStyles();
                return;
            }

            var query = (await _vehicleModelStyleRepository.GetQueryableAsync())
                .Where(x => vehicleModelStyleIds.Contains(x.Id))
                .Select(x => x.Id);

            var vehicleModelStyleIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!vehicleModelStyleIdsInDb.Any())
            {
                return;
            }

            vehicleGalleryItem.RemoveAllVehicleModelStylesExceptGivenIds(vehicleModelStyleIdsInDb);

            foreach (var vehicleModelStyleId in vehicleModelStyleIdsInDb)
            {
                vehicleGalleryItem.AddVehicleModelStyle(vehicleModelStyleId);
            }
        }

        private async Task SetVehicleBodyStylesAsync(VehicleGalleryItem vehicleGalleryItem, List<Guid> vehicleBodyStyleIds)
        {
            if (vehicleBodyStyleIds == null || !vehicleBodyStyleIds.Any())
            {
                vehicleGalleryItem.RemoveAllVehicleBodyStyles();
                return;
            }

            var query = (await _vehicleBodyStyleRepository.GetQueryableAsync())
                .Where(x => vehicleBodyStyleIds.Contains(x.Id))
                .Select(x => x.Id);

            var vehicleBodyStyleIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!vehicleBodyStyleIdsInDb.Any())
            {
                return;
            }

            vehicleGalleryItem.RemoveAllVehicleBodyStylesExceptGivenIds(vehicleBodyStyleIdsInDb);

            foreach (var vehicleBodyStyleId in vehicleBodyStyleIdsInDb)
            {
                vehicleGalleryItem.AddVehicleBodyStyle(vehicleBodyStyleId);
            }
        }

    }
}