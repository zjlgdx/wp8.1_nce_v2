using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public class VocabularyDataSource
    {
        public VocabularyDataSource(double code, string message)
        {
            Code = code;
            Message = message;
            Value = new ObservableCollection<Vocabulary>();
        }
        public double Code { get; private set; }
        public string Message { get; private set; }
        public ObservableCollection<Vocabulary> Value { get; private set; }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class GetCiHuiListDataSource : BaseDataSource
    {
        //http://m.hujiang.com/handler/appweb.json?v=0.8261403972283006&op=GetCiHuiList&bookIndex=1&startIndex=1&endIndex=24&currentIndex=1&callback=jsonp4
        private const string JSON_URL = "http://m.hujiang.com/handler/appweb.json?v={0}&op=GetCiHuiList&bookIndex={1}&startIndex={2}&endIndex={3}&currentIndex={4}";

        private static readonly GetCiHuiListDataSource _sampleDataSource = new GetCiHuiListDataSource();
        private string BookTextKey { get; set; }
        public VocabularyDataSource VocabularyDataSource { get; private set; }

        public static async Task<VocabularyDataSource> GetVocabularyAsync(string bookTextKey)
        {
            await _sampleDataSource.GetSampleDataAsync(bookTextKey);

            return _sampleDataSource.VocabularyDataSource;
        }
        

        private async Task GetSampleDataAsync(string bookTextKey)
        {
            if (this.BookTextKey == bookTextKey && this.VocabularyDataSource != null)
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
            this.JsonLocalFileName = bookTextKey + "GetCiHuiList.json";
            JsonDataUri = string.Format(JSON_URL, DateTime.Now.Ticks, bookIndex, startIndex, endIndex, currentIndex);
            String jsonText = await GetJsonDataSource();

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Value"].GetArray();

            this.VocabularyDataSource = new VocabularyDataSource(jsonObject["Code"].GetNumber(), jsonObject["Message"].GetString());

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                Vocabulary vocabulary = new Vocabulary(groupObject["Word"].GetString(),
                                                groupObject["Pronounce"].GetString(), 
                                                groupObject["Comment"].GetString());

                this.VocabularyDataSource.Value.Add(vocabulary);
            }
        }

    }
}
