using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.ProvinceCities
{
    public abstract class ProvinceCityBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        protected ProvinceCityBase()
        {

        }

        public ProvinceCityBase(Guid id, string name, string slug, string description)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(slug, nameof(slug));
            Check.NotNull(description, nameof(description));
            Name = name;
            Slug = slug;
            Description = description;
        }

    }
}