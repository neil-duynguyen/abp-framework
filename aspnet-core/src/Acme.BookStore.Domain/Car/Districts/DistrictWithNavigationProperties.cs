using ImportSample.ProvinceCities;

using System;
using System.Collections.Generic;

namespace ImportSample.Districts
{
    public abstract class DistrictWithNavigationPropertiesBase
    {
        public District District { get; set; } = null!;

        public ProvinceCity ProvinceCity { get; set; } = null!;
        

        
    }
}