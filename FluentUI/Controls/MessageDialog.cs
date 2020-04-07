using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FluentUI.Controls
{
    internal class MessageDialog : Control
    {
        private Button okButton;
        private Button cancelButton;
        private Button deleteButton;
        private TextBlock header;
        private FluentDialogOptions dialogOptions;
        private bool isMouseOverHeader;
        private bool isMoving;
        private Point? controlPosition;
        private double deltaX;
        private double deltaY;
        private TranslateTransform transform;

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MessageDialog));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
           DependencyProperty.Register("Message", typeof(string), typeof(MessageDialog));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public MessageDialog() { }

        public MessageDialog(FluentDialogOptions dialogOptions = null)
        {
            this.dialogOptions = dialogOptions;
            //if (dialogOptions is null)
            //{
            //    deleteButton.Visibility = Visibility.Collapsed;
            //    cancelButton.Visibility = Visibility.Collapsed;
            //}
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (controlPosition == null)
                controlPosition = this.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

            var mousePosition = Mouse.GetPosition(Application.Current.MainWindow);

            deltaX = mousePosition.X - controlPosition.Value.X;
            deltaY = mousePosition.Y - controlPosition.Value.Y;

            isMoving = true;
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            transform = this.RenderTransform as TranslateTransform;
            isMoving = false;
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (!isMoving) return;      

            var mousePoint = Mouse.GetPosition(Application.Current.MainWindow);

            var offsetX = (transform == null ? controlPosition.Value.X : controlPosition.Value.X - transform.X) + deltaX - mousePoint.X;
            var offsetY = (transform == null ? controlPosition.Value.Y : controlPosition.Value.Y - transform.Y) + deltaY - mousePoint.Y;

            this.RenderTransform = new TranslateTransform(-offsetX, -offsetY);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            FluentDialog.TaskCompletionSource?.TrySetResult(DialogResult.Delete);
            FluentDialog.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            FluentDialog.TaskCompletionSource?.TrySetResult(DialogResult.Cancel);
            FluentDialog.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            FluentDialog.TaskCompletionSource?.TrySetResult(DialogResult.OK);
            FluentDialog.Close();
        }

        private void Header_MouseEnter(object sender, MouseEventArgs e)
        {
         //   isMouseOverHeader = true;
        }

        private void Header_MouseLeave(object sender, MouseEventArgs e)
        {           
           // isMouseOverHeader = false;
        }

        private void SetButtonContent()
        {
            if (dialogOptions != null)
            {
                if (!string.IsNullOrEmpty(dialogOptions.OkButtonContent))
                    okButton.Content = dialogOptions.OkButtonContent;

                if (!string.IsNullOrEmpty(dialogOptions.DeleteButtonContent))
                    deleteButton.Content = dialogOptions.DeleteButtonContent;

                if (!string.IsNullOrEmpty(dialogOptions.CancelButtonContent))
                    cancelButton.Content = dialogOptions.CancelButtonContent;
            }
        }

        private void SetButtonVisibility()
        {
            if (dialogOptions != null)
            {
                okButton.Visibility = dialogOptions.OkButtonVisibility;
                cancelButton.Visibility = dialogOptions.CancelButtonVisibility;
                deleteButton.Visibility = dialogOptions.DeleteButtonVisibility;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            okButton = this.Template.FindName("PART_OKButton", this) as Button;
            okButton.Click += OK_Click;

            cancelButton = this.Template.FindName("PART_CancelButton", this) as Button;
            cancelButton.Click += CancelButton_Click;

            deleteButton = this.Template.FindName("PART_DeleteButton", this) as Button;
            deleteButton.Click += DeleteButton_Click;

            header = this.Template.FindName("PART_Header", this) as TextBlock;
            header.MouseEnter += Header_MouseEnter;
            header.MouseLeave += Header_MouseLeave;

            SetButtonContent();
            SetButtonVisibility();
        }
    }
}
