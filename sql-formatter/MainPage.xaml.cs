using SqlFormatter.Classes;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace sql_formatter
{
    public sealed partial class MainPage : Page
    {
        public string KeywordsRegex { get; private set; }
        private MainPageState _state { get; set; }

        public MainPage()
        {
            InitializeComponent();

            const int width = 680;
            const int height = 340;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(width, height));
            ApplicationView.PreferredLaunchViewSize = new Size(width, height);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            _state = new MainPageState();
            sbPupilInitial.Begin();
        }

        private async void TbRaw_TextChanged(object sender, TextChangedEventArgs e)
        {
            var raw = ((TextBox)sender).Text;
            KeywordsRegex = KeywordsRegex ?? await GetTSqlKeywordsRegex();
            var formatted = SqlFormatter.Classes.SqlFormatter.Format(KeywordsRegex, raw);
            tbFormatted.Text = formatted.Result;

            if (formatted.Changed)
            {
                tbMatches.Opacity = 1;
                runNumMatches.Text = formatted.NumReplacements.ToString();
                tbVerified.Opacity = formatted.Verified ? 1 : 0;
                sbPupilRight.Begin();
            }
            else
            {
                tbMatches.Opacity = 0;
                tbVerified.Opacity = 0;
                sbPupilLeft.Begin();
            }

            if (!_state.FormattedTextboxShown)
            {
                _state.FormattedTextboxShown = true;
                sbFormattedShow.Begin();
            }
        }

        private async Task<string> GetTSqlKeywordsRegex()
        {
            var keywordsFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///tsql-keywords.txt"));
            var keywords = await FileIO.ReadLinesAsync(keywordsFile);
            return SqlKeywordsProvider.GetKeywordsRegex(keywords);
        }

        private void TbFormatted_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.SelectAll();
            CopyToClipboard(tb.Text);
        }

        private void CopyToClipboard(string text)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(text);
            Clipboard.SetContent(dataPackage);
        }
    }
}
