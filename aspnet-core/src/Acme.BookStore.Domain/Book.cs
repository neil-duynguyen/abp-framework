using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore
{
    public class Book : Entity<Guid>
    {
        public string Name { get; set; }      

        public decimal Price { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
