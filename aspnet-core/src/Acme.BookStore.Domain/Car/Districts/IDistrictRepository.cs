using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.Districts
{
    public partial interface IDistrictRepository : IRepository<District, Guid>
    {
        Task<DistrictWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<DistrictWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            Guid? provinceCityId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<District>> GetListAsync(
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
            Guid? provinceCityId = null,
            CancellationToken cancellationToken = default);
    }
}