using ImportSample.Districts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.Wards
{
    public abstract class WardBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }
        public Guid DistrictId { get; set; }

        protected WardBase()
        {

        }

        public WardBase(Guid id, Guid districtId, string name, string description, string slug)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(description, nameof(description));
            Check.NotNull(slug, nameof(slug));
            Name = name;
            Description = description;
            Slug = slug;
            DistrictId = districtId;
        }

    }
}