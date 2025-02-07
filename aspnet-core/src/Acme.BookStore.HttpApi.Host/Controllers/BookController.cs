﻿using Acme.BookStore.Dto;
using DeviceDetectorNET.Parser.Device;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        [HttpPost("{token}/{deviceId}")]
        public async Task<BookViewDto> CreateBook(string token, string deviceId, [FromBody] BookCreateDto bookCreateDto) {
            return await _bookService.CreateBook(token, deviceId, bookCreateDto);
        }

        [AllowAnonymous]
        [HttpGet("books")]
        public async Task<List<BookViewDto>> GetListBook() {
            return await _bookService.GetListBook();
        }

        [AllowAnonymous]
        [HttpGet("BooksElasticsearch")]
        public async Task<IActionResult> GetListBookElastic()
        {  
            return Ok(await _bookService.GetListBookElastic());
        }

        [HttpDelete("{token}/{deviceId}/{id}")]
        public async Task DeleteBook(string token, string deviceId, Guid id) {
            await _bookService.DeleteBook(token, deviceId, id);
        }

        [HttpPut("{token}/{deviceId}/{id}")]
        public async Task<BookViewDto> UpdateBook(string token, string deviceId, Guid id, BookUpdateDto bookUpdateDto)
        {
            return await _bookService.UpdateBook(token, deviceId, id, bookUpdateDto);
        }

        [HttpPost("ImportBookFromExcel")]
        public async Task ImportBookExcel(IFormFile formFile)
        {
            await _bookService.ImportExcelBook(formFile);
        }

        [HttpGet("ExportBookExcel")]
        public async Task<IActionResult> ExportBookExcel()
        {
            var books = await _bookService.GetListBook();

            return await _bookService.ExportExcelBook(books);
             
        }
    }
}
