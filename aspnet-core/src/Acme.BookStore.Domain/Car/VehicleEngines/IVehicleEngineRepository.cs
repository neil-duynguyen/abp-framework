using ImportSample.VehicleEngines;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleEngines
{
    public partial interface IVehicleEngineRepository : IRepository<VehicleEngine, Guid>
    {
        Task<VehicleEngineWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VehicleEngineWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? label = null,
            int? horsePowerMin = null,
            int? horsePowerMax = null,
            int? torqueMin = null,
            int? torqueMax = null,
            string? slug = null,
            string? description = null,
            VehicleEngineType? type = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<VehicleEngine>> GetListAsync(
                    string? filterText = null,
                    string? label = null,
                    int? horsePowerMin = null,
                    int? horsePowerMax = null,
                    int? torqueMin = null,
                    int? torqueMax = null,
                    string? slug = null,
                    string? description = null,
                    VehicleEngineType? type = null,
                    bool? isVerified = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? label = null,
            int? horsePowerMin = null,
            int? horsePowerMax = null,
            int? torqueMin = null,
            int? torqueMax = null,
            string? slug = null,
            string? description = null,
            VehicleEngineType? type = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null,
            CancellationToken cancellationToken = default);
    }
}