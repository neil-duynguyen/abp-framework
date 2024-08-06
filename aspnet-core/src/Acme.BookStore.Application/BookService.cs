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
        private readonly IMapper _mapper;


        public BookService(IRepository<Book, Guid> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookViewDto> CreateBook(BookCreateDto bookCreateDto)
        {
            var insert = await _bookRepository.InsertAsync(_mapper.Map<Book>(bookCreateDto));

            return ObjectMapper.Map<Book, BookViewDto>(insert);
        }

        public async Task<List<BookViewDto>> GetListBook()
        {
            return ObjectMapper.Map<List<Book>, List<BookViewDto>>(await _bookRepository.GetListAsync());
        }

        public async Task<BookViewDto> UpdateBook(Guid id, BookUpdateDto bookUpdateDto)
        {
            var book = _mapper.Map(bookUpdateDto, await _bookRepository.FindAsync(id));

            return _mapper.Map<BookViewDto>(await _bookRepository.UpdateAsync(book));
        }

        public async Task DeleteBook(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
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

        public async Task InitializeFirebase()
        {
            try
            {
                //để đường dẫn trong appsettings
                string pathToServiceAccountKey = "C:\\Users\\DuyNguyen\\Downloads\\testsendnoti-f5a2c-firebase-adminsdk-oyuje-410bc0077a.json";

                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(pathToServiceAccountKey),
                });

                Console.WriteLine("Firebase initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing Firebase: " + ex.Message);
            }
        }



        public async Task SendNoti(string token, string title, string mess)
        {
            await InitializeFirebase();

            var registrationToken = token;

            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = title,
                    Body = mess,
                },
                Token = registrationToken,
            };

            try
            {
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending message: " + ex.Message);
            }
        }
    }
}
