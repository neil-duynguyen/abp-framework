using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore
{
    public class DataVehicle
    {
        public string NameBrand { get; set; }
        public string DescriptionBand { get; set; }
        public string NameModel { get; set; }
        public string DescriptionModel { get; set; }
        public string Year { get; set; }
        public string NameCategory { get; set; }
        public string DescriptionEngine { get; set; }

        public string NameVersion { get; set; } // version

        public string DetailURL { get; set; } // detail URL
        public string TechnicalFeature { get; set; }
    }
}
