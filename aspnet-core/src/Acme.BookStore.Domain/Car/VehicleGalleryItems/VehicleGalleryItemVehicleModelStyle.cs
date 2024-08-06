using System;
using Volo.Abp.Domain.Entities;

namespace ImportSample.VehicleGalleryItems
{
    public class VehicleGalleryItemVehicleModelStyle : Entity
    {

        public Guid VehicleGalleryItemId { get; protected set; }

        public Guid VehicleModelStyleId { get; protected set; }

        private VehicleGalleryItemVehicleModelStyle()
        {

        }

        public VehicleGalleryItemVehicleModelStyle(Guid vehicleGalleryItemId, Guid vehicleModelStyleId)
        {
            VehicleGalleryItemId = vehicleGalleryItemId;
            VehicleModelStyleId = vehicleModelStyleId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    VehicleGalleryItemId,
                    VehicleModelStyleId
                };
        }
    }
}