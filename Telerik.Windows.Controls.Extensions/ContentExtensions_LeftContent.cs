using System;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class ContentExtensions {

        public static readonly DependencyProperty LeftContentProperty = DependencyProperty.RegisterAttached(
            nameof(LeftContent),
            typeof(object),
            typeof(ContentExtensions),
            new PropertyMetadata(LeftContent.PropertyChanged)
        );

        public static void SetLeftContent(DependencyObject element, object value) {
            element.SetValue(LeftContentProperty, value);
        }
        public static object GetLeftContent(DependencyObject element) {
            return element.GetValue(LeftContentProperty);
        }

        private static class LeftContent {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                sender.Ignore();
                e.Ignore();
            }

        }


    }
}
