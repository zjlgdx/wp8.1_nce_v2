using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public class YuanWenDataSource
    {
        public YuanWenDataSource(double code, string message)
        {
            Code = code;
            Message = message;
            Value = new ObservableCollection<Paragraph>();
        }
        public double Code { get; private set; }
        public string Message { get; private set; }
        public ObservableCollection<Paragraph> Value { get; private set; }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class GetYuanWenListDataSource : BaseDataSource
    {
        //http://m.hujiang.com/handler/appweb.json?v=0.6368643059395254&op=GetYuanWenList&bookIndex=1&startIndex=1&endIndex=24&currentIndex=1&callback=jsonp2
        private const string JSON_URL = "http://m.hujiang.com/handler/App/NceApp.json?v={0}&op=GetYuanWenList&bookIndex={1}&startIndex={2}&endIndex={3}&currentIndex={4}";

        private static readonly GetYuanWenListDataSource _sampleDataSource = new GetYuanWenListDataSource();
        private string BookTextKey { get; set; }
        public YuanWenDataSource YuanWenDataSource { get; private set; }

        private bool _classUnitChanged;
        public bool ClassUnitChanged
        {
            get { return this._classUnitChanged; }
        }

        public static async Task<YuanWenDataSource> GetYuanWenAsync(string bookTextKey)
        {
            await _sampleDataSource.GetSampleDataAsync(bookTextKey);

            return _sampleDataSource.YuanWenDataSource;
        }

        public static bool GetClassUnitChanged()
        {
            return _sampleDataSource.ClassUnitChanged;
        }

        private async Task GetSampleDataAsync(string bookTextKey)
        {
            if (this.BookTextKey == bookTextKey && this.YuanWenDataSource != null)
            {
                this._classUnitChanged = false;
                return;
            }

            this.BookTextKey = bookTextKey;

            string bookIndex = string.Empty;
            string startIndex = string.Empty;
            string endIndex = string.Empty;
            string currentIndex = string.Empty;
            if (!string.IsNullOrEmpty(bookTextKey))
            {
                var indexs = bookTextKey.Split(new char[] { '-' });
                if (indexs.Length == 4)
                {
                    bookIndex = indexs[0];
                    startIndex = indexs[1];
                    endIndex = indexs[2];
                    currentIndex = indexs[3];
                }
            }
            this.JsonLocalFileName = bookTextKey + "GetYuanWenList.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks, bookIndex, startIndex, endIndex, currentIndex);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Value"].GetArray();

            this.YuanWenDataSource = new YuanWenDataSource(jsonObject["Code"].GetNumber(), jsonObject["Message"].GetString());

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                Paragraph paragraph = new Paragraph(groupObject["Time"].GetString(),
                                                groupObject["Sentence"].GetString(), string.Empty);

                this.YuanWenDataSource.Value.Add(paragraph);
            }

            this._classUnitChanged = true;
        }

    }
}
