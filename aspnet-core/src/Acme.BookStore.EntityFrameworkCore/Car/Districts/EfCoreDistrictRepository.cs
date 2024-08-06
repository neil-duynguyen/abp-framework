using ImportSample.ProvinceCities;
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

namespace ImportSample.Districts
{
    public abstract class EfCoreDistrictRepositoryBase : EfCoreRepository<BookStoreDbContext, District, Guid>
    {
        public EfCoreDistrictRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<DistrictWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(district => new DistrictWithNavigationProperties
                {
                    District = district,
                    ProvinceCity = dbContext.Set<ProvinceCity>().FirstOrDefault(c => c.Id == district.ProvinceCityId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<DistrictWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            Guid? provinceCityId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, slug, description, provinceCityId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistrictConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<DistrictWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from district in (await GetDbSetAsync())
                   join provinceCity in (await GetDbContextAsync()).Set<ProvinceCity>() on district.ProvinceCityId equals provinceCity.Id into provinceCities
                   from provinceCity in provinceCities.DefaultIfEmpty()
                   select new DistrictWithNavigationProperties
                   {
                       District = district,
                       ProvinceCity = provinceCity
                   };
        }

        protected virtual IQueryable<DistrictWithNavigationProperties> ApplyFilter(
            IQueryable<DistrictWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            string? slug = null,
            string? description = null,
            Guid? provinceCityId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.District.Name!.Contains(filterText!) || e.District.Slug!.Contains(filterText!) || e.District.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.District.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.District.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.District.Description.Contains(description))
                    .WhereIf(provinceCityId != null && provinceCityId != Guid.Empty, e => e.ProvinceCity != null && e.ProvinceCity.Id == provinceCityId);
        }

        public virtual async Task<List<District>> GetListAsync(
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
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistrictConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            Guid? provinceCityId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, slug, description, provinceCityId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<District> ApplyFilter(
            IQueryable<District> query,
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