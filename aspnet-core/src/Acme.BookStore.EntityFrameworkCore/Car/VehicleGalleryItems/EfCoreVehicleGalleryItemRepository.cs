using ImportSample.VehicleGalleryItems;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleModelStyles;
using ImportSample.VehicleYearModels;
using ImportSample.VehicleModels;
using ImportSample.VehicleBrands;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleModelStyles;
using ImportSample.VehicleYearModels;
using ImportSample.VehicleModels;
using ImportSample.VehicleBrands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Acme.BookStore.EntityFrameworkCore;

namespace ImportSample.VehicleGalleryItems
{
    public abstract class EfCoreVehicleGalleryItemRepositoryBase : EfCoreRepository<BookStoreDbContext, VehicleGalleryItem, Guid>
    {
        public EfCoreVehicleGalleryItemRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<VehicleGalleryItemWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.VehicleBrands).Include(x => x.VehicleModels).Include(x => x.VehicleYearModels).Include(x => x.VehicleModelStyles).Include(x => x.VehicleBodyStyles)
                .Select(vehicleGalleryItem => new VehicleGalleryItemWithNavigationProperties
                {
                    VehicleGalleryItem = vehicleGalleryItem,
                    VehicleBrands = (from vehicleGalleryItemVehicleBrands in vehicleGalleryItem.VehicleBrands
                                     join _vehicleBrand in dbContext.Set<VehicleBrand>() on vehicleGalleryItemVehicleBrands.VehicleBrandId equals _vehicleBrand.Id
                                     select _vehicleBrand).ToList(),
                    VehicleModels = (from vehicleGalleryItemVehicleModels in vehicleGalleryItem.VehicleModels
                                     join _vehicleModel in dbContext.Set<VehicleModel>() on vehicleGalleryItemVehicleModels.VehicleModelId equals _vehicleModel.Id
                                     select _vehicleModel).ToList(),
                    VehicleYearModels = (from vehicleGalleryItemVehicleYearModels in vehicleGalleryItem.VehicleYearModels
                                         join _vehicleYearModel in dbContext.Set<VehicleYearModel>() on vehicleGalleryItemVehicleYearModels.VehicleYearModelId equals _vehicleYearModel.Id
                                         select _vehicleYearModel).ToList(),
                    VehicleModelStyles = (from vehicleGalleryItemVehicleModelStyles in vehicleGalleryItem.VehicleModelStyles
                                          join _vehicleModelStyle in dbContext.Set<VehicleModelStyle>() on vehicleGalleryItemVehicleModelStyles.VehicleModelStyleId equals _vehicleModelStyle.Id
                                          select _vehicleModelStyle).ToList(),
                    VehicleBodyStyles = (from vehicleGalleryItemVehicleBodyStyles in vehicleGalleryItem.VehicleBodyStyles
                                         join _vehicleBodyStyle in dbContext.Set<VehicleBodyStyle>() on vehicleGalleryItemVehicleBodyStyles.VehicleBodyStyleId equals _vehicleBodyStyle.Id
                                         select _vehicleBodyStyle).ToList()
                }).FirstOrDefault();
        }

        public virtual async Task<List<VehicleGalleryItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            int? orderMin = null,
            int? orderMax = null,
            string? assetPath = null,
            VehicleGalleryItemType? type = null,
            Guid? vehicleBrandId = null,
            Guid? vehicleModelId = null,
            Guid? vehicleYearModelId = null,
            Guid? vehicleModelStyleId = null,
            Guid? vehicleBodyStyleId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, orderMin, orderMax, assetPath, type, vehicleBrandId, vehicleModelId, vehicleYearModelId, vehicleModelStyleId, vehicleBodyStyleId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleGalleryItemConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VehicleGalleryItemWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vehicleGalleryItem in (await GetDbSetAsync())

                   select new VehicleGalleryItemWithNavigationProperties
                   {
                       VehicleGalleryItem = vehicleGalleryItem,
                       VehicleBrands = new List<VehicleBrand>(),
                       VehicleModels = new List<VehicleModel>(),
                       VehicleYearModels = new List<VehicleYearModel>(),
                       VehicleModelStyles = new List<VehicleModelStyle>(),
                       VehicleBodyStyles = new List<VehicleBodyStyle>()
                   };
        }

        protected virtual IQueryable<VehicleGalleryItemWithNavigationProperties> ApplyFilter(
            IQueryable<VehicleGalleryItemWithNavigationProperties> query,
            string? filterText,
            int? orderMin = null,
            int? orderMax = null,
            string? assetPath = null,
            VehicleGalleryItemType? type = null,
            Guid? vehicleBrandId = null,
            Guid? vehicleModelId = null,
            Guid? vehicleYearModelId = null,
            Guid? vehicleModelStyleId = null,
            Guid? vehicleBodyStyleId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VehicleGalleryItem.AssetPath!.Contains(filterText!))
                    .WhereIf(orderMin.HasValue, e => e.VehicleGalleryItem.Order >= orderMin!.Value)
                    .WhereIf(orderMax.HasValue, e => e.VehicleGalleryItem.Order <= orderMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(assetPath), e => e.VehicleGalleryItem.AssetPath.Contains(assetPath))
                    .WhereIf(type.HasValue, e => e.VehicleGalleryItem.Type == type)
                    .WhereIf(vehicleBrandId != null && vehicleBrandId != Guid.Empty, e => e.VehicleGalleryItem.VehicleBrands.Any(x => x.VehicleBrandId == vehicleBrandId))
                    .WhereIf(vehicleModelId != null && vehicleModelId != Guid.Empty, e => e.VehicleGalleryItem.VehicleModels.Any(x => x.VehicleModelId == vehicleModelId))
                    .WhereIf(vehicleYearModelId != null && vehicleYearModelId != Guid.Empty, e => e.VehicleGalleryItem.VehicleYearModels.Any(x => x.VehicleYearModelId == vehicleYearModelId))
                    .WhereIf(vehicleModelStyleId != null && vehicleModelStyleId != Guid.Empty, e => e.VehicleGalleryItem.VehicleModelStyles.Any(x => x.VehicleModelStyleId == vehicleModelStyleId))
                    .WhereIf(vehicleBodyStyleId != null && vehicleBodyStyleId != Guid.Empty, e => e.VehicleGalleryItem.VehicleBodyStyles.Any(x => x.VehicleBodyStyleId == vehicleBodyStyleId));
        }

        public virtual async Task<List<VehicleGalleryItem>> GetListAsync(
            string? filterText = null,
            int? orderMin = null,
            int? orderMax = null,
            string? assetPath = null,
            VehicleGalleryItemType? type = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, orderMin, orderMax, assetPath, type);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleGalleryItemConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            int? orderMin = null,
            int? orderMax = null,
            string? assetPath = null,
            VehicleGalleryItemType? type = null,
            Guid? vehicleBrandId = null,
            Guid? vehicleModelId = null,
            Guid? vehicleYearModelId = null,
            Guid? vehicleModelStyleId = null,
            Guid? vehicleBodyStyleId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, orderMin, orderMax, assetPath, type, vehicleBrandId, vehicleModelId, vehicleYearModelId, vehicleModelStyleId, vehicleBodyStyleId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VehicleGalleryItem> ApplyFilter(
            IQueryable<VehicleGalleryItem> query,
            string? filterText = null,
            int? orderMin = null,
            int? orderMax = null,
            string? assetPath = null,
            VehicleGalleryItemType? type = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AssetPath!.Contains(filterText!))
                    .WhereIf(orderMin.HasValue, e => e.Order >= orderMin!.Value)
                    .WhereIf(orderMax.HasValue, e => e.Order <= orderMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(assetPath), e => e.AssetPath.Contains(assetPath))
                    .WhereIf(type.HasValue, e => e.Type == type);
        }
    }
}