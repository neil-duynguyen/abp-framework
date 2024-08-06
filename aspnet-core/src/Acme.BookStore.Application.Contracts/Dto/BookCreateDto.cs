using System;

namespace Acme.BookStore.Dto
{
    public class BookCreateDto
    {
        public  string Name { get; set; }

        public  string AuthorName { get; set; }

        public  decimal Price { get; set; }

        public  DateTime PublishDate { get; set; }
    }
}
