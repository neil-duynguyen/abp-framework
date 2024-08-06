using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleBodyStyles
{
    public partial interface IVehicleBodyStyleRepository : IRepository<VehicleBodyStyle, Guid>
    {
        Task<List<VehicleBodyStyle>> GetListAsync(
            string? filterText = null,
            string? enName = null,
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
            string? enName = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            bool? isVerified = null,
            CancellationToken cancellationToken = default);
    }
}