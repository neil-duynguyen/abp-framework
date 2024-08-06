using ImportSample.Colors;

using System;
using System.Collections.Generic;

namespace ImportSample.ColorTranslations
{
    public abstract class ColorTranslationWithNavigationPropertiesBase
    {
        public ColorTranslation ColorTranslation { get; set; } = null!;

        public Color Color { get; set; } = null!;
        

        
    }
}