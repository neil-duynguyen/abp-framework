﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Dto
{
    public class BookUpdateDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
