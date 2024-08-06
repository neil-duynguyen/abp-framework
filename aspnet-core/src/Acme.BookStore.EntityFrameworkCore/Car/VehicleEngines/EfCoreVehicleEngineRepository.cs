using ImportSample.VehicleEngines;
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


namespace ImportSample.VehicleEngines
{
    public abstract class EfCoreVehicleEngineRepositoryBase : EfCoreRepository<BookStoreDbContext, VehicleEngine, Guid>
    {
        public EfCoreVehicleEngineRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<VehicleEngineWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(vehicleEngine => new VehicleEngineWithNavigationProperties
                {
                    VehicleEngine = vehicleEngine,
                    VehicleBrand = dbContext.Set<VehicleBrand>().FirstOrDefault(c => c.Id == vehicleEngine.VehicleBrandId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<VehicleEngineWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? label = null,
            int? horsePowerMin = null,
            int? horsePowerMax = null,
            int? torqueMin = null,
            int? torqueMax = null,
            string? slug = null,
            string? description = null,
            VehicleEngineType? type = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, label, horsePowerMin, horsePowerMax, torqueMin, torqueMax, slug, description, type, isVerified, vehicleBrandId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleEngineConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VehicleEngineWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vehicleEngine in (await GetDbSetAsync())
                   join vehicleBrand in (await GetDbContextAsync()).Set<VehicleBrand>() on vehicleEngine.VehicleBrandId equals vehicleBrand.Id into vehicleBrands
                   from vehicleBrand in vehicleBrands.DefaultIfEmpty()
                   select new VehicleEngineWithNavigationProperties
                   {
                       VehicleEngine = vehicleEngine,
                       VehicleBrand = vehicleBrand
                   };
        }

        protected virtual IQueryable<VehicleEngineWithNavigationProperties> ApplyFilter(
            IQueryable<VehicleEngineWithNavigationProperties> query,
            string? filterText,
            string? label = null,
            int? horsePowerMin = null,
            int? horsePowerMax = null,
            int? torqueMin = null,
            int? torqueMax = null,
            string? slug = null,
            string? description = null,
            VehicleEngineType? type = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.VehicleEngine.Label!.Contains(filterText!) || e.VehicleEngine.Slug!.Contains(filterText!) || e.VehicleEngine.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(label), e => e.VehicleEngine.Label.Contains(label))
                    .WhereIf(horsePowerMin.HasValue, e => e.VehicleEngine.HorsePower >= horsePowerMin!.Value)
                    .WhereIf(horsePowerMax.HasValue, e => e.VehicleEngine.HorsePower <= horsePowerMax!.Value)
                    .WhereIf(torqueMin.HasValue, e => e.VehicleEngine.Torque >= torqueMin!.Value)
                    .WhereIf(torqueMax.HasValue, e => e.VehicleEngine.Torque <= torqueMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.VehicleEngine.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.VehicleEngine.Description.Contains(description))
                    .WhereIf(type.HasValue, e => e.VehicleEngine.Type == type)
                    .WhereIf(isVerified.HasValue, e => e.VehicleEngine.IsVerified == isVerified)
                    .WhereIf(vehicleBrandId != null && vehicleBrandId != Guid.Empty, e => e.VehicleBrand != null && e.VehicleBrand.Id == vehicleBrandId);
        }

        public virtual async Task<List<VehicleEngine>> GetListAsync(
            string? filterText = null,
            string? label = null,
            int? horsePowerMin = null,
            int? horsePowerMax = null,
            int? torqueMin = null,
            int? torqueMax = null,
            string? slug = null,
            string? description = null,
            VehicleEngineType? type = null,
            bool? isVerified = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, label, horsePowerMin, horsePowerMax, torqueMin, torqueMax, slug, description, type, isVerified);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleEngineConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? label = null,
            int? horsePowerMin = null,
            int? horsePowerMax = null,
            int? torqueMin = null,
            int? torqueMax = null,
            string? slug = null,
            string? description = null,
            VehicleEngineType? type = null,
            bool? isVerified = null,
            Guid? vehicleBrandId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, label, horsePowerMin, horsePowerMax, torqueMin, torqueMax, slug, description, type, isVerified, vehicleBrandId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VehicleEngine> ApplyFilter(
            IQueryable<VehicleEngine> query,
            string? filterText = null,
            string? label = null,
            int? horsePowerMin = null,
            int? horsePowerMax = null,
            int? torqueMin = null,
            int? torqueMax = null,
            string? slug = null,
            string? description = null,
            VehicleEngineType? type = null,
            bool? isVerified = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Label!.Contains(filterText!) || e.Slug!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(label), e => e.Label.Contains(label))
                    .WhereIf(horsePowerMin.HasValue, e => e.HorsePower >= horsePowerMin!.Value)
                    .WhereIf(horsePowerMax.HasValue, e => e.HorsePower <= horsePowerMax!.Value)
                    .WhereIf(torqueMin.HasValue, e => e.Torque >= torqueMin!.Value)
                    .WhereIf(torqueMax.HasValue, e => e.Torque <= torqueMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(type.HasValue, e => e.Type == type)
                    .WhereIf(isVerified.HasValue, e => e.IsVerified == isVerified);
        }
    }
}