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
    public abstract class VehicleBrandBase : FullAuditedAggregateRoot<Guid>
    {
        public virtual string? Name { get; set; }
      
        public virtual string? Slug { get; set; }
      
        public virtual string? Description { get; set; }

        public virtual bool? IsVerified { get; set; }

        protected VehicleBrandBase()
        {

        }

        protected VehicleBrandBase(Guid id, string? name, string? slug, string? description, bool? isVerified)
        {
            Id = id;
            Name = name;
            Slug = slug;
            Description = description;
            IsVerified = isVerified;
        }
    }
}