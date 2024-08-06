using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Dto
{
    public class BookUpdateDto
    {
        public virtual string Name { get; set; }

        public virtual string AuthorName { get; set; }

        public virtual decimal Price { get; set; }

        public virtual DateTime PublishDate { get; set; }
    }
}
