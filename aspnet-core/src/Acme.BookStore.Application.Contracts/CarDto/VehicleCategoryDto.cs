using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.CarDto
{
    public class VehicleCategoryDto
    {
        public string Name { get; set; }

        public VehicleCategoryDto()
        {
        }

        public VehicleCategoryDto(string name)
        {
            Name = name;
        }
    }
}
