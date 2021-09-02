using System;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class ContentExtensions {

        public static readonly DependencyProperty LeftContentTemplateProperty = DependencyProperty.RegisterAttached(
            nameof(LeftContentTemplate),
            typeof(DataTemplate),
            typeof(ContentExtensions),
            new PropertyMetadata(LeftContentTemplate.PropertyChanged)
        );

        public static void SetLeftContentTemplate(DependencyObject element, DataTemplate value) {
            element.SetValue(LeftContentTemplateProperty, value);
        }
        public static DataTemplate? GetLeftContentTemplate(DependencyObject element) {
            return element.GetValue(LeftContentTemplateProperty) as DataTemplate;
        }

        private static class LeftContentTemplate {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                sender.Ignore();
                e.Ignore();
            }

        }


    }
}
