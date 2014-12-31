using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using WP.NCE.Common;

namespace WP.NCE.DataModel
{
    public class BaseDataSource
    {
        public const string RootFolder = "WP.NCE";
        public string JsonDataUri { get; set; }

        public string JsonLocalFileName { get; set; }

        public async Task<string> GetJsonDataSource()
        {
            var jsonfilename = await StorageDataHelper.GetJsonFileNameFromMusicLibraryAsync(RootFolder, JsonLocalFileName);

            if (string.IsNullOrEmpty(jsonfilename))
            {
                Uri dataUri = new Uri(JsonDataUri);
                HttpClient client = new HttpClient();

                using (var response = await client.GetAsync(dataUri))
                {
                    var json = await response.Content.ReadAsStringAsync();
                   await StorageDataHelper.writeTextToSDCard(RootFolder, JsonLocalFileName, json);
                    return json;
                }
            }
            else
            {
                 // read from sd card
                var json = await StorageDataHelper.readTextFromSDCard(RootFolder, JsonLocalFileName);
                return json;
            }

            
        }
    }
}
