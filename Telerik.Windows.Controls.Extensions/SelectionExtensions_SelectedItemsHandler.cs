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
                    RadGridView x => CreateBinderFor(x, Container),
                    RadComboBox x => CreateBinderFor(x, Container),
                    RadListBox x => CreateBinderFor(x, Container),
                    _ => throw new NotImplementedException(),
                };

                return ret;
            }

            public static SelectedItemsBinder CreateBinderFor(RadGridView Item, object Container) {
                return new RadGridViewSelectedItemsBinder(Item, Container);
            }

            public static SelectedItemsBinder CreateBinderFor(RadComboBox Item, object Container) {
                return new RadComboBoxSelectedItemsBinder(Item, Container);
            }

            public static SelectedItemsBinder CreateBinderFor(RadListBox Item, object Container) {
                return new RadListBoxSelectedItemsBinder(Item, Container);
            }

        }

    }
}
