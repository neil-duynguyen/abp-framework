using ImportSample.Colors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.ColorTranslations
{
    public class ColorTranslation : ColorTranslationBase
    {
        //<suite-custom-code-autogenerated>
        protected ColorTranslation()
        {

        }

        public ColorTranslation(Guid id, Guid colorId, string language, string name)
            : base(id, colorId, language, name)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}