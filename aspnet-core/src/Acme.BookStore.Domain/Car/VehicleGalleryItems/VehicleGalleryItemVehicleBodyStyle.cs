using System;
using Volo.Abp.Domain.Entities;

namespace ImportSample.VehicleGalleryItems
{
    public class VehicleGalleryItemVehicleBodyStyle : Entity
    {

        public Guid VehicleGalleryItemId { get; protected set; }

        public Guid VehicleBodyStyleId { get; protected set; }

        private VehicleGalleryItemVehicleBodyStyle()
        {

        }

        public VehicleGalleryItemVehicleBodyStyle(Guid vehicleGalleryItemId, Guid vehicleBodyStyleId)
        {
            VehicleGalleryItemId = vehicleGalleryItemId;
            VehicleBodyStyleId = vehicleBodyStyleId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    VehicleGalleryItemId,
                    VehicleBodyStyleId
                };
        }
    }
}