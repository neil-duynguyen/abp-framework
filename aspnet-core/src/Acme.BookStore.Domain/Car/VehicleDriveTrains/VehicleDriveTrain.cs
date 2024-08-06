using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.VehicleDriveTrains
{
    public abstract class VehicleDriveTrainBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string EnName { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual bool IsVerified { get; set; }

        protected VehicleDriveTrainBase()
        {

        }

        public VehicleDriveTrainBase(Guid id, string enName, string name, string slug, string description, bool isVerified)
        {

            Id = id;
            Check.NotNull(enName, nameof(enName));
            Check.NotNull(name, nameof(name));
            Check.NotNull(slug, nameof(slug));
            Check.NotNull(description, nameof(description));
            EnName = enName;
            Name = name;
            Slug = slug;
            Description = description;
            IsVerified = isVerified;
        }

    }
}