using System.Windows;
namespace SnakeWPF
{
    public static class SizeObserver
    {
        public static readonly DependencyProperty ObserveProperty =
            DependencyProperty.RegisterAttached(
                "Observe",
                typeof(bool),
                typeof(SizeObserver),
                new PropertyMetadata(false, OnObserveChanged));

        public static bool GetObserve(DependencyObject obj)
            => (bool)obj.GetValue(ObserveProperty);

        public static void SetObserve(DependencyObject obj, bool value)
            => obj.SetValue(ObserveProperty, value);

        public static readonly DependencyProperty ObservedWidthProperty =
            DependencyProperty.RegisterAttached(
                "ObservedWidth",
                typeof(double),
                typeof(SizeObserver),
                new FrameworkPropertyMetadata(double.NaN,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static double GetObservedWidth(DependencyObject obj)
            => (double)obj.GetValue(ObservedWidthProperty);

        public static void SetObservedWidth(DependencyObject obj, double value)
            => obj.SetValue(ObservedWidthProperty, value);

        public static readonly DependencyProperty ObservedHeightProperty =
            DependencyProperty.RegisterAttached(
                "ObservedHeight",
                typeof(double),
                typeof(SizeObserver),
                new FrameworkPropertyMetadata(double.NaN,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static double GetObservedHeight(DependencyObject obj)
            => (double)obj.GetValue(ObservedHeightProperty);

        public static void SetObservedHeight(DependencyObject obj, double value)
            => obj.SetValue(ObservedHeightProperty, value);

        private static void OnObserveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                if ((bool)e.NewValue)
                    element.SizeChanged += OnSizeChanged;
                else
                    element.SizeChanged -= OnSizeChanged;
            }
        }

        private static void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                SetObservedWidth(element, element.ActualWidth);
                SetObservedHeight(element, element.ActualHeight);
            }
        }
    }
}