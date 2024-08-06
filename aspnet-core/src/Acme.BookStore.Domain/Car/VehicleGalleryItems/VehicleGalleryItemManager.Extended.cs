using ImportSample.VehicleBrands;
using ImportSample.VehicleModels;
using ImportSample.VehicleYearModels;
using ImportSample.VehicleModelStyles;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleGalleryItems;
using System;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleGalleryItems
{
    public class VehicleGalleryItemManager : VehicleGalleryItemManagerBase
    {
        //<suite-custom-code-autogenerated>
        public VehicleGalleryItemManager(IVehicleGalleryItemRepository vehicleGalleryItemRepository,
        IRepository<VehicleBrand, Guid> vehicleBrandRepository,
        IRepository<VehicleModel, Guid> vehicleModelRepository,
        IRepository<VehicleYearModel, Guid> vehicleYearModelRepository,
        IRepository<VehicleModelStyle, Guid> vehicleModelStyleRepository,
        IRepository<VehicleBodyStyle, Guid> vehicleBodyStyleRepository)
            : base(vehicleGalleryItemRepository, vehicleBrandRepository, vehicleModelRepository, vehicleYearModelRepository, vehicleModelStyleRepository, vehicleBodyStyleRepository)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}