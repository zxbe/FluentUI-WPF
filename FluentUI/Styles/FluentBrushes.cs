using System.Windows.Media;

namespace FluentUI.Styles
{
    public static class FluentBrushes
    {
        static FluentBrushes()
        {
            NeutralGray160.Freeze();
        }

        public static SolidColorBrush NeutralGray160 { get; } = new SolidColorBrush(Color.FromRgb(50, 49, 48));
    }
}
