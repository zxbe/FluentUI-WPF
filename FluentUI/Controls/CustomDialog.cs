using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Controls
{
    internal class CustomDialog : Control
    {
        public static readonly DependencyProperty CustomContentProperty =
            DependencyProperty.Register("CustomContent", typeof(object), typeof(CustomDialog));

        public object CustomContent
        {
            get { return (object)GetValue(CustomContentProperty); }
            set { SetValue(CustomContentProperty, value); }
        }

        public CustomDialog(object content)
        {
            CustomContent = content;
        }
    }
}
