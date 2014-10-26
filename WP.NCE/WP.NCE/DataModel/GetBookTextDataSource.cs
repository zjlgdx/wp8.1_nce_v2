using System;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public class BookTextDataSource
    {
        public BookTextDataSource(double code, string message, BookText value)
        {
            Code = code;
            Message = message;
            Value = value;
        }

        public double Code { get; private set; }
        public string Message { get; private set; }
        public BookText Value { get; private set; }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class GetBookTextDataSource : BaseDataSource
    {
        //http://m.hujiang.com/handler/appweb.json?v=0.9384690483566374&op=GetBookText&key=nce&textKey=1-1-24-1&callback=jsonp6
        private const string JSON_URL = "http://m.hujiang.com/handler/appweb.json?v={0}&op=GetBookText&key=nce&textKey={1}";

        private static GetBookTextDataSource _sampleDataSource = new GetBookTextDataSource();

        private string TextKey { get; set; }

        private BookTextDataSource _bookTextDataSource;
        public BookTextDataSource BookTextDataSource
        {
            get { return this._bookTextDataSource; }
        }

        public static async Task<BookTextDataSource> GetBookTextAsync(string key, string textKey)
        {
            await _sampleDataSource.GetSampleDataAsync(key, textKey);

            return _sampleDataSource.BookTextDataSource;
        }


        private async Task GetSampleDataAsync(string key, string textKey)
        {
            
            if (this.TextKey == textKey && this._bookTextDataSource != null)
                return;

            this.TextKey = textKey;
            this.JsonLocalFileName = textKey + "GetBookText.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks, textKey);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonObject bookObject = jsonObject["Value"].GetObject();
            BookText bookText = new BookText(bookObject["Key"].GetString(),
                                bookObject["Index"].GetString(),
                                bookObject["Name"].GetString(),
                                bookObject["Video"].GetString());
            this._bookTextDataSource = new BookTextDataSource(jsonObject["Code"].GetNumber(),
                jsonObject["Message"].GetString(), bookText);
        }

    }
}
