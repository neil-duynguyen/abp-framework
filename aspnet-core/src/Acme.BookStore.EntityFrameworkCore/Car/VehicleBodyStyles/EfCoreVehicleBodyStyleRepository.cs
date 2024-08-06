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

namespace ImportSample.VehicleBodyStyles
{
    public abstract class EfCoreVehicleBodyStyleRepositoryBase : EfCoreRepository<BookStoreDbContext, VehicleBodyStyle, Guid>
    {
        public EfCoreVehicleBodyStyleRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<VehicleBodyStyle>> GetListAsync(
            string? filterText = null,
            string? enName = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            bool? isVerified = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, enName, name, slug, description, isVerified);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleBodyStyleConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? enName = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            bool? isVerified = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, enName, name, slug, description, isVerified);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VehicleBodyStyle> ApplyFilter(
            IQueryable<VehicleBodyStyle> query,
            string? filterText = null,
            string? enName = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            bool? isVerified = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EnName!.Contains(filterText!) || e.Name!.Contains(filterText!) || e.Slug!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(enName), e => e.EnName.Contains(enName))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(isVerified.HasValue, e => e.IsVerified == isVerified);
        }
    }
}