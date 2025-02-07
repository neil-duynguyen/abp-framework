using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.Colors
{
    public class Color : ColorBase
    {
        //<suite-custom-code-autogenerated>
        protected Color()
        {

        }

        public Color(Guid id, string name, string enName, string description, string slug, string code, bool isVerified)
            : base(id, name, enName, description, slug, code, isVerified)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}