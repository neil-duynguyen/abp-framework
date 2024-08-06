using System;
using Volo.Abp.Domain.Entities;

namespace ImportSample.VehicleModelStyles
{
    public class VehicleModelStyleColor : Entity
    {

        public Guid VehicleModelStyleId { get; protected set; }

        public Guid ColorId { get; protected set; }

        private VehicleModelStyleColor()
        {

        }

        public VehicleModelStyleColor(Guid vehicleModelStyleId, Guid colorId)
        {
            VehicleModelStyleId = vehicleModelStyleId;
            ColorId = colorId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    VehicleModelStyleId,
                    ColorId
                };
        }
    }
}