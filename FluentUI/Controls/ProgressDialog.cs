using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Controls
{
    internal class ProgressDialog : Control
    {
        private Button cancelButton;

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ProgressDialog));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
           DependencyProperty.Register("Message", typeof(string), typeof(ProgressDialog));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        static ProgressDialog() { }

        public ProgressDialog() { }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            FluentDialog.CancellationTokenSource.Cancel();
            FluentDialog.CancellationTokenSource.Dispose();
            button.Width = 120;
            button.Content = "Cancelando...";
            button.IsEnabled = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            cancelButton = this.Template.FindName("PART_CancelButton", this) as Button;
            cancelButton.Click += CancelButton_Click;
        }
    }
}
