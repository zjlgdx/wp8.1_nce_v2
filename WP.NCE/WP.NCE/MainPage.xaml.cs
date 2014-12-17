using System;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using WP.NCE.Common;
using WP.NCE.DataModel;

namespace WP.NCE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        //private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");
        public MainPage()
        {
            this.InitializeComponent();

            // Hub is only supported in Portrait orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            this.NavigationCacheMode = NavigationCacheMode.Required;

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

            
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var bookListDataSource = await GetBookListDataSource.GetBookListAsync();
            this.DefaultViewModel["BookList"] = bookListDataSource;

            // book 1

            //key=nce&bookKey=xingainian1
            var bookOneUnitList = await GetBookUnitListDataSource.GetBookUnitListAsync(key: "nce", bookKey: "xingainian1");
            this.DefaultViewModel["BookOneUnitList"] = bookOneUnitList;

            var bookTwoUnitList = await GetBookUnitListDataSource.GetBookUnitListAsync(key: "nce", bookKey: "xingainian2");
            this.DefaultViewModel["BookTwoUnitList"] = bookTwoUnitList;

            var bookThreeUnitList = await GetBookUnitListDataSource.GetBookUnitListAsync(key: "nce", bookKey: "xingainian3");
            this.DefaultViewModel["BookThreeUnitList"] = bookThreeUnitList;

            var bookFourUnitList = await GetBookUnitListDataSource.GetBookUnitListAsync(key: "nce", bookKey: "xingainian4");
            this.DefaultViewModel["BookFourUnitList"] = bookFourUnitList;
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
            // TODO: Save the unique state of the page here.
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
			
			this.navigationHelper.OnNavigatedTo(e);
        }
		
		protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void BookListSection_ItemClick(object sender, ItemClickEventArgs e)
        {
            var bookKey = ((Book)e.ClickedItem).Key;

            if (!Frame.Navigate(typeof(BookUnitListPage), bookKey))
            {
                throw new Exception("Navigation failed.");
            }
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var bookUnitKey= ((BookUnit)e.ClickedItem).Key;
            if (!Frame.Navigate(typeof(UnitListPage), bookUnitKey))
            {
                throw new Exception("Navigation failed.");
            }
        }
    }
}
