using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.VehicleBrands
{
    public class VehicleBrand : VehicleBrandBase
    {
        //<suite-custom-code-autogenerated>
        protected VehicleBrand()
        {

        }

        public VehicleBrand(Guid id, string name, string slug, string description, bool isVerified)
            : base(id, name, slug, description, isVerified)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}