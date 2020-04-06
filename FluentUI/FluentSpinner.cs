using System.Windows;
using System.Windows.Controls;

namespace FluentUI
{
    public class FluentSpinner : Control
    {       
        public static readonly DependencyProperty SpinnerSizeProperty =
          DependencyProperty.Register("SpinnerSize", typeof(FluentSpinnerSize), typeof(FluentSpinner), new PropertyMetadata(FluentSpinnerSize.Normal));

        public static readonly DependencyProperty SpinnerGeometryProperty =
          DependencyProperty.Register("SpinnerGeometry", typeof(Rect), typeof(FluentSpinner), new PropertyMetadata(new Rect(0, 20,20,20)));

        public static readonly DependencyProperty LabelProperty =
           DependencyProperty.Register("Label", typeof(string), typeof(FluentSpinner));       

        public static readonly DependencyProperty SpinnerPositionProperty =
           DependencyProperty.Register("SpinnerPosition", typeof(Dock), typeof(FluentSpinner));            

        public FluentSpinnerSize SpinnerSize
        {
            get { return (FluentSpinnerSize)GetValue(SpinnerSizeProperty); }
            set { SetValue(SpinnerSizeProperty, value); }
        }

        public Rect SpinnerGeometry
        {
            get { return (Rect)GetValue(SpinnerGeometryProperty); }
            set { SetValue(SpinnerGeometryProperty, value); }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }      

        public Dock SpinnerPosition
        {
            get { return (Dock)GetValue(SpinnerPositionProperty); }
            set { SetValue(SpinnerPositionProperty, value); }
        }

        static FluentSpinner() { }       
    }
}
