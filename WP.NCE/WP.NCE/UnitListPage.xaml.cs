using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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
    public sealed partial class UnitListPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        Color? originbackgroundColor;
        Color? originforegroundColor;
        double originopacity;

        public UnitListPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;


            StatusBar statusBar = StatusBar.GetForCurrentView();
            originbackgroundColor = statusBar.BackgroundColor;
            originforegroundColor = statusBar.ForegroundColor;
            originopacity = statusBar.BackgroundOpacity;
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
                await Helper.ShowSystemTrayAsync(Colors.CornflowerBlue, Colors.White, text: "loading...");
            
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var bookUnitKey = (string)e.NavigationParameter;
            //this.DefaultViewModel["Item"] = item;
            //key=nce&unitKey=1-1-24
            var bookUnit = await GetBookUnitDataSource.GetBookUnitAsync(key: "nce", unitKey: bookUnitKey);
            this.DefaultViewModel["BookUnit"] = bookUnit;

            //key=nce&unitKey=1-1-24
            var bookTextList = await GetBookTextListDataSource.GetBookTextListAsync(key: "nce", unitKey: bookUnitKey);
            this.DefaultViewModel["BookTextList"] = bookTextList;
            }
            catch (Exception)
            {
                failed = true;
            }
            await Helper.HideSystemTrayAsync(originbackgroundColor, originforegroundColor, originopacity);
            if (failed)
            {
                MessageDialog md2 = new MessageDialog(Constants.WININET_E_NAME_NOT_RESOLVED, Constants.NETWORK_CONNECTION);
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
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        

        private void UnitInfoListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var bookText = (BookText)e.ClickedItem;
            if (!Frame.Navigate(typeof(ClassUnit), bookText.Key))
            {
                throw new Exception("Navigation failed.");
            }
        }

        
    }
}
