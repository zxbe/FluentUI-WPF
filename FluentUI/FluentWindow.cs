using System;
using System.Windows;
using System.Windows.Controls;

namespace FluentUI
{
    [TemplatePart(Name = PART_Content, Type = typeof(ContentControl))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_MaximizeButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_RestoreButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_MinimizeButton, Type = typeof(Button))]
    public class FluentWindow : Window
    {
        private const string PART_Content = "PART_Content";
        private const string PART_CloseButton = "PART_CloseButton";
        private const string PART_RestoreButton = "PART_RestoreButton";
        private const string PART_MinimizeButton = "PART_MinimizeButton";
        private const string PART_MaximizeButton = "PART_MaximizeButton";

        public static readonly DependencyProperty TitleAreaProperty =
          DependencyProperty.Register("TitleArea", typeof(Control), typeof(FluentWindow));

        public static readonly DependencyProperty ChromeWindowIconProperty =
          DependencyProperty.Register("ChromeWindowIcon", typeof(Control), typeof(FluentWindow));

        public Control TitleArea
        {
            get { return (Control)GetValue(TitleAreaProperty); }
            set { SetValue(TitleAreaProperty, value); }
        }

        public Control ChromeWindowIcon
        {
            get { return (Control)GetValue(ChromeWindowIconProperty); }
            set { SetValue(ChromeWindowIconProperty, value); }
        }

        static FluentWindow()
        {
            DefaultStyleKeyProperty
                .OverrideMetadata(typeof(FluentWindow), new FrameworkPropertyMetadata(typeof(FluentWindow)));
        }

        public FluentWindow()
        {

        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    this.Margin = new Thickness(0, 5, 0, -5);
                    break;
                case WindowState.Normal:
                    this.Margin = new Thickness(0, 0, 0, 0);
                    break;
            }
        }       

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            base.WindowState = WindowState.Maximized;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            base.WindowState = WindowState.Minimized;
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            base.WindowState = WindowState.Normal;
        }

        public new virtual void Close()
        {
            base.Close();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var closeButton = GetTemplateChild(PART_CloseButton) as Button;
            closeButton.Click += CloseButton_Click;

            var minimizeButton = GetTemplateChild(PART_MinimizeButton) as Button;
            minimizeButton.Click += MinimizeButton_Click;

            var restoreButton = GetTemplateChild(PART_RestoreButton) as Button;
            restoreButton.Click += RestoreButton_Click;

            var maximizeButton = GetTemplateChild(PART_MaximizeButton) as Button;
            maximizeButton.Click += MaximizeButton_Click;
        }
    }
}
