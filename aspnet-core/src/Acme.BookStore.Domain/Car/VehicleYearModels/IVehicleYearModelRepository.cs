using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleYearModels
{
    public partial interface IVehicleYearModelRepository : IRepository<VehicleYearModel, Guid>
    {
        Task<VehicleYearModelWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VehicleYearModelWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            int? yearMin = null,
            int? yearMax = null,
            bool? isVerified = null,
            Guid? vehicleModelId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<VehicleYearModel>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    string? slug = null,
                    string? description = null,
                    int? yearMin = null,
                    int? yearMax = null,
                    bool? isVerified = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            int? yearMin = null,
            int? yearMax = null,
            bool? isVerified = null,
            Guid? vehicleModelId = null,
            CancellationToken cancellationToken = default);
    }
}