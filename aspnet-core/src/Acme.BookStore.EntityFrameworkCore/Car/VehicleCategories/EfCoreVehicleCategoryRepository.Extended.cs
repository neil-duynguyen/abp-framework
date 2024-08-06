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

namespace ImportSample.VehicleCategories
{
    public class EfCoreVehicleCategoryRepository : EfCoreVehicleCategoryRepositoryBase, IVehicleCategoryRepository
    {
        public EfCoreVehicleCategoryRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}