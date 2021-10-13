using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class SelectionExtensions {
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached(
            nameof(SelectedItems),
            typeof(INotifyCollectionChanged),
            typeof(SelectionExtensions),
            new PropertyMetadata(null, SelectedItems.PropertyChanged)
            );

        public static INotifyCollectionChanged? GetSelectedItems(this DependencyObject obj) {
            return (INotifyCollectionChanged)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(this DependencyObject obj, INotifyCollectionChanged? value) {
            obj.SetValue(SelectedItemsProperty, value);
        }

        private static class SelectedItems {
            public static void PropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args) {
                {
                    if (target.GetSelectedItemsHandler() is { } V1) {
                        V1.Events_Disable();
                    }
                }

                var NewValue = default(SelectedItemsBinder?);

                {
                    if (args.NewValue is { } V1) {
                        NewValue = SelectedItemsHandler.CreateBinderFor(target, V1);
                    }
                }

                target.SetSelectedItemsHandler(NewValue);


            }
        }

    }
}
