using System;
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
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //FluentDialog.Show("Dialog", "This is a Dialog.", new FluentDialogOptions 
            //{ CancelButtonVisibility = Visibility.Collapsed, DeleteButtonVisibility = Visibility.Collapsed });

            var cancelationToken = new CancellationTokenSource();
            await FluentDialog.ShowProgressAsync("Aguarde...", "Processando...", () =>
            {
                Thread.Sleep(4000);
            }, cancelationToken);
        }
    }
}
