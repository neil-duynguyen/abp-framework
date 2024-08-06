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
    public abstract class ColorBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string EnName { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        public virtual bool IsVerified { get; set; }

        protected ColorBase()
        {

        }

        public ColorBase(Guid id, string name, string enName, string description, string slug, string code, bool isVerified)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(enName, nameof(enName));
            Check.NotNull(description, nameof(description));
            Check.NotNull(slug, nameof(slug));
            Check.NotNull(code, nameof(code));
            Name = name;
            EnName = enName;
            Description = description;
            Slug = slug;
            Code = code;
            IsVerified = isVerified;
        }

    }
}