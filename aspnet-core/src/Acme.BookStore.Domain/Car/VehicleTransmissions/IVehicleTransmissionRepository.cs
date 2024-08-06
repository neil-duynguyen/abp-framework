using ImportSample.VehicleTransmissions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleTransmissions
{
    public partial interface IVehicleTransmissionRepository : IRepository<VehicleTransmission, Guid>
    {
        Task<List<VehicleTransmission>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? label = null,
            int? speedsMin = null,
            int? speedsMax = null,
            string? description = null,
            string? slug = null,
            VehicleTransmissionType? type = null,
            bool? isVerified = null,
            CancellationToken cancellationToken = default);
    }
}