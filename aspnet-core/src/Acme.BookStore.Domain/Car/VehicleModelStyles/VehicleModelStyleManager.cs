using ImportSample.Colors;
using ImportSample.VehicleModelStyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleModelStyles
{
    public abstract class VehicleModelStyleManagerBase : DomainService
    {
        protected IVehicleModelStyleRepository _vehicleModelStyleRepository;
        protected IRepository<Color, Guid> _colorRepository;

        public VehicleModelStyleManagerBase(IVehicleModelStyleRepository vehicleModelStyleRepository,
        IRepository<Color, Guid> colorRepository)
        {
            _vehicleModelStyleRepository = vehicleModelStyleRepository;
            _colorRepository = colorRepository;
        }

        public virtual async Task<VehicleModelStyle> CreateAsync(
        List<Guid> colorIds,
        Guid vehicleYearModelId, Guid? vehicleCategoryId, Guid? vehicleBodyStyleId, Guid? vehicleEngineId, Guid? vehicleTransmissionId, Guid? vehicleDriveTrainId, string name, string description, string slug, int seats, int doors, string madeIn, VehicleModelStyleStatus status, string? highlight = null, float? capacity = null, float? operatingRange = null, float? combinedKm = null, float? cityKm = null, float? highwayKm = null, string? batteryType = null, float? standardCharging = null, float? rapidCharging = null, int? safetyRate = null, string? standardFeature = null, string? technicalFeature = null)
        {
            Check.NotNull(vehicleYearModelId, nameof(vehicleYearModelId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(madeIn, nameof(madeIn));
            Check.NotNull(status, nameof(status));

            var vehicleModelStyle = new VehicleModelStyle(
             GuidGenerator.Create(),
             vehicleYearModelId, vehicleCategoryId, vehicleBodyStyleId, vehicleEngineId, vehicleTransmissionId, vehicleDriveTrainId, name, description, slug, seats, doors, madeIn, status, highlight, capacity, operatingRange, combinedKm, cityKm, highwayKm, batteryType, standardCharging, rapidCharging, safetyRate, standardFeature, technicalFeature
             );

            await SetColorsAsync(vehicleModelStyle, colorIds);

            return await _vehicleModelStyleRepository.InsertAsync(vehicleModelStyle);
        }

        public virtual async Task<VehicleModelStyle> UpdateAsync(
            Guid id,
            List<Guid> colorIds,
        Guid vehicleYearModelId, Guid? vehicleCategoryId, Guid? vehicleBodyStyleId, Guid? vehicleEngineId, Guid? vehicleTransmissionId, Guid? vehicleDriveTrainId, string name, string description, string slug, int seats, int doors, string madeIn, VehicleModelStyleStatus status, string? highlight = null, float? capacity = null, float? operatingRange = null, float? combinedKm = null, float? cityKm = null, float? highwayKm = null, string? batteryType = null, float? standardCharging = null, float? rapidCharging = null, int? safetyRate = null, string? standardFeature = null, string? technicalFeature = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(vehicleYearModelId, nameof(vehicleYearModelId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(madeIn, nameof(madeIn));
            Check.NotNull(status, nameof(status));

            var queryable = await _vehicleModelStyleRepository.WithDetailsAsync(x => x.Colors);
            var query = queryable.Where(x => x.Id == id);

            var vehicleModelStyle = await AsyncExecuter.FirstOrDefaultAsync(query);

            vehicleModelStyle.VehicleYearModelId = vehicleYearModelId;
            vehicleModelStyle.VehicleCategoryId = vehicleCategoryId;
            vehicleModelStyle.VehicleBodyStyleId = vehicleBodyStyleId;
            vehicleModelStyle.VehicleEngineId = vehicleEngineId;
            vehicleModelStyle.VehicleTransmissionId = vehicleTransmissionId;
            vehicleModelStyle.VehicleDriveTrainId = vehicleDriveTrainId;
            vehicleModelStyle.Name = name;
            vehicleModelStyle.Description = description;
            vehicleModelStyle.Slug = slug;
            vehicleModelStyle.Seats = seats;
            vehicleModelStyle.Doors = doors;
            vehicleModelStyle.MadeIn = madeIn;
            vehicleModelStyle.Status = status;
            vehicleModelStyle.Highlight = highlight;
            vehicleModelStyle.Capacity = capacity;
            vehicleModelStyle.OperatingRange = operatingRange;
            vehicleModelStyle.CombinedKm = combinedKm;
            vehicleModelStyle.CityKm = cityKm;
            vehicleModelStyle.HighwayKm = highwayKm;
            vehicleModelStyle.BatteryType = batteryType;
            vehicleModelStyle.StandardCharging = standardCharging;
            vehicleModelStyle.RapidCharging = rapidCharging;
            vehicleModelStyle.SafetyRate = safetyRate;
            vehicleModelStyle.StandardFeature = standardFeature;
            vehicleModelStyle.TechnicalFeature = technicalFeature;

            await SetColorsAsync(vehicleModelStyle, colorIds);

            vehicleModelStyle.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleModelStyleRepository.UpdateAsync(vehicleModelStyle);
        }

        private async Task SetColorsAsync(VehicleModelStyle vehicleModelStyle, List<Guid> colorIds)
        {
            if (colorIds == null || !colorIds.Any())
            {
                vehicleModelStyle.RemoveAllColors();
                return;
            }

            var query = (await _colorRepository.GetQueryableAsync())
                .Where(x => colorIds.Contains(x.Id))
                .Select(x => x.Id);

            var colorIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!colorIdsInDb.Any())
            {
                return;
            }

            vehicleModelStyle.RemoveAllColorsExceptGivenIds(colorIdsInDb);

            foreach (var colorId in colorIdsInDb)
            {
                vehicleModelStyle.AddColor(colorId);
            }
        }

    }
}