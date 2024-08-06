using ImportSample.VehicleBrands;
using ImportSample.VehicleModels;
using ImportSample.VehicleYearModels;
using ImportSample.VehicleModelStyles;
using ImportSample.VehicleBodyStyles;

using System;
using System.Collections.Generic;

namespace ImportSample.VehicleGalleryItems
{
    public abstract class VehicleGalleryItemWithNavigationPropertiesBase
    {
        public VehicleGalleryItem VehicleGalleryItem { get; set; } = null!;

        

        public List<VehicleBrand> VehicleBrands { get; set; } = null!;
        public List<VehicleModel> VehicleModels { get; set; } = null!;
        public List<VehicleYearModel> VehicleYearModels { get; set; } = null!;
        public List<VehicleModelStyle> VehicleModelStyles { get; set; } = null!;
        public List<VehicleBodyStyle> VehicleBodyStyles { get; set; } = null!;
        
    }
}