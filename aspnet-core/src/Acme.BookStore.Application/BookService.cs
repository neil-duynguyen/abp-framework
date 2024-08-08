using Acme.BookStore.Dto;
using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    [RemoteService(IsEnabled = false)]
    public class BookService : ApplicationService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly SendNotificationsService _sendNotificationsService;
        private readonly IMapper _mapper;


        public BookService(IRepository<Book, Guid> bookRepository, IMapper mapper, SendNotificationsService sendNotificationsService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _sendNotificationsService = sendNotificationsService;
        }

        public async Task<BookViewDto> CreateBook(string token, string deviceId, BookCreateDto bookCreateDto)
        {
            var insert = await _bookRepository.InsertAsync(_mapper.Map<Book>(bookCreateDto));

            //send noti
            await _sendNotificationsService.SendNoti(token, deviceId, "Create Book", "Create Book Successfully");

            return ObjectMapper.Map<Book, BookViewDto>(insert);
        }

        public async Task<List<BookViewDto>> GetListBook()
        {
            return ObjectMapper.Map<List<Book>, List<BookViewDto>>(await _bookRepository.GetListAsync());
        }

        public async Task<BookViewDto> UpdateBook(string token, string deviceId, Guid id, BookUpdateDto bookUpdateDto)
        {
            var book = _mapper.Map(bookUpdateDto, await _bookRepository.FindAsync(id));

            //send noti
            await _sendNotificationsService.SendNoti(token, deviceId, "Update Book", "Update Book Successfully");     

            return _mapper.Map<BookViewDto>(await _bookRepository.UpdateAsync(book));
        }

        public async Task DeleteBook(string token, string deviceId, Guid id)
        {
            await _bookRepository.DeleteAsync(id);
            //send noti
            await _sendNotificationsService.SendNoti(token, deviceId, "Delete Book", "Delete Book Successfully");

            
        }

        public async Task ImportExcelBook(IFormFile formFile)
        {
            var listBook = new List<Book>();


            //Read Excel File 
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);

                using (var excelPackage = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var col = 1;
                        try
                        {
                            var name = worksheet.Cells[row, col].Value.ToString()!.Trim();
                            var authorName = worksheet.Cells[row, ++col].Value.ToString()!.Trim();
                            var price = worksheet.Cells[row, ++col].Value;
                            var publishDate = worksheet.Cells[row, ++col].Value;

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
        }

        
    }
}
