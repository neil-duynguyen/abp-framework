using ImportSample.Districts;
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
namespace ImportSample.Wards
{
    public abstract class EfCoreWardRepositoryBase : EfCoreRepository<BookStoreDbContext, Ward, Guid>
    {
        public EfCoreWardRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<WardWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(ward => new WardWithNavigationProperties
                {
                    Ward = ward,
                    District = dbContext.Set<District>().FirstOrDefault(c => c.Id == ward.DistrictId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<WardWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            Guid? districtId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, description, slug, districtId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WardConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<WardWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from ward in (await GetDbSetAsync())
                   join district in (await GetDbContextAsync()).Set<District>() on ward.DistrictId equals district.Id into districts
                   from district in districts.DefaultIfEmpty()
                   select new WardWithNavigationProperties
                   {
                       Ward = ward,
                       District = district
                   };
        }

        protected virtual IQueryable<WardWithNavigationProperties> ApplyFilter(
            IQueryable<WardWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            string? description = null,
            string? slug = null,
            Guid? districtId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Ward.Name!.Contains(filterText!) || e.Ward.Description!.Contains(filterText!) || e.Ward.Slug!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Ward.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Ward.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Ward.Slug.Contains(slug))
                    .WhereIf(districtId != null && districtId != Guid.Empty, e => e.District != null && e.District.Id == districtId);
        }

        public virtual async Task<List<Ward>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, description, slug);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WardConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            Guid? districtId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, description, slug, districtId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Ward> ApplyFilter(
            IQueryable<Ward> query,
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Description!.Contains(filterText!) || e.Slug!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug));
        }
    }
}