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

namespace ImportSample.VehicleDriveTrains
{
    public class EfCoreVehicleDriveTrainRepository : EfCoreVehicleDriveTrainRepositoryBase, IVehicleDriveTrainRepository
    {
        public EfCoreVehicleDriveTrainRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}