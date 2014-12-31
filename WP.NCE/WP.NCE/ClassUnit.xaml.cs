using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WP.NCE.Common;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using WP.NCE.DataModel;

namespace WP.NCE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClassUnit : Page
    {
        private MediaPlayer _mediaPlayer;

        private const string FirstGroupName = "FirstGroup";
        private const string SecondGroupName = "SecondGroup";
        private const string ThreeGroupName = "ThreeGroup";
        private const string FourGroupName = "FourGroup";
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private string bookTextKey;
        private string bookTitle;

        public ClassUnit()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            bool failed = false;
            try
            {
                bookTextKey = (string)e.NavigationParameter;

                var message = new ValueSet();
                message.Add("bookTextKey", bookTextKey);
                BackgroundMediaPlayer.SendMessageToBackground(message);

                var bookTextInfo = await GetBookTextDataSource.GetBookTextAsync(null, bookTextKey);
                this.DefaultViewModel["BookText"] = bookTextInfo;
                bookTitle = bookTextInfo.Value.Name;
                var sampleDataGroup =
                    await GetYuanWenListDataSource.GetYuanWenAsync(bookTextKey: bookTextKey);
                this.DefaultViewModel[FirstGroupName] = sampleDataGroup;

                var mp3file = await DownloadAudioFile();

                string[] fileInfo = new[] { bookTitle, mp3file, bookTextKey, "!autoplay" };
                message = new ValueSet
                    {
                        {
                            "SetSource",
                            fileInfo
                        }

                    };
                BackgroundMediaPlayer.SendMessageToBackground(message);

            }
            catch (Exception)
            {
                failed = true;
            }
            if (failed)
            {
                MessageDialog md2 = new MessageDialog("网络异常，请检查网络设置!", "网络链接");
                await md2.ShowAsync();
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _mediaPlayer = BackgroundMediaPlayer.Current;
            App.Current.Suspending += ForegroundApp_Suspending;
            App.Current.Resuming += ForegroundApp_Resuming;
            _mediaPlayer.CurrentStateChanged += this.MediaPlayer_CurrentStateChanged;
            BackgroundMediaPlayer.MessageReceivedFromBackground += BackgroundMediaPlayer_MessageReceivedFromBackground;
            this.navigationHelper.OnNavigatedTo(e);
        }

        async void BackgroundMediaPlayer_MessageReceivedFromBackground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            ValueSet valueSet = e.Data;

            foreach (string key in valueSet.Keys)
            {
                switch (key)
                {
                    case "updateplaybuttonstatus":
                        Debug.WriteLine("updateplaybuttonstatus:");
                        //
                        var _bookTextKey = e.Data[key].ToString();
                        Debug.WriteLine(bookTextKey);

                        if (MediaPlayerState.Playing == BackgroundMediaPlayer.Current.CurrentState)
                        {
                            if (_bookTextKey == bookTextKey)
                            {
                                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                        {
                                            PlayAppBarButton.Label = "pause";
                                            PlayAppBarButton.Icon = new SymbolIcon(Symbol.Pause);
                                        });
                            }

                        }


                        break;
                }
            }
        }

        private void ForegroundApp_Resuming(object sender, object e)
        {
            _mediaPlayer.CurrentStateChanged += this.MediaPlayer_CurrentStateChanged;
        }

        private void ForegroundApp_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            _mediaPlayer.CurrentStateChanged -= this.MediaPlayer_CurrentStateChanged;
            BackgroundMediaPlayer.MessageReceivedFromBackground -= BackgroundMediaPlayer_MessageReceivedFromBackground;
            deferral.Complete();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        /// <summary>
        /// MediaPlayer state changed event handlers. 
        /// Note that we can subscribe to events even if Media Player is playing media in background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void MediaPlayer_CurrentStateChanged(MediaPlayer sender, object args)
        {
            switch (sender.CurrentState)
            {
                case MediaPlayerState.Playing:
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        PlayAppBarButton.Label = "pause";
                        PlayAppBarButton.Icon = new SymbolIcon(Symbol.Pause);
                    }
                        );

                    break;
                case MediaPlayerState.Paused:
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        PlayAppBarButton.Label = "play";
                        PlayAppBarButton.Icon = new SymbolIcon(Symbol.Play);
                    }
                    );

                    break;
            }
        }

        private async void FourPivot_Loaded(object sender, RoutedEventArgs e)
        {
            bool failed = false;
            try
            {


                var sampleDataGroup = await GetXiangJieListDataSource.GetXiangJieAsync(bookTextKey: bookTextKey);
                //"<html><body><h2>This is an HTML fragment</h2></body></html>");
                StringBuilder sbHtml = new StringBuilder("<html><body>");
                foreach (var lineContent in sampleDataGroup.Value)
                {
                    sbHtml.AppendFormat("<p>{0}</p>", lineContent.Content);
                }

                sbHtml.Append("</body></html>");
                wvXiangjie.NavigateToString(sbHtml.ToString());
                this.DefaultViewModel[FourGroupName] = sampleDataGroup;
            }
            catch (Exception)
            {

                failed = true;
            }

            if (failed)
            {
                MessageDialog md2 = new MessageDialog("网络异常，请检查网络设置!", "网络链接");
                await md2.ShowAsync();
            }
        }

        private enum Mp3Type
        {
            Unknown,
            American,
            English

        }

        private Mp3Type AudioType { get; set; }

        private string getAudioFileName
        {
            get
            {
                var filename = "";
                switch (AudioType)
                {
                    case Mp3Type.Unknown:
                        filename = bookTextKey + "am_yuanwen.mp3";
                        break;
                    case Mp3Type.American:
                        filename = bookTextKey + "am_yuanwen.mp3";
                        break;
                    case Mp3Type.English:
                        filename = bookTextKey + "yuanwen.mp3";
                        break;
                    default:
                        filename = bookTextKey + "am_yuanwen.mp3";
                        break;
                }

                return filename;
            }
        }



        private async Task<string> DownloadAudioFile()
        {
            var pronunciation = "";

            switch (AudioType)
            {
                case Mp3Type.Unknown:
                    pronunciation = "am_";
                    break;
                case Mp3Type.American:
                    pronunciation = "am_";
                    break;
                case Mp3Type.English:
                    pronunciation = "";
                    break;
                default:
                    pronunciation = "am_";
                    break;
            }

            var part = bookTextKey.Substring(0, bookTextKey.LastIndexOf("-"));
            var part2 = bookTextKey.Substring(bookTextKey.LastIndexOf("-") + 1);
            //http://f1.w.hjfile.cn/doc/touch_m/nce/3-41-50/am_yuanwen_42.mp3
            //http://f1.w.hjfile.cn/doc/touch_m/nce/3-41-50/yuanwen_42.mp3
            var downloadUriString = string.Format("http://f1.w.hjfile.cn/doc/touch_m/nce/{0}/{1}yuanwen_{2}.mp3",
                part, pronunciation, part2);
            var fileName = getAudioFileName;

            var file = await StorageDataHelper.GetAudioFileFromMusicLibraryAsync("WP.NCE", fileName);
            if (string.IsNullOrEmpty(file))
            {
                var filefullname = await StorageDataHelper.DownloadAudioFileToMusicLibraryAsync(downloadUriString, "WP.NCE", fileName);
                return filefullname;
            }

            return file;
        }

        private async void ThreePivot_Loaded(object sender, RoutedEventArgs e)
        {
            bool failed = false;
            try
            {
                var sampleDataGroup = await GetCiHuiListDataSource.GetVocabularyAsync(bookTextKey: bookTextKey);
                this.DefaultViewModel[ThreeGroupName] = sampleDataGroup;

            }
            catch (Exception)
            {
                failed = true;
            }

            if (failed)
            {
                MessageDialog md2 = new MessageDialog("网络异常，请检查网络设置!", "网络链接");
                await md2.ShowAsync();
            }
        }

        private async void SecondPivot_Loaded(object sender, RoutedEventArgs e)
        {
            bool failed = false;
            try
            {
                var sampleDataGroup = await GetShuangYuListDataSource.GetYuanWenAsync(bookTextKey: bookTextKey);
                this.DefaultViewModel[SecondGroupName] = sampleDataGroup;

            }
            catch (Exception)
            {

                failed = true;
            }

            if (failed)
            {
                MessageDialog md2 = new MessageDialog("网络异常，请检查网络设置!", "网络链接");
                await md2.ShowAsync();
            }
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var paragraph = (Paragraph)e.ClickedItem;
            var timespanValue = "00:" + paragraph.Time;

            var message = new ValueSet();
            message.Add("SetPosition", timespanValue);
            BackgroundMediaPlayer.SendMessageToBackground(message);
        }

        private async void PlayAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = await StorageDataHelper.GetAudioFileFromMusicLibraryAsync("WP.NCE", getAudioFileName);

                if (MediaPlayerState.Playing == BackgroundMediaPlayer.Current.CurrentState)
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        string[] fileInfo = new[] { bookTitle, file, bookTextKey, "autoplay" };
                        var message = new ValueSet
                    {
                        {
                            "samePause-notPlay",
                            fileInfo
                        }

                    };
                        BackgroundMediaPlayer.SendMessageToBackground(message);
                    }
                    else
                    {
                        MessageDialog md2 = new MessageDialog("file is not ready!", "audio");
                        await md2.ShowAsync();
                    }
                }
                else if (MediaPlayerState.Paused == BackgroundMediaPlayer.Current.CurrentState)
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        string[] fileInfo = new[] { bookTitle, file, bookTextKey, "autoplay" };
                        var message = new ValueSet
                    {
                        {
                            "samePlay-notPlay",
                            fileInfo
                        }

                    };
                        BackgroundMediaPlayer.SendMessageToBackground(message);
                    }
                    else
                    {
                        MessageDialog md2 = new MessageDialog("file is not ready!", "audio");
                        await md2.ShowAsync();
                    }
                }
                else if (MediaPlayerState.Closed == BackgroundMediaPlayer.Current.CurrentState)
                {


                    if (!string.IsNullOrEmpty(file))
                    {
                        string[] fileInfo = new[] { bookTitle, file, bookTextKey, "autoplay" };
                        var message = new ValueSet
                    {
                        {
                            "Play",
                            fileInfo
                        }

                    };
                        BackgroundMediaPlayer.SendMessageToBackground(message);
                    }
                    else
                    {
                        MessageDialog md2 = new MessageDialog("file is not ready!", "audio");
                        await md2.ShowAsync();
                    }

                }

            }
            catch (Exception)
            {


            }


        }

        private async void American_OnClick(object sender, RoutedEventArgs e)
        {
            bool failed = false;
            try
            {
                AudioType = Mp3Type.American;
                var file = await DownloadAudioFile();

                if (!string.IsNullOrEmpty(file))
                {
                    string[] fileInfo = new[] { bookTitle, file, bookTextKey, "autoplay" };
                    var message = new ValueSet
                    {
                        {
                            "Play",
                            fileInfo
                        }

                    };
                    BackgroundMediaPlayer.SendMessageToBackground(message);
                }
                else
                {
                    MessageDialog md2 = new MessageDialog("file is not ready!", "audio");
                    await md2.ShowAsync();
                }
            }
            catch (Exception)
            {

                failed = true;
            }

            if (failed)
            {
                MessageDialog md3 = new MessageDialog("网络异常，请检查网络设置!", "网络链接");
                await md3.ShowAsync();
            }
        }

        private async void English_OnClick(object sender, RoutedEventArgs e)
        {
            bool failed = false;
            try
            {
                AudioType = Mp3Type.English;
                var file = await DownloadAudioFile();

                if (!string.IsNullOrEmpty(file))
                {
                    string[] fileInfo = new[] { bookTitle, file, bookTextKey, "autoplay" };
                    var message = new ValueSet
                    {
                        {
                            "Play",
                            fileInfo
                        }

                    };
                    BackgroundMediaPlayer.SendMessageToBackground(message);
                }
                else
                {
                    MessageDialog md2 = new MessageDialog("file is not ready!", "audio");
                    await md2.ShowAsync();
                }
            }
            catch (Exception)
            {

                failed = true;
            }

            if (failed)
            {
                MessageDialog md3 = new MessageDialog("网络异常，请检查网络设置!", "网络链接");
                await md3.ShowAsync();
            }
        }
    }
}
