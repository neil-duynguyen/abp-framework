using ImportSample.VehicleModels;
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
namespace ImportSample.VehicleYearModels
{
    public abstract class EfCoreVehicleYearModelRepositoryBase : EfCoreRepository<BookStoreDbContext, VehicleYearModel, Guid>
    {
        public EfCoreVehicleYearModelRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<VehicleYearModelWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(vehicleYearModel => new VehicleYearModelWithNavigationProperties
                {
                    VehicleYearModel = vehicleYearModel,
                    VehicleModel = dbContext.Set<VehicleModel>().FirstOrDefault(c => c.Id == vehicleYearModel.VehicleModelId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<VehicleYearModelWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            int? yearMin = null,
            int? yearMax = null,
            bool? isVerified = null,
            Guid? vehicleModelId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, slug, description, yearMin, yearMax, isVerified, vehicleModelId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleYearModelConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VehicleYearModelWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vehicleYearModel in (await GetDbSetAsync())
                   join vehicleModel in (await GetDbContextAsync()).Set<VehicleModel>() on vehicleYearModel.VehicleModelId equals vehicleModel.Id into vehicleModels
                   from vehicleModel in vehicleModels.DefaultIfEmpty()
                   select new VehicleYearModelWithNavigationProperties
                   {
                       VehicleYearModel = vehicleYearModel,
                       VehicleModel = vehicleModel
                   };
        }

        protected virtual IQueryable<VehicleYearModelWithNavigationProperties> ApplyFilter(
            IQueryable<VehicleYearModelWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            string? slug = null,
            string? description = null,
            int? yearMin = null,
            int? yearMax = null,
            bool? isVerified = null,
            Guid? vehicleModelId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VehicleYearModel.Name!.Contains(filterText!) || e.VehicleYearModel.Slug!.Contains(filterText!) || e.VehicleYearModel.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.VehicleYearModel.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.VehicleYearModel.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.VehicleYearModel.Description.Contains(description))
                    .WhereIf(yearMin.HasValue, e => e.VehicleYearModel.Year >= yearMin!.Value)
                    .WhereIf(yearMax.HasValue, e => e.VehicleYearModel.Year <= yearMax!.Value)
                    .WhereIf(isVerified.HasValue, e => e.VehicleYearModel.IsVerified == isVerified)
                    .WhereIf(vehicleModelId != null && vehicleModelId != Guid.Empty, e => e.VehicleModel != null && e.VehicleModel.Id == vehicleModelId);
        }

        public virtual async Task<List<VehicleYearModel>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            int? yearMin = null,
            int? yearMax = null,
            bool? isVerified = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, slug, description, yearMin, yearMax, isVerified);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleYearModelConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            int? yearMin = null,
            int? yearMax = null,
            bool? isVerified = null,
            Guid? vehicleModelId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, slug, description, yearMin, yearMax, isVerified, vehicleModelId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VehicleYearModel> ApplyFilter(
            IQueryable<VehicleYearModel> query,
            string? filterText = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            int? yearMin = null,
            int? yearMax = null,
            bool? isVerified = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Slug!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(yearMin.HasValue, e => e.Year >= yearMin!.Value)
                    .WhereIf(yearMax.HasValue, e => e.Year <= yearMax!.Value)
                    .WhereIf(isVerified.HasValue, e => e.IsVerified == isVerified);
        }
    }
}