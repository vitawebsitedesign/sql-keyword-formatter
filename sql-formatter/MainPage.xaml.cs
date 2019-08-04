using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace sql_formatter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void TbRaw_TextChanged(object sender, TextChangedEventArgs e)
        {
            var raw = ((TextBox)sender).Text;
            var formatted = await SqlFormatter.GetCasedSql(raw);
            tbFormatted.Text = formatted;
        }
    }
}
