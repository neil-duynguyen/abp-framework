using ImportSample.VehicleModels;

using System;
using System.Collections.Generic;

namespace ImportSample.VehicleYearModels
{
    public abstract class VehicleYearModelWithNavigationPropertiesBase
    {
        public VehicleYearModel VehicleYearModel { get; set; } = null!;

        public VehicleModel VehicleModel { get; set; } = null!;
        

        
    }
}