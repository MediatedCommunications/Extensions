using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Telerik.Windows.Controls {
    public static partial class TextBlockExtensions {

        public static readonly DependencyProperty EllipsizeTextProperty = DependencyProperty.RegisterAttached(
            nameof(EllipsizeText),
            typeof(string),
            typeof(TextBlockExtensions),
            new PropertyMetadata(string.Empty, EllipsizeText.PropertyChanged)
        );

        public static void SetEllipsizeText(TextBlock element, string value) {
            element.SetValue(EllipsizeTextProperty, value);
        }
        public static string GetEllipsizeText(TextBlock element) {
            return element.GetValue(EllipsizeTextProperty) as string ?? string.Empty;
        }

        private static class EllipsizeText {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                if (sender is TextBlock V1) {
                    V1.SizeChanged -= TextBlock_SizeChanged;
                    V1.SizeChanged += TextBlock_SizeChanged;
                    
                    if(V1.Parent is FrameworkElement V2) {
                        V2.SizeChanged -= Parent_SizeChanged;
                        V2.SizeChanged += Parent_SizeChanged;
                    }


                    TextBlockExtensions.Ellipsize.Resize(V1);
                }
            }

            private static void TextBlock_SizeChanged(object sender, SizeChangedEventArgs e) {
                if (sender is TextBlock V1) {
                    TextBlockExtensions.Ellipsize.Resize(V1);
                }
            }

            private static void Parent_SizeChanged(object sender, SizeChangedEventArgs e) {
                if (sender is TextBlock V1) {
                    TextBlockExtensions.Ellipsize.Resize(V1);
                }
            }

        }


    }
}
