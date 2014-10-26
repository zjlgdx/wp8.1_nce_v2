using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public class BookListDataSource
    {
        public BookListDataSource(double code, string message)
        {
            Code = code;
            Message = message;
            Value = new ObservableCollection<Book>();
        }
        
        public double Code { get; private set; }
        public string Message { get; private set; }
        public ObservableCollection<Book> Value { get; private set; }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class GetBookListDataSource : BaseDataSource
    {
        //http://m.hujiang.com/handler/appweb.json?v=0.6686999415978789&op=GetBookList&key=nce&callback=jsonp1
        private const string JSON_URL = "http://m.hujiang.com/handler/appweb.json?v={0}&op=GetBookList&key=nce";

        private static GetBookListDataSource _sampleDataSource = new GetBookListDataSource();

        private BookListDataSource _bookListDataSource;
        public BookListDataSource BookListDataSource
        {
            get { return this._bookListDataSource; }
        }

        public static async Task<BookListDataSource> GetBookListAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.BookListDataSource;
        }
        

        private async Task GetSampleDataAsync()
        {
            
            if (this._bookListDataSource != null)
                return;
            this.JsonLocalFileName = "GetBookList.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Value"].GetArray();

            this._bookListDataSource = new BookListDataSource(jsonObject["Code"].GetNumber(), jsonObject["Message"].GetString());

            foreach (JsonValue bookValue in jsonArray)
            {
                JsonObject bookObject = bookValue.GetObject();
                Book book = new Book(bookObject["Key"].GetString(),
                                    bookObject["BookIndex"].GetString(),
                                    bookObject["EnName"].GetString(),
                                    bookObject["CnName"].GetString(),
                                    bookObject["ShortName"].GetString(),
                                    bookObject["ImgUrl"].ToJsonString());

                this.BookListDataSource.Value.Add(book);
            }
        }

    }
}
