using ImportSample.ProvinceCities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.Districts
{
    public abstract class DistrictBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }

        [NotNull]
        public virtual string Description { get; set; }
        public Guid ProvinceCityId { get; set; }

        protected DistrictBase()
        {

        }

        public DistrictBase(Guid id, Guid provinceCityId, string name, string slug, string description)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(slug, nameof(slug));
            Check.NotNull(description, nameof(description));
            Name = name;
            Slug = slug;
            Description = description;
            ProvinceCityId = provinceCityId;
        }

    }
}