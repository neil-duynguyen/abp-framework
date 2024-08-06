using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleCategories
{
    public partial interface IVehicleCategoryRepository : IRepository<VehicleCategory, Guid>
    {
        Task<List<VehicleCategory>> GetListAsync(
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
            CancellationToken cancellationToken = default);
    }
}