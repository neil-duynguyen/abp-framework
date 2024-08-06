using ImportSample.VehicleBrands;

using System;
using System.Collections.Generic;

namespace ImportSample.VehicleEngines
{
    public abstract class VehicleEngineWithNavigationPropertiesBase
    {
        public VehicleEngine VehicleEngine { get; set; } = null!;

        public VehicleBrand VehicleBrand { get; set; } = null!;
        

        
    }
}