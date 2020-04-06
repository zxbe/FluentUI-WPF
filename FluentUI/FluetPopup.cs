using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace FluentUI
{
    public class FluentPopup : Control
    {
        private Popup _popup;

        public static readonly DependencyProperty ContentProperty = 
            DependencyProperty.Register("Content", typeof(Control), typeof(FluentPopup));

        public Control Content
        {
            get { return (Control)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty = 
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(FluentPopup));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set 
            {
                SetValue(IsOpenProperty, value);
                _popup.IsOpen = IsOpen;
            }
        }

        static FluentPopup() { }

        public FluentPopup() { }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            IsOpen = false;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            _popup.IsOpen = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _popup = this.Template.FindName("popup", this) as Popup;           
        }
    }
}
