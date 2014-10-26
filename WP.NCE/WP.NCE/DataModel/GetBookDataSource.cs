using System;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public class BookDataSource
    {
        public BookDataSource(double code, string message, Book value)
        {
            Code = code;
            Message = message;
            Value = value;
        }

        public double Code { get; private set; }
        public string Message { get; private set; }
        public Book Value { get; private set; }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class GetBookDataSource : BaseDataSource
    {
        //http://m.hujiang.com/handler/appweb.json?v=0.3706367036793381&op=GetBook&Key=nce&bookKey=xingainian1&callback=jsonp2
        private const string JSON_URL = "http://m.hujiang.com/handler/appweb.json?v={0}&op=GetBook&Key=nce&bookKey={1}";

        private static GetBookDataSource _sampleDataSource = new GetBookDataSource();

        private string BookKey { get; set; }

        private BookDataSource _bookDataSource;
        public BookDataSource BookDataSource
        {
            get { return this._bookDataSource; }
        }

        public static async Task<BookDataSource> GetBookAsync(string key, string bookKey)
        {
            await _sampleDataSource.GetSampleDataAsync(key, bookKey);

            return _sampleDataSource.BookDataSource;
        }


        private async Task GetSampleDataAsync(string key, string bookKey)
        {
            
            if (this.BookKey == bookKey && this._bookDataSource != null)
                return;

            this.BookKey = bookKey;
            this.JsonLocalFileName = bookKey + "GetBook.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks, bookKey);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonObject bookObject = jsonObject["Value"].GetObject();

            Book book = new Book(bookObject["Key"].GetString(),
                                bookObject["BookIndex"].GetString(),
                                bookObject["EnName"].GetString(),
                                bookObject["CnName"].GetString(),
                                bookObject["ShortName"].GetString(),
                                bookObject["ImgUrl"].ToJsonString());
            this._bookDataSource = new BookDataSource(jsonObject["Code"].GetNumber(), 
                jsonObject["Message"].GetString(), book);
        }

    }

    public static class JsonStringExtension
    {
        public static string ToJsonString(this IJsonValue obj)
        {
            if (obj == null || obj.ValueType == JsonValueType.Null)
            {
                return string.Empty;
            }

            return obj.GetString();
        }
    }

}
