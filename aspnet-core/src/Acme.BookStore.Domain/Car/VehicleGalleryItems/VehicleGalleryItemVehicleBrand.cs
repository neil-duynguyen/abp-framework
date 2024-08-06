using System;
using Volo.Abp.Domain.Entities;

namespace ImportSample.VehicleGalleryItems
{
    public class VehicleGalleryItemVehicleBrand : Entity
    {

        public Guid VehicleGalleryItemId { get; protected set; }

        public Guid VehicleBrandId { get; protected set; }

        private VehicleGalleryItemVehicleBrand()
        {

        }

        public VehicleGalleryItemVehicleBrand(Guid vehicleGalleryItemId, Guid vehicleBrandId)
        {
            VehicleGalleryItemId = vehicleGalleryItemId;
            VehicleBrandId = vehicleBrandId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    VehicleGalleryItemId,
                    VehicleBrandId
                };
        }
    }
}