using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FluentUI
{
    [TemplateVisualState(Name = "Large", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Small", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class FluentProgressRing : Control
    {
        public static readonly DependencyProperty BindableWidthProperty = DependencyProperty.Register("BindableWidth", typeof(double), typeof(FluentProgressRing), new PropertyMetadata(default(double), BindableWidthCallback));

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(FluentProgressRing), new PropertyMetadata(true, IsActiveChanged));

        public static readonly DependencyProperty IsLargeProperty = DependencyProperty.Register("IsLarge", typeof(bool), typeof(FluentProgressRing), new PropertyMetadata(true, IsLargeChangedCallback));

        public static readonly DependencyProperty MaxSideLengthProperty = DependencyProperty.Register("MaxSideLength", typeof(double), typeof(FluentProgressRing), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty EllipseDiameterProperty = DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(FluentProgressRing), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty EllipseOffsetProperty = DependencyProperty.Register("EllipseOffset", typeof(Thickness), typeof(FluentProgressRing), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty EllipseDiameterScaleProperty = DependencyProperty.Register("EllipseDiameterScale", typeof(double), typeof(FluentProgressRing), new PropertyMetadata(1D));

        private List<Action> _deferredActions = new List<Action>();

        static FluentProgressRing()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentProgressRing), new FrameworkPropertyMetadata(typeof(FluentProgressRing)));
            VisibilityProperty.OverrideMetadata(typeof(FluentProgressRing),
                                                new FrameworkPropertyMetadata(
                                                    new PropertyChangedCallback(
                                                        (ringObject, e) => {
                                                            if (e.NewValue != e.OldValue)
                                                            {
                                                                var ring = (FluentProgressRing)ringObject;
                                                                //auto set IsActive to false if we're hiding it.
                                                                if ((Visibility)e.NewValue != Visibility.Visible)
                                                                {
                                                                    //sets the value without overriding it's binding (if any).
                                                                    ring.SetCurrentValue(FluentProgressRing.IsActiveProperty, false);
                                                                }
                                                                else
                                                                {
                                                                    // #1105 don't forget to re-activate
                                                                    ring.SetCurrentValue(FluentProgressRing.IsActiveProperty, true);
                                                                }
                                                            }
                                                        })));
        }

        public FluentProgressRing()
        {
            SizeChanged += OnSizeChanged;
        }

        public double MaxSideLength
        {
            get { return (double)GetValue(MaxSideLengthProperty); }
            private set { SetValue(MaxSideLengthProperty, value); }
        }

        public double EllipseDiameter
        {
            get { return (double)GetValue(EllipseDiameterProperty); }
            private set { SetValue(EllipseDiameterProperty, value); }
        }

        public double EllipseDiameterScale
        {
            get { return (double)GetValue(EllipseDiameterScaleProperty); }
            set { SetValue(EllipseDiameterScaleProperty, value); }
        }

        public Thickness EllipseOffset
        {
            get { return (Thickness)GetValue(EllipseOffsetProperty); }
            private set { SetValue(EllipseOffsetProperty, value); }
        }

        public double BindableWidth
        {
            get { return (double)GetValue(BindableWidthProperty); }
            private set { SetValue(BindableWidthProperty, value); }
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public bool IsLarge
        {
            get { return (bool)GetValue(IsLargeProperty); }
            set { SetValue(IsLargeProperty, value); }
        }

        private static void BindableWidthCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as FluentProgressRing;
            if (ring == null)
                return;

            var action = new Action(() =>
            {

                ring.SetEllipseDiameter((double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetEllipseOffset((double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetMaxSideLength((double)dependencyPropertyChangedEventArgs.NewValue);
            });

            if (ring._deferredActions != null)
                ring._deferredActions.Add(action);
            else
                action();
        }

        private void SetMaxSideLength(double width)
        {
            SetCurrentValue(MaxSideLengthProperty, width <= 20 ? 20 : width);
        }

        private void SetEllipseDiameter(double width)
        {
            SetCurrentValue(EllipseDiameterProperty, (width / 8) * EllipseDiameterScale);
        }

        private void SetEllipseOffset(double width)
        {
            SetCurrentValue(EllipseOffsetProperty, new Thickness(0, width / 2, 0, 0));
        }

        private static void IsLargeChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as FluentProgressRing;
            if (ring == null)
                return;

            ring.UpdateLargeState();
        }

        private void UpdateLargeState()
        {
            Action action;

            if (IsLarge)
                action = () => VisualStateManager.GoToState(this, "Large", true);
            else
                action = () => VisualStateManager.GoToState(this, "Small", true);

            if (_deferredActions != null)
                _deferredActions.Add(action);

            else
                action();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            SetCurrentValue(BindableWidthProperty, ActualWidth);
        }

        private static void IsActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as FluentProgressRing;
            if (ring == null)
                return;

            ring.UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            Action action;

            if (IsActive)
                action = () => VisualStateManager.GoToState(this, "Active", true);
            else
                action = () => VisualStateManager.GoToState(this, "Inactive", true);

            if (_deferredActions != null)
                _deferredActions.Add(action);

            else
                action();
        }

        public override void OnApplyTemplate()
        {
            //make sure the states get updated
            UpdateLargeState();
            UpdateActiveState();
            base.OnApplyTemplate();
            if (_deferredActions != null)
                foreach (var action in _deferredActions)
                    action();
            _deferredActions = null;
        }
    }
}
