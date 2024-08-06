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

namespace ImportSample.Colors
{
    public abstract class EfCoreColorRepositoryBase : EfCoreRepository<BookStoreDbContext, Color, Guid>
    {
        public EfCoreColorRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<Color>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, enName, description, slug, code, isVerified);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ColorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? enName = null,
            string? description = null,
            string? slug = null,
            string? code = null,
            bool? isVerified = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, enName, description, slug, code, isVerified);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Color> ApplyFilter(
            IQueryable<Color> query,
            string? filterText = null,
            string? name = null,
            string? enName = null,
            string? description = null,
            string? slug = null,
            string? code = null,
            bool? isVerified = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.EnName!.Contains(filterText!) || e.Description!.Contains(filterText!) || e.Slug!.Contains(filterText!) || e.Code!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(enName), e => e.EnName.Contains(enName))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(isVerified.HasValue, e => e.IsVerified == isVerified);
        }
    }
}