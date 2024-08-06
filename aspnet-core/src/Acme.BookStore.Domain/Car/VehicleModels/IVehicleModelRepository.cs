using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleModels
{
    public partial interface IVehicleModelRepository : IRepository<VehicleModel, Guid>
    {
        Task<VehicleModelWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VehicleModelWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<VehicleModel>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    string? description = null,
                    string? slug = null,
                    bool? isVerified = null,
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
            bool? isVerified = null,
            Guid? vehicleBrandId = null,
            CancellationToken cancellationToken = default);
    }
}