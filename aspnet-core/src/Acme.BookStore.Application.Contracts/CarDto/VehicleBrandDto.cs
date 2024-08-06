using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.CarDto
{
    public class VehicleBrandDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public VehicleBrandDto() { }

        public VehicleBrandDto(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
