using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class SelectionExtensions {
        public static readonly DependencyProperty SelectedItemsHandlerProperty = DependencyProperty.RegisterAttached(
            nameof(SelectedItemsHandler),
            typeof(SelectedItemsBinder),
            typeof(SelectionExtensions)
            );

        public static SelectedItemsBinder? GetSelectedItemsHandler(this DependencyObject obj) {
            return (SelectedItemsBinder)obj.GetValue(SelectedItemsHandlerProperty);
        }

        public static void SetSelectedItemsHandler(this DependencyObject obj, SelectedItemsBinder? value) {
            obj.SetValue(SelectedItemsHandlerProperty, value);
        }

        private static class SelectedItemsHandler {

            public static SelectedItemsBinder CreateBinderFor(object O, object Container) {
                var ret = O switch {
                    RadGridView V1 => CreateBinderFor(V1, Container),
                    _ => throw new NotImplementedException(),
                };

                return ret;
            }

            public static SelectedItemsBinder CreateBinderFor(RadGridView V1, object Container) {
                return new RadGridViewSelectedItemsBinder(V1, Container);
            }



        }

    }
}
