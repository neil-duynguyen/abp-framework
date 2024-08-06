using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.ColorTranslations
{
    public partial interface IColorTranslationRepository : IRepository<ColorTranslation, Guid>
    {
        Task<ColorTranslationWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ColorTranslationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? language = null,
            string? name = null,
            Guid? colorId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ColorTranslation>> GetListAsync(
                    string? filterText = null,
                    string? language = null,
                    string? name = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? language = null,
            string? name = null,
            Guid? colorId = null,
            CancellationToken cancellationToken = default);
    }
}