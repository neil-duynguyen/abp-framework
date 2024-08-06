using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleDriveTrains
{
    public partial interface IVehicleDriveTrainRepository : IRepository<VehicleDriveTrain, Guid>
    {
        Task<List<VehicleDriveTrain>> GetListAsync(
            string? filterText = null,
            string? enName = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            bool? isVerified = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? enName = null,
            string? name = null,
            string? slug = null,
            string? description = null,
            bool? isVerified = null,
            CancellationToken cancellationToken = default);
    }
}