using ImportSample.VehicleTransmissions;
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

namespace ImportSample.VehicleTransmissions
{
    public abstract class EfCoreVehicleTransmissionRepositoryBase : EfCoreRepository<BookStoreDbContext, VehicleTransmission, Guid>
    {
        public EfCoreVehicleTransmissionRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<VehicleTransmission>> GetListAsync(
            string? filterText = null,
            string? label = null,
            int? speedsMin = null,
            int? speedsMax = null,
            string? description = null,
            string? slug = null,
            VehicleTransmissionType? type = null,
            bool? isVerified = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, label, speedsMin, speedsMax, description, slug, type, isVerified);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleTransmissionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? label = null,
            int? speedsMin = null,
            int? speedsMax = null,
            string? description = null,
            string? slug = null,
            VehicleTransmissionType? type = null,
            bool? isVerified = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, label, speedsMin, speedsMax, description, slug, type, isVerified);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VehicleTransmission> ApplyFilter(
            IQueryable<VehicleTransmission> query,
            string? filterText = null,
            string? label = null,
            int? speedsMin = null,
            int? speedsMax = null,
            string? description = null,
            string? slug = null,
            VehicleTransmissionType? type = null,
            bool? isVerified = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Label!.Contains(filterText!) || e.Description!.Contains(filterText!) || e.Slug!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(label), e => e.Label.Contains(label))
                    .WhereIf(speedsMin.HasValue, e => e.Speeds >= speedsMin!.Value)
                    .WhereIf(speedsMax.HasValue, e => e.Speeds <= speedsMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug))
                    .WhereIf(type.HasValue, e => e.Type == type)
                    .WhereIf(isVerified.HasValue, e => e.IsVerified == isVerified);
        }
    }
}