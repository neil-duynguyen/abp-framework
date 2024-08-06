using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleBrands
{
    public partial interface IVehicleBrandRepository : IRepository<VehicleBrand, Guid>
    {
        Task<List<VehicleBrand>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
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
            bool? isVerified = null,
            CancellationToken cancellationToken = default);
    }
}