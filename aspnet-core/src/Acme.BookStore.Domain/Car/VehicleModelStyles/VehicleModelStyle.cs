using ImportSample.VehicleModelStyles;
using ImportSample.VehicleYearModels;
using ImportSample.VehicleCategories;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleEngines;
using ImportSample.VehicleTransmissions;
using ImportSample.VehicleDriveTrains;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.VehicleModelStyles
{
    public abstract class VehicleModelStyleBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual string? Slug { get; set; }

        [CanBeNull]
        public virtual string? Highlight { get; set; }

        public virtual float? Capacity { get; set; } //dung tích

        public virtual float? OperatingRange { get; set; }

        public virtual float? CombinedKm { get; set; }

        public virtual float? CityKm { get; set; }

        public virtual float? HighwayKm { get; set; }

        [CanBeNull]
        public virtual string? BatteryType { get; set; }

        public virtual float? StandardCharging { get; set; }

        public virtual float? RapidCharging { get; set; }

        public virtual int? SafetyRate { get; set; }

        public virtual int? Seats { get; set; }

        public virtual int? Doors { get; set; }

        [CanBeNull]
        public virtual string? StandardFeature { get; set; }

        [CanBeNull]
        public virtual string? TechnicalFeature { get; set; }

        [CanBeNull]
        public virtual string? MadeIn { get; set; }

        public virtual VehicleModelStyleStatus Status { get; set; }
        public Guid VehicleYearModelId { get; set; }
        public Guid? VehicleCategoryId { get; set; }
        public Guid? VehicleBodyStyleId { get; set; }
        public Guid? VehicleEngineId { get; set; }
        public Guid? VehicleTransmissionId { get; set; }
        public Guid? VehicleDriveTrainId { get; set; }
        public ICollection<VehicleModelStyleColor>? Colors { get; private set; }

        protected VehicleModelStyleBase()
        {

        }

        public VehicleModelStyleBase(Guid id, Guid vehicleYearModelId, Guid? vehicleCategoryId, Guid? vehicleBodyStyleId, Guid? vehicleEngineId, Guid? vehicleTransmissionId, Guid? vehicleDriveTrainId, string name, string description, string slug, int seats, int doors, string madeIn, VehicleModelStyleStatus status, string? highlight = null, float? capacity = null, float? operatingRange = null, float? combinedKm = null, float? cityKm = null, float? highwayKm = null, string? batteryType = null, float? standardCharging = null, float? rapidCharging = null, int? safetyRate = null, string? standardFeature = null, string? technicalFeature = null)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(description, nameof(description));
            Check.NotNull(slug, nameof(slug));
            Check.NotNull(madeIn, nameof(madeIn));
            Name = name;
            Description = description;
            Slug = slug;
            Seats = seats;
            Doors = doors;
            MadeIn = madeIn;
            Status = status;
            Highlight = highlight;
            Capacity = capacity;
            OperatingRange = operatingRange;
            CombinedKm = combinedKm;
            CityKm = cityKm;
            HighwayKm = highwayKm;
            BatteryType = batteryType;
            StandardCharging = standardCharging;
            RapidCharging = rapidCharging;
            SafetyRate = safetyRate;
            StandardFeature = standardFeature;
            TechnicalFeature = technicalFeature;
            VehicleYearModelId = vehicleYearModelId;
            VehicleCategoryId = vehicleCategoryId;
            VehicleBodyStyleId = vehicleBodyStyleId;
            VehicleEngineId = vehicleEngineId;
            VehicleTransmissionId = vehicleTransmissionId;
            VehicleDriveTrainId = vehicleDriveTrainId;
            Colors = new Collection<VehicleModelStyleColor>();
        }
        public virtual void AddColor(Guid colorId)
        {
            Check.NotNull(colorId, nameof(colorId));

            if (IsInColors(colorId))
            {
                return;
            }

            Colors.Add(new VehicleModelStyleColor(Id, colorId));
        }

        public virtual void RemoveColor(Guid colorId)
        {
            Check.NotNull(colorId, nameof(colorId));

            if (!IsInColors(colorId))
            {
                return;
            }

            Colors.RemoveAll(x => x.ColorId == colorId);
        }

        public virtual void RemoveAllColorsExceptGivenIds(List<Guid> colorIds)
        {
            Check.NotNullOrEmpty(colorIds, nameof(colorIds));

            Colors.RemoveAll(x => !colorIds.Contains(x.ColorId));
        }

        public virtual void RemoveAllColors()
        {
            Colors.RemoveAll(x => x.VehicleModelStyleId == Id);
        }

        private bool IsInColors(Guid colorId)
        {
            return Colors.Any(x => x.ColorId == colorId);
        }
    }
}