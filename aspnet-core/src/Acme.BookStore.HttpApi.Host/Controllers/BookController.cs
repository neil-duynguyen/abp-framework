using Acme.BookStore.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    //[Authorize(Roles = ("manager"))]
    public class BookController : AbpController
    {
        private readonly BookService _bookService;
        public BookController(BookService bookService) {
            _bookService = bookService;
        }

        
        [HttpPost]
        public async Task<BookViewDto> CreateBook (string token, [FromBody]BookCreateDto bookCreateDto) {
            return await _bookService.CreateBook(token, bookCreateDto);
        }

        [AllowAnonymous]
        [HttpGet("books")]
        public async Task<List<BookViewDto>> GetListBook() {
            return await _bookService.GetListBook();
        }

        [HttpDelete]
        public async Task DeleteBook(string token, Guid id) {
            await _bookService.DeleteBook(token, id);
        }
        
        [HttpPut]
        public async Task<BookViewDto> UpdateBook(string token, Guid id, BookUpdateDto bookUpdateDto)
        {
            return await _bookService.UpdateBook(token, id, bookUpdateDto);
        }


        
    }
}
