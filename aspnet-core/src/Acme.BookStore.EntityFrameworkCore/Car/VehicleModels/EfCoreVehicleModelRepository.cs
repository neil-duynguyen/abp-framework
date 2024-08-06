using ImportSample.VehicleBrands;
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


namespace ImportSample.VehicleModels
{
    public abstract class EfCoreVehicleModelRepositoryBase : EfCoreRepository<BookStoreDbContext, VehicleModel, Guid>
    {
        public EfCoreVehicleModelRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<VehicleModelWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(vehicleModel => new VehicleModelWithNavigationProperties
                {
                    VehicleModel = vehicleModel,
                    VehicleBrand = dbContext.Set<VehicleBrand>().FirstOrDefault(c => c.Id == vehicleModel.VehicleBrandId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<VehicleModelWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, description, slug, isVerified, vehicleBrandId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleModelConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VehicleModelWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vehicleModel in (await GetDbSetAsync())
                   join vehicleBrand in (await GetDbContextAsync()).Set<VehicleBrand>() on vehicleModel.VehicleBrandId equals vehicleBrand.Id into vehicleBrands
                   from vehicleBrand in vehicleBrands.DefaultIfEmpty()
                   select new VehicleModelWithNavigationProperties
                   {
                       VehicleModel = vehicleModel,
                       VehicleBrand = vehicleBrand
                   };
        }

        protected virtual IQueryable<VehicleModelWithNavigationProperties> ApplyFilter(
            IQueryable<VehicleModelWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            string? description = null,
            string? slug = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VehicleModel.Name!.Contains(filterText!) || e.VehicleModel.Description!.Contains(filterText!) || e.VehicleModel.Slug!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.VehicleModel.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.VehicleModel.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.VehicleModel.Slug.Contains(slug))
                    .WhereIf(isVerified.HasValue, e => e.VehicleModel.IsVerified == isVerified)
                    .WhereIf(vehicleBrandId != null && vehicleBrandId != Guid.Empty, e => e.VehicleBrand != null && e.VehicleBrand.Id == vehicleBrandId);
        }

        public virtual async Task<List<VehicleModel>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            bool? isVerified = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, description, slug, isVerified);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleModelConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, description, slug, isVerified, vehicleBrandId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VehicleModel> ApplyFilter(
            IQueryable<VehicleModel> query,
            string? filterText = null,
            string? name = null,
            string? description = null,
            string? slug = null,
            bool? isVerified = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Description!.Contains(filterText!) || e.Slug!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug))
                    .WhereIf(isVerified.HasValue, e => e.IsVerified == isVerified);
        }
    }
}