using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace Telerik.Windows.Controls {
    public static partial class ButtonExtensions {

        public static bool SetClickHandler(this DependencyObject d, RoutedEventHandler MyEvent) {
            d.RemoveClickHandler(MyEvent);
            var ret = d.AddClickHandler(MyEvent);
            return ret;
        }


        public static bool RemoveClickHandler(this DependencyObject d, RoutedEventHandler MyEvent) {
            var ret = true;
            if (d is Hyperlink H) {
                H.Click -= MyEvent;
            } else if (d is ButtonBase B) {
                B.Click -= MyEvent;
            } else if (d is RadMenuItem R) {
                R.Click -= new RadRoutedEventHandler(MyEvent);
            } else if (d is MenuItem M) {
                M.Click -= MyEvent;
            } else {
                ret = false;
            }

            return ret;
        }

        public static bool AddClickHandler(this DependencyObject d, RoutedEventHandler MyEvent) {
            var ret = true;
            if (d is Hyperlink H) {
                H.Click += MyEvent;
            } else if (d is ButtonBase B) {
                B.Click += MyEvent;
            } else if (d is RadMenuItem R) {
                R.Click += new RadRoutedEventHandler(MyEvent);
            } else if (d is MenuItem M) {
                M.Click += MyEvent;
            } else {
                ret = false;
            }

            return ret;
        }

       
    }

}
