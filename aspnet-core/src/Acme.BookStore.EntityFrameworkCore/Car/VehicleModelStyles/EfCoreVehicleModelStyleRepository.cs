using ImportSample.VehicleModelStyles;
using ImportSample.VehicleDriveTrains;
using ImportSample.VehicleTransmissions;
using ImportSample.VehicleEngines;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleCategories;
using ImportSample.VehicleYearModels;
using ImportSample.Colors;
using ImportSample.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Acme.BookStore.EntityFrameworkCore;

namespace ImportSample.VehicleModelStyles
{
    public abstract class EfCoreVehicleModelStyleRepositoryBase : EfCoreRepository<BookStoreDbContext, VehicleModelStyle, Guid>
    {
        public EfCoreVehicleModelStyleRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<VehicleModelStyleWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.Colors)
                .Select(vehicleModelStyle => new VehicleModelStyleWithNavigationProperties
                {
                    VehicleModelStyle = vehicleModelStyle,
                    VehicleYearModel = dbContext.Set<VehicleYearModel>().FirstOrDefault(c => c.Id == vehicleModelStyle.VehicleYearModelId),
                    VehicleCategory = dbContext.Set<VehicleCategory>().FirstOrDefault(c => c.Id == vehicleModelStyle.VehicleCategoryId),
                    VehicleBodyStyle = dbContext.Set<VehicleBodyStyle>().FirstOrDefault(c => c.Id == vehicleModelStyle.VehicleBodyStyleId),
                    VehicleEngine = dbContext.Set<VehicleEngine>().FirstOrDefault(c => c.Id == vehicleModelStyle.VehicleEngineId),
                    VehicleTransmission = dbContext.Set<VehicleTransmission>().FirstOrDefault(c => c.Id == vehicleModelStyle.VehicleTransmissionId),
                    VehicleDriveTrain = dbContext.Set<VehicleDriveTrain>().FirstOrDefault(c => c.Id == vehicleModelStyle.VehicleDriveTrainId),
                    Colors = (from vehicleModelStyleColors in vehicleModelStyle.Colors
                              join _color in dbContext.Set<Color>() on vehicleModelStyleColors.ColorId equals _color.Id
                              select _color).ToList()
                }).FirstOrDefault();
        }

        public virtual async Task<List<VehicleModelStyleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            string? highlight = null,
            float? capacityMin = null,
            float? capacityMax = null,
            float? operatingRangeMin = null,
            float? operatingRangeMax = null,
            float? combinedKmMin = null,
            float? combinedKmMax = null,
            float? cityKmMin = null,
            float? cityKmMax = null,
            float? highwayKmMin = null,
            float? highwayKmMax = null,
            string? batteryType = null,
            float? standardChargingMin = null,
            float? standardChargingMax = null,
            float? rapidChargingMin = null,
            float? rapidChargingMax = null,
            int? safetyRateMin = null,
            int? safetyRateMax = null,
            int? seatsMin = null,
            int? seatsMax = null,
            int? doorsMin = null,
            int? doorsMax = null,
            string? standardFeature = null,
            string? technicalFeature = null,
            string? madeIn = null,
            VehicleModelStyleStatus? status = null,
            Guid? vehicleYearModelId = null,
            Guid? vehicleCategoryId = null,
            Guid? vehicleBodyStyleId = null,
            Guid? vehicleEngineId = null,
            Guid? vehicleTransmissionId = null,
            Guid? vehicleDriveTrainId = null,
            Guid? colorId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, description, slug, highlight, capacityMin, capacityMax, operatingRangeMin, operatingRangeMax, combinedKmMin, combinedKmMax, cityKmMin, cityKmMax, highwayKmMin, highwayKmMax, batteryType, standardChargingMin, standardChargingMax, rapidChargingMin, rapidChargingMax, safetyRateMin, safetyRateMax, seatsMin, seatsMax, doorsMin, doorsMax, standardFeature, technicalFeature, madeIn, status, vehicleYearModelId, vehicleCategoryId, vehicleBodyStyleId, vehicleEngineId, vehicleTransmissionId, vehicleDriveTrainId, colorId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleModelStyleConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VehicleModelStyleWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vehicleModelStyle in (await GetDbSetAsync())
                   join vehicleYearModel in (await GetDbContextAsync()).Set<VehicleYearModel>() on vehicleModelStyle.VehicleYearModelId equals vehicleYearModel.Id into vehicleYearModels
                   from vehicleYearModel in vehicleYearModels.DefaultIfEmpty()
                   join vehicleCategory in (await GetDbContextAsync()).Set<VehicleCategory>() on vehicleModelStyle.VehicleCategoryId equals vehicleCategory.Id into vehicleCategories
                   from vehicleCategory in vehicleCategories.DefaultIfEmpty()
                   join vehicleBodyStyle in (await GetDbContextAsync()).Set<VehicleBodyStyle>() on vehicleModelStyle.VehicleBodyStyleId equals vehicleBodyStyle.Id into vehicleBodyStyles
                   from vehicleBodyStyle in vehicleBodyStyles.DefaultIfEmpty()
                   join vehicleEngine in (await GetDbContextAsync()).Set<VehicleEngine>() on vehicleModelStyle.VehicleEngineId equals vehicleEngine.Id into vehicleEngines
                   from vehicleEngine in vehicleEngines.DefaultIfEmpty()
                   join vehicleTransmission in (await GetDbContextAsync()).Set<VehicleTransmission>() on vehicleModelStyle.VehicleTransmissionId equals vehicleTransmission.Id into vehicleTransmissions
                   from vehicleTransmission in vehicleTransmissions.DefaultIfEmpty()
                   join vehicleDriveTrain in (await GetDbContextAsync()).Set<VehicleDriveTrain>() on vehicleModelStyle.VehicleDriveTrainId equals vehicleDriveTrain.Id into vehicleDriveTrains
                   from vehicleDriveTrain in vehicleDriveTrains.DefaultIfEmpty()
                   select new VehicleModelStyleWithNavigationProperties
                   {
                       VehicleModelStyle = vehicleModelStyle,
                       VehicleYearModel = vehicleYearModel,
                       VehicleCategory = vehicleCategory,
                       VehicleBodyStyle = vehicleBodyStyle,
                       VehicleEngine = vehicleEngine,
                       VehicleTransmission = vehicleTransmission,
                       VehicleDriveTrain = vehicleDriveTrain,
                       Colors = new List<Color>()
                   };
        }

        protected virtual IQueryable<VehicleModelStyleWithNavigationProperties> ApplyFilter(
            IQueryable<VehicleModelStyleWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            string? description = null,
            string? slug = null,
            string? highlight = null,
            float? capacityMin = null,
            float? capacityMax = null,
            float? operatingRangeMin = null,
            float? operatingRangeMax = null,
            float? combinedKmMin = null,
            float? combinedKmMax = null,
            float? cityKmMin = null,
            float? cityKmMax = null,
            float? highwayKmMin = null,
            float? highwayKmMax = null,
            string? batteryType = null,
            float? standardChargingMin = null,
            float? standardChargingMax = null,
            float? rapidChargingMin = null,
            float? rapidChargingMax = null,
            int? safetyRateMin = null,
            int? safetyRateMax = null,
            int? seatsMin = null,
            int? seatsMax = null,
            int? doorsMin = null,
            int? doorsMax = null,
            string? standardFeature = null,
            string? technicalFeature = null,
            string? madeIn = null,
            VehicleModelStyleStatus? status = null,
            Guid? vehicleYearModelId = null,
            Guid? vehicleCategoryId = null,
            Guid? vehicleBodyStyleId = null,
            Guid? vehicleEngineId = null,
            Guid? vehicleTransmissionId = null,
            Guid? vehicleDriveTrainId = null,
            Guid? colorId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VehicleModelStyle.Name!.Contains(filterText!) || e.VehicleModelStyle.Description!.Contains(filterText!) || e.VehicleModelStyle.Slug!.Contains(filterText!) || e.VehicleModelStyle.Highlight!.Contains(filterText!) || e.VehicleModelStyle.BatteryType!.Contains(filterText!) || e.VehicleModelStyle.StandardFeature!.Contains(filterText!) || e.VehicleModelStyle.TechnicalFeature!.Contains(filterText!) || e.VehicleModelStyle.MadeIn!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.VehicleModelStyle.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.VehicleModelStyle.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.VehicleModelStyle.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(highlight), e => e.VehicleModelStyle.Highlight.Contains(highlight))
                    .WhereIf(capacityMin.HasValue, e => e.VehicleModelStyle.Capacity >= capacityMin!.Value)
                    .WhereIf(capacityMax.HasValue, e => e.VehicleModelStyle.Capacity <= capacityMax!.Value)
                    .WhereIf(operatingRangeMin.HasValue, e => e.VehicleModelStyle.OperatingRange >= operatingRangeMin!.Value)
                    .WhereIf(operatingRangeMax.HasValue, e => e.VehicleModelStyle.OperatingRange <= operatingRangeMax!.Value)
                    .WhereIf(combinedKmMin.HasValue, e => e.VehicleModelStyle.CombinedKm >= combinedKmMin!.Value)
                    .WhereIf(combinedKmMax.HasValue, e => e.VehicleModelStyle.CombinedKm <= combinedKmMax!.Value)
                    .WhereIf(cityKmMin.HasValue, e => e.VehicleModelStyle.CityKm >= cityKmMin!.Value)
                    .WhereIf(cityKmMax.HasValue, e => e.VehicleModelStyle.CityKm <= cityKmMax!.Value)
                    .WhereIf(highwayKmMin.HasValue, e => e.VehicleModelStyle.HighwayKm >= highwayKmMin!.Value)
                    .WhereIf(highwayKmMax.HasValue, e => e.VehicleModelStyle.HighwayKm <= highwayKmMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(batteryType), e => e.VehicleModelStyle.BatteryType.Contains(batteryType))
                    .WhereIf(standardChargingMin.HasValue, e => e.VehicleModelStyle.StandardCharging >= standardChargingMin!.Value)
                    .WhereIf(standardChargingMax.HasValue, e => e.VehicleModelStyle.StandardCharging <= standardChargingMax!.Value)
                    .WhereIf(rapidChargingMin.HasValue, e => e.VehicleModelStyle.RapidCharging >= rapidChargingMin!.Value)
                    .WhereIf(rapidChargingMax.HasValue, e => e.VehicleModelStyle.RapidCharging <= rapidChargingMax!.Value)
                    .WhereIf(safetyRateMin.HasValue, e => e.VehicleModelStyle.SafetyRate >= safetyRateMin!.Value)
                    .WhereIf(safetyRateMax.HasValue, e => e.VehicleModelStyle.SafetyRate <= safetyRateMax!.Value)
                    .WhereIf(seatsMin.HasValue, e => e.VehicleModelStyle.Seats >= seatsMin!.Value)
                    .WhereIf(seatsMax.HasValue, e => e.VehicleModelStyle.Seats <= seatsMax!.Value)
                    .WhereIf(doorsMin.HasValue, e => e.VehicleModelStyle.Doors >= doorsMin!.Value)
                    .WhereIf(doorsMax.HasValue, e => e.VehicleModelStyle.Doors <= doorsMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(standardFeature), e => e.VehicleModelStyle.StandardFeature.Contains(standardFeature))
                    .WhereIf(!string.IsNullOrWhiteSpace(technicalFeature), e => e.VehicleModelStyle.TechnicalFeature.Contains(technicalFeature))
                    .WhereIf(!string.IsNullOrWhiteSpace(madeIn), e => e.VehicleModelStyle.MadeIn.Contains(madeIn))
                    .WhereIf(status.HasValue, e => e.VehicleModelStyle.Status == status)
                    .WhereIf(vehicleYearModelId != null && vehicleYearModelId != Guid.Empty, e => e.VehicleYearModel != null && e.VehicleYearModel.Id == vehicleYearModelId)
                    .WhereIf(vehicleCategoryId != null && vehicleCategoryId != Guid.Empty, e => e.VehicleCategory != null && e.VehicleCategory.Id == vehicleCategoryId)
                    .WhereIf(vehicleBodyStyleId != null && vehicleBodyStyleId != Guid.Empty, e => e.VehicleBodyStyle != null && e.VehicleBodyStyle.Id == vehicleBodyStyleId)
                    .WhereIf(vehicleEngineId != null && vehicleEngineId != Guid.Empty, e => e.VehicleEngine != null && e.VehicleEngine.Id == vehicleEngineId)
                    .WhereIf(vehicleTransmissionId != null && vehicleTransmissionId != Guid.Empty, e => e.VehicleTransmission != null && e.VehicleTransmission.Id == vehicleTransmissionId)
                    .WhereIf(vehicleDriveTrainId != null && vehicleDriveTrainId != Guid.Empty, e => e.VehicleDriveTrain != null && e.VehicleDriveTrain.Id == vehicleDriveTrainId)
                    .WhereIf(colorId != null && colorId != Guid.Empty, e => e.VehicleModelStyle.Colors.Any(x => x.ColorId == colorId));
        }

        public virtual async Task<List<VehicleModelStyle>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            string? highlight = null,
            float? capacityMin = null,
            float? capacityMax = null,
            float? operatingRangeMin = null,
            float? operatingRangeMax = null,
            float? combinedKmMin = null,
            float? combinedKmMax = null,
            float? cityKmMin = null,
            float? cityKmMax = null,
            float? highwayKmMin = null,
            float? highwayKmMax = null,
            string? batteryType = null,
            float? standardChargingMin = null,
            float? standardChargingMax = null,
            float? rapidChargingMin = null,
            float? rapidChargingMax = null,
            int? safetyRateMin = null,
            int? safetyRateMax = null,
            int? seatsMin = null,
            int? seatsMax = null,
            int? doorsMin = null,
            int? doorsMax = null,
            string? standardFeature = null,
            string? technicalFeature = null,
            string? madeIn = null,
            VehicleModelStyleStatus? status = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, description, slug, highlight, capacityMin, capacityMax, operatingRangeMin, operatingRangeMax, combinedKmMin, combinedKmMax, cityKmMin, cityKmMax, highwayKmMin, highwayKmMax, batteryType, standardChargingMin, standardChargingMax, rapidChargingMin, rapidChargingMax, safetyRateMin, safetyRateMax, seatsMin, seatsMax, doorsMin, doorsMax, standardFeature, technicalFeature, madeIn, status);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleModelStyleConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            string? highlight = null,
            float? capacityMin = null,
            float? capacityMax = null,
            float? operatingRangeMin = null,
            float? operatingRangeMax = null,
            float? combinedKmMin = null,
            float? combinedKmMax = null,
            float? cityKmMin = null,
            float? cityKmMax = null,
            float? highwayKmMin = null,
            float? highwayKmMax = null,
            string? batteryType = null,
            float? standardChargingMin = null,
            float? standardChargingMax = null,
            float? rapidChargingMin = null,
            float? rapidChargingMax = null,
            int? safetyRateMin = null,
            int? safetyRateMax = null,
            int? seatsMin = null,
            int? seatsMax = null,
            int? doorsMin = null,
            int? doorsMax = null,
            string? standardFeature = null,
            string? technicalFeature = null,
            string? madeIn = null,
            VehicleModelStyleStatus? status = null,
            Guid? vehicleYearModelId = null,
            Guid? vehicleCategoryId = null,
            Guid? vehicleBodyStyleId = null,
            Guid? vehicleEngineId = null,
            Guid? vehicleTransmissionId = null,
            Guid? vehicleDriveTrainId = null,
            Guid? colorId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, description, slug, highlight, capacityMin, capacityMax, operatingRangeMin, operatingRangeMax, combinedKmMin, combinedKmMax, cityKmMin, cityKmMax, highwayKmMin, highwayKmMax, batteryType, standardChargingMin, standardChargingMax, rapidChargingMin, rapidChargingMax, safetyRateMin, safetyRateMax, seatsMin, seatsMax, doorsMin, doorsMax, standardFeature, technicalFeature, madeIn, status, vehicleYearModelId, vehicleCategoryId, vehicleBodyStyleId, vehicleEngineId, vehicleTransmissionId, vehicleDriveTrainId, colorId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VehicleModelStyle> ApplyFilter(
            IQueryable<VehicleModelStyle> query,
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            string? highlight = null,
            float? capacityMin = null,
            float? capacityMax = null,
            float? operatingRangeMin = null,
            float? operatingRangeMax = null,
            float? combinedKmMin = null,
            float? combinedKmMax = null,
            float? cityKmMin = null,
            float? cityKmMax = null,
            float? highwayKmMin = null,
            float? highwayKmMax = null,
            string? batteryType = null,
            float? standardChargingMin = null,
            float? standardChargingMax = null,
            float? rapidChargingMin = null,
            float? rapidChargingMax = null,
            int? safetyRateMin = null,
            int? safetyRateMax = null,
            int? seatsMin = null,
            int? seatsMax = null,
            int? doorsMin = null,
            int? doorsMax = null,
            string? standardFeature = null,
            string? technicalFeature = null,
            string? madeIn = null,
            VehicleModelStyleStatus? status = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Description!.Contains(filterText!) || e.Slug!.Contains(filterText!) || e.Highlight!.Contains(filterText!) || e.BatteryType!.Contains(filterText!) || e.StandardFeature!.Contains(filterText!) || e.TechnicalFeature!.Contains(filterText!) || e.MadeIn!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(highlight), e => e.Highlight.Contains(highlight))
                    .WhereIf(capacityMin.HasValue, e => e.Capacity >= capacityMin!.Value)
                    .WhereIf(capacityMax.HasValue, e => e.Capacity <= capacityMax!.Value)
                    .WhereIf(operatingRangeMin.HasValue, e => e.OperatingRange >= operatingRangeMin!.Value)
                    .WhereIf(operatingRangeMax.HasValue, e => e.OperatingRange <= operatingRangeMax!.Value)
                    .WhereIf(combinedKmMin.HasValue, e => e.CombinedKm >= combinedKmMin!.Value)
                    .WhereIf(combinedKmMax.HasValue, e => e.CombinedKm <= combinedKmMax!.Value)
                    .WhereIf(cityKmMin.HasValue, e => e.CityKm >= cityKmMin!.Value)
                    .WhereIf(cityKmMax.HasValue, e => e.CityKm <= cityKmMax!.Value)
                    .WhereIf(highwayKmMin.HasValue, e => e.HighwayKm >= highwayKmMin!.Value)
                    .WhereIf(highwayKmMax.HasValue, e => e.HighwayKm <= highwayKmMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(batteryType), e => e.BatteryType.Contains(batteryType))
                    .WhereIf(standardChargingMin.HasValue, e => e.StandardCharging >= standardChargingMin!.Value)
                    .WhereIf(standardChargingMax.HasValue, e => e.StandardCharging <= standardChargingMax!.Value)
                    .WhereIf(rapidChargingMin.HasValue, e => e.RapidCharging >= rapidChargingMin!.Value)
                    .WhereIf(rapidChargingMax.HasValue, e => e.RapidCharging <= rapidChargingMax!.Value)
                    .WhereIf(safetyRateMin.HasValue, e => e.SafetyRate >= safetyRateMin!.Value)
                    .WhereIf(safetyRateMax.HasValue, e => e.SafetyRate <= safetyRateMax!.Value)
                    .WhereIf(seatsMin.HasValue, e => e.Seats >= seatsMin!.Value)
                    .WhereIf(seatsMax.HasValue, e => e.Seats <= seatsMax!.Value)
                    .WhereIf(doorsMin.HasValue, e => e.Doors >= doorsMin!.Value)
                    .WhereIf(doorsMax.HasValue, e => e.Doors <= doorsMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(standardFeature), e => e.StandardFeature.Contains(standardFeature))
                    .WhereIf(!string.IsNullOrWhiteSpace(technicalFeature), e => e.TechnicalFeature.Contains(technicalFeature))
                    .WhereIf(!string.IsNullOrWhiteSpace(madeIn), e => e.MadeIn.Contains(madeIn))
                    .WhereIf(status.HasValue, e => e.Status == status);
        }
    }
}