using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FluentUI.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();

        //    lstIcons.ItemsSource = Enum.GetValues(typeof(FluentIconEnum));

            this.Loaded += MainWindow_Loaded;
        }        

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
          //  listView.ItemsSource = await GetUserList();
          //  dataGrid.ItemsSource = DummyData.Get();
        }

        private async Task<IEnumerable> GetUserList()
        {
            return null;
            //using var httpClient = new HttpClient();
            //using var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            //string apiResponse = await response.Content.ReadAsStringAsync();
            //return JsonSerializer.Deserialize<List<User>>(apiResponse, new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true,
            //});
        }

        private void lstIcons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 1) return;
            var icon = e.AddedItems.Cast<FluentIconEnum>().First();
            Clipboard.SetText($"<fabric:FabricIcon Icon=\"{icon}\" />");
        }

        private void dialog_Click(object sender, RoutedEventArgs e)
        {
            FluentDialog.Show("Dialog", "This is a Dialog.", new FluentDialogOptions
            { CancelButtonVisibility = Visibility.Collapsed, DeleteButtonVisibility = Visibility.Collapsed });
        }

        private async void progress_Click(object sender, RoutedEventArgs e)
        {
            var cancelationToken = new CancellationTokenSource();

            await FluentDialog.ShowProgressAsync("Wait...", "Process 1...", () =>
            {
                Thread.Sleep(4000);
                FluentDialog.UpdateDialog("Process 2...");
                Thread.Sleep(3000);

            }, cancelationToken);
        }

        private void custom_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
