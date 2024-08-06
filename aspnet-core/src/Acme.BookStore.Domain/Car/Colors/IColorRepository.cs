using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.Colors
{
    public partial interface IColorRepository : IRepository<Color, Guid>
    {
        Task<List<Color>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? enName = null,
            string? description = null,
            string? slug = null,
            string? code = null,
            bool? isVerified = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? enName = null,
            string? description = null,
            string? slug = null,
            string? code = null,
            bool? isVerified = null,
            CancellationToken cancellationToken = default);
    }
}