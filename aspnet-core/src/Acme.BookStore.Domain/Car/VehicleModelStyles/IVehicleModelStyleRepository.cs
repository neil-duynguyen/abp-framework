using ImportSample.VehicleModelStyles;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleModelStyles
{
    public partial interface IVehicleModelStyleRepository : IRepository<VehicleModelStyle, Guid>
    {
        Task<VehicleModelStyleWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VehicleModelStyleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<VehicleModelStyle>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}