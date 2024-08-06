using ImportSample.Districts;

using System;
using System.Collections.Generic;

namespace ImportSample.Wards
{
    public abstract class WardWithNavigationPropertiesBase
    {
        public Ward Ward { get; set; } = null!;

        public District District { get; set; } = null!;
        

        
    }
}