using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.VehicleCategories
{
    public abstract class VehicleCategoryBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }

        public virtual bool IsVerified { get; set; }

        protected VehicleCategoryBase()
        {

        }

        public VehicleCategoryBase(Guid id, string name, string description, string slug, bool isVerified)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(description, nameof(description));
            Check.NotNull(slug, nameof(slug));
            Name = name;
            Description = description;
            Slug = slug;
            IsVerified = isVerified;
        }

    }
}