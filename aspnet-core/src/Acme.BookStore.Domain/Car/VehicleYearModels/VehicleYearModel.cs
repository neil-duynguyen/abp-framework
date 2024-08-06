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
    public abstract class VehicleYearModelBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Slug { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual int Year { get; set; }

        public virtual bool IsVerified { get; set; }
        public Guid VehicleModelId { get; set; }

        protected VehicleYearModelBase()
        {

        }

        public VehicleYearModelBase(Guid id, Guid vehicleModelId, string name, string slug, string description, int year, bool isVerified)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(slug, nameof(slug));
            Check.NotNull(description, nameof(description));
            Name = name;
            Slug = slug;
            Description = description;
            Year = year;
            IsVerified = isVerified;
            VehicleModelId = vehicleModelId;
        }

    }
}