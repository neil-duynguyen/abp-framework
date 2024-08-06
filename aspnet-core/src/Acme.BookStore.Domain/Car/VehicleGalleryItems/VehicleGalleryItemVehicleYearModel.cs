using System;
using Volo.Abp.Domain.Entities;

namespace ImportSample.VehicleGalleryItems
{
    public class VehicleGalleryItemVehicleYearModel : Entity
    {

        public Guid VehicleGalleryItemId { get; protected set; }

        public Guid VehicleYearModelId { get; protected set; }

        private VehicleGalleryItemVehicleYearModel()
        {

        }

        public VehicleGalleryItemVehicleYearModel(Guid vehicleGalleryItemId, Guid vehicleYearModelId)
        {
            VehicleGalleryItemId = vehicleGalleryItemId;
            VehicleYearModelId = vehicleYearModelId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    VehicleGalleryItemId,
                    VehicleYearModelId
                };
        }
    }
}