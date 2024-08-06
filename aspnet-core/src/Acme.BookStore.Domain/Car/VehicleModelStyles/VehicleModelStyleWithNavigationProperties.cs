using ImportSample.VehicleYearModels;
using ImportSample.VehicleCategories;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleEngines;
using ImportSample.VehicleTransmissions;
using ImportSample.VehicleDriveTrains;
using ImportSample.Colors;

using System;
using System.Collections.Generic;

namespace ImportSample.VehicleModelStyles
{
    public abstract class VehicleModelStyleWithNavigationPropertiesBase
    {
        public VehicleModelStyle VehicleModelStyle { get; set; } = null!;

        public VehicleYearModel VehicleYearModel { get; set; } = null!;
        public VehicleCategory VehicleCategory { get; set; } = null!;
        public VehicleBodyStyle VehicleBodyStyle { get; set; } = null!;
        public VehicleEngine VehicleEngine { get; set; } = null!;
        public VehicleTransmission VehicleTransmission { get; set; } = null!;
        public VehicleDriveTrain VehicleDriveTrain { get; set; } = null!;
        

        public List<Color> Colors { get; set; } = null!;
        
    }
}