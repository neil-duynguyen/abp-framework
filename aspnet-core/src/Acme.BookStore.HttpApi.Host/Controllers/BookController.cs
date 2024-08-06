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
        public async Task<BookViewDto> CreateBook ( [FromBody]BookCreateDto bookCreateDto) {
            return await _bookService.CreateBook(bookCreateDto);
        }

        [AllowAnonymous]
        [HttpGet("books")]
        public async Task<List<BookViewDto>> GetListBook() {
            return await _bookService.GetListBook();
        }

        [HttpDelete]
        public async Task DeleteBook(Guid id) {
            await _bookService.DeleteBook(id);
        }
        
        [HttpPut]
        public async Task<BookViewDto> UpdateBook(Guid id, BookUpdateDto bookUpdateDto)
        {
            return await _bookService.UpdateBook(id, bookUpdateDto);
        }


        [HttpPost("SendNoti")]
        public async Task SendNoti(string token, string title, string mess)
        {
            await _bookService.SendNoti(token, title, mess);
        }
    }
}
