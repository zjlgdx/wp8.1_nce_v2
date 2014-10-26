using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public class XiangJieListDataSource
    {
        public XiangJieListDataSource(double code, string message)
        {
            Code = code;
            Message = message;
            Value = new ObservableCollection<LineContent>();
        }
        public double Code { get; private set; }
        public string Message { get; private set; }
        public ObservableCollection<LineContent> Value { get; private set; }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class GetXiangJieListDataSource : BaseDataSource
    {
        //4.2 http://m.hujiang.com/handler/appweb.json?v=0.8839236546773463&op=GetXiangJieList&bookIndex=1&startIndex=1&endIndex=24&currentIndex=1&callback=jsonp5
        private const string JSON_URL = "http://m.hujiang.com/handler/appweb.json?v={0}&op=GetXiangJieList&bookIndex={1}&startIndex={2}&endIndex={3}&currentIndex={4}";

        private static readonly GetXiangJieListDataSource _sampleDataSource = new GetXiangJieListDataSource();
        private string BookTextKey { get; set; }
        public XiangJieListDataSource XiangJieDataSource { get; private set; }

        public static async Task<XiangJieListDataSource> GetXiangJieAsync(string bookTextKey)
        {
            await _sampleDataSource.GetSampleDataAsync(bookTextKey);

            return _sampleDataSource.XiangJieDataSource;
        }


        private async Task GetSampleDataAsync(string bookTextKey)
        {
            if (this.BookTextKey == bookTextKey && this.XiangJieDataSource != null)
                return;

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
            this.JsonLocalFileName = bookTextKey + "GetXiangJieList.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks, bookIndex, startIndex, endIndex, currentIndex);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Value"].GetArray();

            this.XiangJieDataSource = new XiangJieListDataSource(jsonObject["Code"].GetNumber(), jsonObject["Message"].GetString());

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                LineContent group = new LineContent(
                                                groupObject["LineContent"].GetString());

                this.XiangJieDataSource.Value.Add(group);
            }
        }

    }
}
