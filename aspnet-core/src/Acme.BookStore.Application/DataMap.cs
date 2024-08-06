using Acme.BookStore.CarDto;
using CsvHelper.Configuration;
using ImportSample.VehicleBrands;
using ImportSample.VehicleCategories;
using ImportSample.VehicleYearModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore
{
    internal class DataMap
    {     
        public class DataVehicleMap : ClassMap<DataVehicle> {
            public DataVehicleMap() {
                Map(m => m.NameBrand).Name("Brand");
                Map(m => m.DescriptionBand).Name("Brand URL");

                Map(m => m.NameModel).Name("Model");
                Map(m => m.DescriptionModel).Name("Model URL");

                Map(m => m.Year).Name("Model Year");

                Map(m => m.NameCategory).Name("Category");

                Map(m => m.DescriptionEngine).Name("Engine");

                Map(m => m.NameVersion).Name("Version");
                Map(m => m.DetailURL).Name("Detail URL");
                //Map(m => m.TechnicalFeature).Name("Technical Feature");
                
            }
        }
    }
}
