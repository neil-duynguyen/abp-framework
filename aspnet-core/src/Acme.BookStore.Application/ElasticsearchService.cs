using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore
{
    public class ElasticsearchService
    {
        private static readonly string ElasticSearchUrl = "http://localhost:9200";
        private static ElasticClient _client;

        static ElasticsearchService()
        {
            var settings = new ConnectionSettings(new Uri(ElasticSearchUrl)).DefaultIndex("book_store");
            _client = new ElasticClient(settings);
        }

        public static ElasticClient Client => _client;
    }

}
