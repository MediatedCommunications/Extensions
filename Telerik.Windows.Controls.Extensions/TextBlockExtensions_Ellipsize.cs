using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Telerik.Windows.Controls {
    public static partial class TextBlockExtensions {

        private static class Ellipsize {
            internal static DependencyProperty TextProperty(DependencyObject This) {
                var ret = default(DependencyProperty?);

                if(This is Run V1) {
                    ret = Run.TextProperty;
                } else if (This is TextBlock V2) {
                    ret = TextBlock.TextProperty;
                } else {
                    throw new NotImplementedException();
                }

                return ret;
            }

            internal static void Resize(TextBlock This) {
                var Dip = VisualTreeHelper.GetDpi(This).PixelsPerDip;
                var TypeFace = new Typeface(This.FontFamily, This.FontStyle, This.FontWeight, This.FontStretch);
                var Content = GetEllipsizeText(This);
                var Position = GetEllipsizePosition(This);


                var Start = 0;
                var End = Content.Length;

                var NewContent = Strings.Empty;

                for (var i = End; i >= Start; i--) {

                    var SubText = Content.Ellipsize(i, Position);

                    var Formatted = new FormattedText(SubText, CultureInfo.CurrentCulture, This.FlowDirection, TypeFace, This.FontSize, This.Foreground, Dip);
                    if(Formatted.Width < This.ActualWidth) {
                        NewContent = SubText;
                        break;
                    }
                }

                var Property = TextProperty(This);

                This.SetValue(Property, NewContent);
            }
        }

    }

}
