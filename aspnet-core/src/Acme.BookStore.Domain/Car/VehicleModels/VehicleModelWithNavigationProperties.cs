using ImportSample.VehicleBrands;

using System;
using System.Collections.Generic;

namespace ImportSample.VehicleModels
{
    public abstract class VehicleModelWithNavigationPropertiesBase
    {
        public VehicleModel VehicleModel { get; set; } = null!;

        public VehicleBrand VehicleBrand { get; set; } = null!;
        

        
    }
}