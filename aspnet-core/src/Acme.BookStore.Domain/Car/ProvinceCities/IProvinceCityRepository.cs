using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.ProvinceCities
{
    public partial interface IProvinceCityRepository : IRepository<ProvinceCity, Guid>
    {
        Task<List<ProvinceCity>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
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
            CancellationToken cancellationToken = default);
    }
}