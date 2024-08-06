using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.CarDto
{
    public class VehicleEngineDto
    {
        public virtual string Description { get; set; }
        public Guid? VehicleBrandId { get; set; }

        public VehicleEngineDto()
        {
        }

        public VehicleEngineDto(string description, Guid? vehicleBrandId)
        {
            Description = description;
            VehicleBrandId = vehicleBrandId;
        }
    }
}
