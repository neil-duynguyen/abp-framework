using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Acme.BookStore.EntityFrameworkCore;

namespace ImportSample.ProvinceCities
{
    public abstract class EfCoreProvinceCityRepositoryBase : EfCoreRepository<BookStoreDbContext, ProvinceCity, Guid>
    {
        public EfCoreProvinceCityRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<ProvinceCity>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, slug, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProvinceCityConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, slug, description);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ProvinceCity> ApplyFilter(
            IQueryable<ProvinceCity> query,
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Slug!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}