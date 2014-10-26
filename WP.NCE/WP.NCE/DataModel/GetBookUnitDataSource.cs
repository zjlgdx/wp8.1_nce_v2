using System;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{

    public class BookUnitDataSource
    {
        public BookUnitDataSource(double code, string message, BookUnit value)
        {
            Code = code;
            Message = message;
            Value = value;
        }

        public double Code { get; private set; }
        public string Message { get; private set; }
        public BookUnit Value { get; private set; }

        public override string ToString()
        {
            return this.Message;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class GetBookUnitDataSource : BaseDataSource
    {
        //http://m.hujiang.com/handler/appweb.json?v=0.7858805949799716&op=GetBookUnit&key=nce&unitKey=1-1-24&callback=jsonp2
        private const string JSON_URL = "http://m.hujiang.com/handler/appweb.json?v={0}&op=GetBookUnit&key=nce&unitKey={1}";

        private static GetBookUnitDataSource _sampleDataSource = new GetBookUnitDataSource();
        private string UnitKey { get; set; }

        private BookUnitDataSource _bookUnitDataSource;
        public BookUnitDataSource BookUnitDataSource
        {
            get { return this._bookUnitDataSource; }
        }

        public static async Task<BookUnitDataSource> GetBookUnitAsync(string key, string unitKey)
        {
            await _sampleDataSource.GetSampleDataAsync(key, unitKey);

            return _sampleDataSource.BookUnitDataSource;
        }


        private async Task GetSampleDataAsync(string key, string unitKey)
        {
            if (this.UnitKey == unitKey && this._bookUnitDataSource != null)
                return;

            this.UnitKey = unitKey;
            this.JsonLocalFileName = unitKey + "GetBookUnit.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks, unitKey);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonObject bookUnitValue = jsonObject["Value"].GetObject();

            BookUnit bookUnit = new BookUnit(bookUnitValue["Key"].GetString(),
                                             bookUnitValue["UnitIndex"].GetString(),
                                             bookUnitValue["Name"].GetString());

            this._bookUnitDataSource = new BookUnitDataSource(jsonObject["Code"].GetNumber(), 
                jsonObject["Message"].GetString(), bookUnit);


        }

    }
}
