using ImportSample.VehicleModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.VehicleYearModels
{
    public class VehicleYearModel : VehicleYearModelBase
    {
        //<suite-custom-code-autogenerated>
        protected VehicleYearModel()
        {

        }

        public VehicleYearModel(Guid id, Guid vehicleModelId, string name, string slug, string description, int year, bool isVerified)
            : base(id, vehicleModelId, name, slug, description, year, isVerified)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}