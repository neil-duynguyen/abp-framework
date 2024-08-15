using Acme.BookStore.Dto;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using OfficeOpenXml;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static ServiceStack.Diagnostics.Events;
using static System.Reflection.Metadata.BlobBuilder;

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

        public async Task<BookViewDto> CreateBook(string? token, string? deviceId, BookCreateDto bookCreateDto)
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

        public async Task<List<BookViewDto>> GetListBookElastic()
        {

            /*  var searchResponse = ElasticsearchService.Client.Search<object>(s => s.Index("book_store").Query(q => q.MatchAll()));

              foreach (var hit in searchResponse.Hits)
              {
                  var source = hit.Source;
                  var document = source as IDictionary<string, object>;
                  Console.WriteLine($"Name: {document["Name"]}");
                  Console.WriteLine($"Price: {document["Price"]}");
                  Console.WriteLine($"Publish Date: {document["PublishDate"]}");
                  Console.WriteLine();
              }*/

            var searchResponse = ElasticsearchService.Client.Search<object>(s => s.Index("book_store").Query(q => q.MatchAll()));

            return searchResponse.Documents.Select(doc => _mapper.Map<BookViewDto>(doc)).ToList();

        }

        public async Task<BookViewDto> UpdateBook(string token, string deviceId, Guid id, BookUpdateDto bookUpdateDto)
        {
            var book = _mapper.Map(bookUpdateDto, await _bookRepository.FindAsync(id)) ?? throw new Exception("Not found book.");

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
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header,
                ShouldSkipRecord = x => x.Row.Parser.Record.All(string.IsNullOrWhiteSpace),
                AllowComments = true,
                TrimOptions = TrimOptions.Trim
            };

            try
            {
                using (var stream = formFile.OpenReadStream())
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                using (var csv = new CsvHelper.CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<DataBookMap>();

                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        var dataBook = csv.GetRecord<BookCreateDto>();

                        await CreateBook(null, null, dataBook);

                    }

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public async Task<FileContentResult> ExportExcelBook(List<BookViewDto> bookViews)
        {

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Quote = '"',
                Escape = '"',
                Encoding = Encoding.UTF8,
            };

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
                using (var csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfig))
                {
                    csvWriter.WriteRecords(bookViews);
                    await streamWriter.FlushAsync();
                }

                memoryStream.Position = 0;

                var csvData = memoryStream.ToArray();

                var fileName = "Books.csv";
                var contentType = "text/csv";

                return new FileContentResult(csvData, contentType)
                {
                    FileDownloadName = fileName
                };

            }
        }
    }
}
