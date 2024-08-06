using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.CarDto
{
    public class VehicleModelDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid VehicleBrandId { get; set; }

        public VehicleModelDto() { }
        public VehicleModelDto(string name, string description, Guid vehicleBrandId)
        {
            Name = name;
            Description = description;
            VehicleBrandId = vehicleBrandId;
        }
    }
}
