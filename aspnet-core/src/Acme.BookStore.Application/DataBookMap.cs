using Acme.BookStore.Dto;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore
{
    public class DataBookMap : ClassMap<BookCreateDto>
    {
        public DataBookMap()
        {
            Map(x => x.Name).Name("Name");
            Map(x => x.Price).Name("Price");
            Map(x => x.PublishDate).Name("PublishDate");
        }
    }
}
