using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;

namespace WP.NCE.Common
{
    class StorageDataHelper
    {
        public static async Task<string> GetJsonFileNameFromMusicLibraryAsync(string foldername, string filename)
        {
            try
            {
                var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                //var devices = KnownFolders.RemovableDevices;

                //var sdCards = await devices.GetFoldersAsync();

                //if (sdCards.Count == 0) return string.Empty;

                //var firstCard = sdCards[0];

                StorageFolder folder = await localFolder.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
                StorageFolder subfolder = await folder.CreateFolderAsync("Json", CreationCollisionOption.OpenIfExists);

                var file = await subfolder.GetFileAsync(filename);
                return file.Path;
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("File not found");

                return string.Empty;
            }


        }

        public static async Task<string> GetAudioFileFromMusicLibraryAsync(string foldername, string filename)
        {
            try
            {
                var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                //var devices = KnownFolders.RemovableDevices;

                //var sdCards = await devices.GetFoldersAsync();

                //if (sdCards.Count == 0) return string.Empty;

                //var firstCard = sdCards[0];

                StorageFolder folder = await localFolder.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
                StorageFolder subfolder = await folder.CreateFolderAsync("audio", CreationCollisionOption.OpenIfExists);
                var file = await subfolder.GetFileAsync(filename);
                return file.Path;
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("File not found");

                return string.Empty;
            }


        }


        public async static Task writeTextToSDCard(string foldername, string filename, string logData)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var devices = Windows.Storage.KnownFolders.RemovableDevices;

            //var sdCards = await devices.GetFoldersAsync();

            //if (sdCards.Count == 0) return;

            //var firstCard = sdCards[0];

            StorageFolder notesFolder = await localFolder.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
            StorageFolder subfolder = await notesFolder.CreateFolderAsync("Json", CreationCollisionOption.OpenIfExists);
            StorageFile file = await subfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, logData);

            

        }

        public async static Task<string> readTextFromSDCard(string foldername, string filename)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var devices = Windows.Storage.KnownFolders.RemovableDevices;

            //var sdCards = await devices.GetFoldersAsync();

            //if (sdCards.Count == 0) return null;

            //var firstCard = sdCards[0];
            StorageFolder notesFolder = await localFolder.GetFolderAsync(foldername);
            StorageFolder subfolder = await notesFolder.GetFolderAsync("Json");
            StorageFile file = await subfolder.GetFileAsync(filename);
            
            string result = await FileIO.ReadTextAsync(file);
            return result;

            
        }

        //public static async Task DownloadJsonFileToMusicLibraryAsync(string downloadUriString, string foldername, string filename)
        //{
        //    var devices = KnownFolders.RemovableDevices;

        //    var sdCards = await devices.GetFoldersAsync();

        //    if (sdCards.Count == 0) return;

        //    var firstCard = sdCards[0];

        //    Uri downLoadingUri = new Uri(downloadUriString);
        //    HttpClient client = new HttpClient();
        //    using (var response = await client.GetAsync(downLoadingUri))
        //    {

        //        var buffer = await response.Content.ReadAsStringAsync();
        //        var folder = await firstCard.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
        //        StorageFolder subfolder = await folder.CreateFolderAsync("Json", CreationCollisionOption.OpenIfExists);
        //        StorageFile file = await subfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
        //        await FileIO.WriteTextAsync(file, buffer);
        //    }
        //}

        public static async Task<string> DownloadAudioFileToMusicLibraryAsync(string downloadUriString, string foldername, string filename)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var devices = KnownFolders.RemovableDevices;

            //var sdCards = await devices.GetFoldersAsync();

            //if (sdCards.Count == 0) return string.Empty;

            //var firstCard = sdCards[0];

            Uri downLoadingUri = new Uri(downloadUriString);
            HttpClient client = new HttpClient();
            using (var response = await client.GetAsync(downLoadingUri))
            {
                //http://msdn.microsoft.com/en-us/library/windows/apps/xaml/Hh758325(v=win.10).aspx
                // Quickstart: Reading and writing files (XAML)\
                var buffer = await response.Content.ReadAsBufferAsync();
                var folder = await localFolder.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
                StorageFolder subfolder = await folder.CreateFolderAsync("audio", CreationCollisionOption.OpenIfExists);
                StorageFile file = await subfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBufferAsync(file, buffer);

                return file.Path;
            }
        }
    }
}
