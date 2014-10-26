using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public class BookUnitListDataSource
    {
        public BookUnitListDataSource(double code, string message)
        {
            Code = code;
            Message = message;
            Value = new ObservableCollection<BookUnit>();
        }
        
        public double Code { get; private set; }
        public string Message { get; private set; }
        public ObservableCollection<BookUnit> Value { get; private set; }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class GetBookUnitListDataSource : BaseDataSource
    {
        //http://m.hujiang.com/handler/appweb.json?v=0.9860641923733056&op=GetBookUnitList&key=nce&bookKey=xingainian1&callback=jsonp1
        private const string JSON_URL = "http://m.hujiang.com/handler/appweb.json?v={0}&op=GetBookUnitList&key=nce&bookKey={1}";

        private static readonly GetBookUnitListDataSource _sampleDataSource = new GetBookUnitListDataSource();
        private string BookKey { get; set; }
        public BookUnitListDataSource BookUnitListDataSource { get; private set; }

        public static async Task<BookUnitListDataSource> GetBookUnitListAsync(string key, string bookKey)
        {
            await _sampleDataSource.GetSampleDataAsync(key, bookKey);

            return _sampleDataSource.BookUnitListDataSource;
        }


        private async Task GetSampleDataAsync(string key, string bookKey)
        {
            if (this.BookKey == bookKey && this.BookUnitListDataSource != null)
                return;

            this.BookKey = bookKey;
            this.JsonLocalFileName = bookKey + "GetBookUnitList.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks, bookKey);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Value"].GetArray();

            this.BookUnitListDataSource = new BookUnitListDataSource(jsonObject["Code"].GetNumber(), jsonObject["Message"].GetString());

            foreach (JsonValue bookValue in jsonArray)
            {
                JsonObject bookObject = bookValue.GetObject();
                BookUnit bookUnit = new BookUnit(bookObject["Key"].GetString(),
                                                bookObject["UnitIndex"].GetString(),
                                                bookObject["Name"].GetString()
                                                );

                this.BookUnitListDataSource.Value.Add(bookUnit);
            }
        }

    }
}
