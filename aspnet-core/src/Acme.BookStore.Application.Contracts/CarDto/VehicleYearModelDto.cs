using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.CarDto
{
    public class VehicleYearModelDto
    {
        public virtual int Year { get; set; }
        public Guid VehicleModelId { get; set; }

        public VehicleYearModelDto()
        {
        }

        public VehicleYearModelDto(int year, Guid vehicleModelId)
        {
            Year = year;
            VehicleModelId = vehicleModelId;
        }
    }
}
