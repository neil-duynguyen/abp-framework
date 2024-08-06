using ImportSample.VehicleEngines;
using ImportSample.VehicleBrands;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.VehicleEngines
{
    public abstract class VehicleEngineBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Label { get; set; }

        public virtual int HorsePower { get; set; }

        public virtual int Torque { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual VehicleEngineType Type { get; set; }

        public virtual bool IsVerified { get; set; }
        public Guid? VehicleBrandId { get; set; }

        protected VehicleEngineBase()
        {

        }

        public VehicleEngineBase(Guid id, Guid? vehicleBrandId, string label, int horsePower, int torque, string slug, string description, VehicleEngineType type, bool isVerified)
        {

            Id = id;
            Check.NotNull(label, nameof(label));
            Check.NotNull(slug, nameof(slug));
            Check.NotNull(description, nameof(description));
            Label = label;
            HorsePower = horsePower;
            Torque = torque;
            Slug = slug;
            Description = description;
            Type = type;
            IsVerified = isVerified;
            VehicleBrandId = vehicleBrandId;
        }

    }
}