using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Telerik.Windows.Controls {
    public static partial class TextBlockExtensions {

        public static readonly DependencyProperty EllipsizePositionProperty = DependencyProperty.RegisterAttached(
            nameof(EllipsizePosition),
            typeof(System.EllipsizePosition),
            typeof(TextBlockExtensions),
            new PropertyMetadata(System.EllipsizePosition.None, EllipsizePosition.PropertyChanged)
        );

        public static void SetEllipsizePosition(TextBlock element, System.EllipsizePosition value) {
            element.SetValue(EllipsizePositionProperty, value);
        }
        public static System.EllipsizePosition GetEllipsizePosition(TextBlock element) {
            return (System.EllipsizePosition) element.GetValue(EllipsizePositionProperty);
        }

        private static class EllipsizePosition {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                if (sender is TextBlock V1) {
                    TextBlockExtensions.Ellipsize.Resize(V1);
                }
            }

        }


    }
}
