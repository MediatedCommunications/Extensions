using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class ContentExtensions {

        public static readonly DependencyProperty MiddleContentProperty = DependencyProperty.RegisterAttached(
            nameof(MiddleContent),
            typeof(object),
            typeof(ContentExtensions),
            new PropertyMetadata(MiddleContent.PropertyChanged)
        );

        public static void SetMiddleContent(DependencyObject element, object value) {
            element.SetValue(MiddleContentProperty, value);
        }
        public static object GetMiddleContent(DependencyObject element) {
            return element.GetValue(MiddleContentProperty);
        }

        private static class MiddleContent {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {

            }

        }


    }
}
