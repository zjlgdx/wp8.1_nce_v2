using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public class BookTextListDataSource
    {
        public BookTextListDataSource(double code, string message)
        {
            Code = code;
            Message = message;
            Value = new ObservableCollection<BookText>();
        }
        
        public double Code { get; private set; }
        public string Message { get; private set; }
        public ObservableCollection<BookText> Value { get; private set; }
    }


    public class GetBookTextListDataSource : BaseDataSource
    {
        //http://m.hujiang.com/handler/appweb.json?v=0.19058830104768276&op=GetBookTextList&key=nce&unitKey=1-1-24&callback=jsonp1
        private const string JSON_URL = "http://m.hujiang.com/handler/appweb.json?v={0}&op=GetBookTextList&key=nce&unitKey={1}";
        private static GetBookTextListDataSource _sampleDataSource = new GetBookTextListDataSource();

        private string UnitKey { get; set; }

        private BookTextListDataSource _bookTextListDataSource;
        public BookTextListDataSource BookTextListDataSource
        {
            get { return this._bookTextListDataSource; }
        }

        public static async Task<BookTextListDataSource> GetBookTextListAsync(string key, string unitKey)
        {
            await _sampleDataSource.GetSampleDataAsync(key, unitKey);

            return _sampleDataSource.BookTextListDataSource;
        }


        private async Task GetSampleDataAsync(string key, string unitKey)
        {
            if (this.UnitKey == unitKey && this._bookTextListDataSource != null)
                return;

            this.UnitKey = unitKey;
            this.JsonLocalFileName = unitKey + "GetBookTextList.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks, unitKey);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Value"].GetArray();

            this._bookTextListDataSource = new BookTextListDataSource(jsonObject["Code"].GetNumber(), jsonObject["Message"].GetString());

            foreach (JsonValue bookValue in jsonArray)
            {
                JsonObject bookObject = bookValue.GetObject();
                BookText bookUnit = new BookText(bookObject["Key"].GetString(),
                                                bookObject["Index"].GetString(),
                                                bookObject["Name"].GetString(),
                                                bookObject["Video"].GetString()
                                                );

                this.BookTextListDataSource.Value.Add(bookUnit);
            }
        }

    }
}
