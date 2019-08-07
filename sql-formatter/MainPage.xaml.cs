using SqlKeywordFormatter.Util;
using System;
using System.Linq;
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

        public MainPage()
        {
            InitializeComponent();

            const int width = 680;
            const int height = 340;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(width, height));
            ApplicationView.PreferredLaunchViewSize = new Size(width, height);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private async void TbRaw_TextChanged(object sender, TextChangedEventArgs e)
        {
            var raw = ((TextBox)sender).Text;
            KeywordsRegex = KeywordsRegex ?? await GetKeywordsRegex();
            var formatted = SqlFormatter.Format(KeywordsRegex, raw);
            tbFormatted.Text = formatted.Result;

            if (formatted.Changed)
            {
                tbMatches.Opacity = 1;
                runNumMatches.Text = formatted.NumReplacements.ToString();
                tbVerified.Opacity = formatted.Verified ? 1 : 0;
            }
            else
            {
                tbMatches.Opacity = 0;
            }
        }

        private async Task<string> GetKeywordsRegex()
        {
            var keywordsFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///sql-keywords.txt"));
            var keywordLines = await FileIO.ReadLinesAsync(keywordsFile);
            var keywords = keywordLines.Select(w => $@"{w}");
            var keywordsRegex = string.Join("|", keywords);
            return keywordsRegex;
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
