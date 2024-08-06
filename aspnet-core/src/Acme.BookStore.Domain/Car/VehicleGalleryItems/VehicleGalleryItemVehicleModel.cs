using System;
using Volo.Abp.Domain.Entities;

namespace ImportSample.VehicleGalleryItems
{
    public class VehicleGalleryItemVehicleModel : Entity
    {

        public Guid VehicleGalleryItemId { get; protected set; }

        public Guid VehicleModelId { get; protected set; }

        private VehicleGalleryItemVehicleModel()
        {

        }

        public VehicleGalleryItemVehicleModel(Guid vehicleGalleryItemId, Guid vehicleModelId)
        {
            VehicleGalleryItemId = vehicleGalleryItemId;
            VehicleModelId = vehicleModelId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    VehicleGalleryItemId,
                    VehicleModelId
                };
        }
    }
}