using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class ContentExtensions {

        public static readonly DependencyProperty MiddleContentTemplateProperty = DependencyProperty.RegisterAttached(
            nameof(MiddleContentTemplate),
            typeof(DataTemplate),
            typeof(ContentExtensions),
            new PropertyMetadata(MiddleContentTemplate.PropertyChanged)
        );

        public static void SetMiddleContentTemplate(DependencyObject element, DataTemplate value) {
            element.SetValue(MiddleContentTemplateProperty, value);
        }
        public static DataTemplate? GetMiddleContentTemplate(DependencyObject element) {
            return element.GetValue(MiddleContentTemplateProperty) as DataTemplate;
        }

        private static class MiddleContentTemplate {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {

            }

        }


    }
}
