using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.CarDto
{
    public class VehicleModelStyleDto
    {
        public string Name { get; set; } // version

        public string Description { get; set; } // detail URL

        public string TechnicalFeature { get; set; }

        public VehicleModelStyleDto() { }

        public VehicleModelStyleDto(string name, string description, string technicalFeature)
        {
            Name = name;
            Description = description;
            TechnicalFeature = technicalFeature;
        }
    }
}
