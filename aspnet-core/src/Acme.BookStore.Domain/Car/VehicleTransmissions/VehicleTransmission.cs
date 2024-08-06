using ImportSample.VehicleTransmissions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.VehicleTransmissions
{
    public abstract class VehicleTransmissionBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Label { get; set; }

        public virtual int Speeds { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }

        public virtual VehicleTransmissionType Type { get; set; }

        public virtual bool IsVerified { get; set; }

        protected VehicleTransmissionBase()
        {

        }

        public VehicleTransmissionBase(Guid id, string label, int speeds, string description, string slug, VehicleTransmissionType type, bool isVerified)
        {

            Id = id;
            Check.NotNull(label, nameof(label));
            Check.NotNull(description, nameof(description));
            Check.NotNull(slug, nameof(slug));
            Label = label;
            Speeds = speeds;
            Description = description;
            Slug = slug;
            Type = type;
            IsVerified = isVerified;
        }

    }
}