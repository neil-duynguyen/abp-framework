using ImportSample.VehicleGalleryItems;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.VehicleGalleryItems
{
    public abstract class VehicleGalleryItemBase : FullAuditedAggregateRoot<Guid>
    {
        public virtual int Order { get; set; }

        [NotNull]
        public virtual string AssetPath { get; set; }

        public virtual VehicleGalleryItemType Type { get; set; }

        public ICollection<VehicleGalleryItemVehicleBrand> VehicleBrands { get; private set; }
        public ICollection<VehicleGalleryItemVehicleModel> VehicleModels { get; private set; }
        public ICollection<VehicleGalleryItemVehicleYearModel> VehicleYearModels { get; private set; }
        public ICollection<VehicleGalleryItemVehicleModelStyle> VehicleModelStyles { get; private set; }
        public ICollection<VehicleGalleryItemVehicleBodyStyle> VehicleBodyStyles { get; private set; }

        protected VehicleGalleryItemBase()
        {

        }

        public VehicleGalleryItemBase(Guid id, int order, string assetPath, VehicleGalleryItemType type)
        {

            Id = id;
            Check.NotNull(assetPath, nameof(assetPath));
            Order = order;
            AssetPath = assetPath;
            Type = type;
            VehicleBrands = new Collection<VehicleGalleryItemVehicleBrand>();
            VehicleModels = new Collection<VehicleGalleryItemVehicleModel>();
            VehicleYearModels = new Collection<VehicleGalleryItemVehicleYearModel>();
            VehicleModelStyles = new Collection<VehicleGalleryItemVehicleModelStyle>();
            VehicleBodyStyles = new Collection<VehicleGalleryItemVehicleBodyStyle>();
        }
        public virtual void AddVehicleBrand(Guid vehicleBrandId)
        {
            Check.NotNull(vehicleBrandId, nameof(vehicleBrandId));

            if (IsInVehicleBrands(vehicleBrandId))
            {
                return;
            }

            VehicleBrands.Add(new VehicleGalleryItemVehicleBrand(Id, vehicleBrandId));
        }

        public virtual void RemoveVehicleBrand(Guid vehicleBrandId)
        {
            Check.NotNull(vehicleBrandId, nameof(vehicleBrandId));

            if (!IsInVehicleBrands(vehicleBrandId))
            {
                return;
            }

            VehicleBrands.RemoveAll(x => x.VehicleBrandId == vehicleBrandId);
        }

        public virtual void RemoveAllVehicleBrandsExceptGivenIds(List<Guid> vehicleBrandIds)
        {
            Check.NotNullOrEmpty(vehicleBrandIds, nameof(vehicleBrandIds));

            VehicleBrands.RemoveAll(x => !vehicleBrandIds.Contains(x.VehicleBrandId));
        }

        public virtual void RemoveAllVehicleBrands()
        {
            VehicleBrands.RemoveAll(x => x.VehicleGalleryItemId == Id);
        }

        private bool IsInVehicleBrands(Guid vehicleBrandId)
        {
            return VehicleBrands.Any(x => x.VehicleBrandId == vehicleBrandId);
        }

        public virtual void AddVehicleModel(Guid vehicleModelId)
        {
            Check.NotNull(vehicleModelId, nameof(vehicleModelId));

            if (IsInVehicleModels(vehicleModelId))
            {
                return;
            }

            VehicleModels.Add(new VehicleGalleryItemVehicleModel(Id, vehicleModelId));
        }

        public virtual void RemoveVehicleModel(Guid vehicleModelId)
        {
            Check.NotNull(vehicleModelId, nameof(vehicleModelId));

            if (!IsInVehicleModels(vehicleModelId))
            {
                return;
            }

            VehicleModels.RemoveAll(x => x.VehicleModelId == vehicleModelId);
        }

        public virtual void RemoveAllVehicleModelsExceptGivenIds(List<Guid> vehicleModelIds)
        {
            Check.NotNullOrEmpty(vehicleModelIds, nameof(vehicleModelIds));

            VehicleModels.RemoveAll(x => !vehicleModelIds.Contains(x.VehicleModelId));
        }

        public virtual void RemoveAllVehicleModels()
        {
            VehicleModels.RemoveAll(x => x.VehicleGalleryItemId == Id);
        }

        private bool IsInVehicleModels(Guid vehicleModelId)
        {
            return VehicleModels.Any(x => x.VehicleModelId == vehicleModelId);
        }

        public virtual void AddVehicleYearModel(Guid vehicleYearModelId)
        {
            Check.NotNull(vehicleYearModelId, nameof(vehicleYearModelId));

            if (IsInVehicleYearModels(vehicleYearModelId))
            {
                return;
            }

            VehicleYearModels.Add(new VehicleGalleryItemVehicleYearModel(Id, vehicleYearModelId));
        }

        public virtual void RemoveVehicleYearModel(Guid vehicleYearModelId)
        {
            Check.NotNull(vehicleYearModelId, nameof(vehicleYearModelId));

            if (!IsInVehicleYearModels(vehicleYearModelId))
            {
                return;
            }

            VehicleYearModels.RemoveAll(x => x.VehicleYearModelId == vehicleYearModelId);
        }

        public virtual void RemoveAllVehicleYearModelsExceptGivenIds(List<Guid> vehicleYearModelIds)
        {
            Check.NotNullOrEmpty(vehicleYearModelIds, nameof(vehicleYearModelIds));

            VehicleYearModels.RemoveAll(x => !vehicleYearModelIds.Contains(x.VehicleYearModelId));
        }

        public virtual void RemoveAllVehicleYearModels()
        {
            VehicleYearModels.RemoveAll(x => x.VehicleGalleryItemId == Id);
        }

        private bool IsInVehicleYearModels(Guid vehicleYearModelId)
        {
            return VehicleYearModels.Any(x => x.VehicleYearModelId == vehicleYearModelId);
        }

        public virtual void AddVehicleModelStyle(Guid vehicleModelStyleId)
        {
            Check.NotNull(vehicleModelStyleId, nameof(vehicleModelStyleId));

            if (IsInVehicleModelStyles(vehicleModelStyleId))
            {
                return;
            }

            VehicleModelStyles.Add(new VehicleGalleryItemVehicleModelStyle(Id, vehicleModelStyleId));
        }

        public virtual void RemoveVehicleModelStyle(Guid vehicleModelStyleId)
        {
            Check.NotNull(vehicleModelStyleId, nameof(vehicleModelStyleId));

            if (!IsInVehicleModelStyles(vehicleModelStyleId))
            {
                return;
            }

            VehicleModelStyles.RemoveAll(x => x.VehicleModelStyleId == vehicleModelStyleId);
        }

        public virtual void RemoveAllVehicleModelStylesExceptGivenIds(List<Guid> vehicleModelStyleIds)
        {
            Check.NotNullOrEmpty(vehicleModelStyleIds, nameof(vehicleModelStyleIds));

            VehicleModelStyles.RemoveAll(x => !vehicleModelStyleIds.Contains(x.VehicleModelStyleId));
        }

        public virtual void RemoveAllVehicleModelStyles()
        {
            VehicleModelStyles.RemoveAll(x => x.VehicleGalleryItemId == Id);
        }

        private bool IsInVehicleModelStyles(Guid vehicleModelStyleId)
        {
            return VehicleModelStyles.Any(x => x.VehicleModelStyleId == vehicleModelStyleId);
        }

        public virtual void AddVehicleBodyStyle(Guid vehicleBodyStyleId)
        {
            Check.NotNull(vehicleBodyStyleId, nameof(vehicleBodyStyleId));

            if (IsInVehicleBodyStyles(vehicleBodyStyleId))
            {
                return;
            }

            VehicleBodyStyles.Add(new VehicleGalleryItemVehicleBodyStyle(Id, vehicleBodyStyleId));
        }

        public virtual void RemoveVehicleBodyStyle(Guid vehicleBodyStyleId)
        {
            Check.NotNull(vehicleBodyStyleId, nameof(vehicleBodyStyleId));

            if (!IsInVehicleBodyStyles(vehicleBodyStyleId))
            {
                return;
            }

            VehicleBodyStyles.RemoveAll(x => x.VehicleBodyStyleId == vehicleBodyStyleId);
        }

        public virtual void RemoveAllVehicleBodyStylesExceptGivenIds(List<Guid> vehicleBodyStyleIds)
        {
            Check.NotNullOrEmpty(vehicleBodyStyleIds, nameof(vehicleBodyStyleIds));

            VehicleBodyStyles.RemoveAll(x => !vehicleBodyStyleIds.Contains(x.VehicleBodyStyleId));
        }

        public virtual void RemoveAllVehicleBodyStyles()
        {
            VehicleBodyStyles.RemoveAll(x => x.VehicleGalleryItemId == Id);
        }

        private bool IsInVehicleBodyStyles(Guid vehicleBodyStyleId)
        {
            return VehicleBodyStyles.Any(x => x.VehicleBodyStyleId == vehicleBodyStyleId);
        }
    }
}