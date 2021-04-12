using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls.GridView;

namespace Telerik.Windows.Controls {
    public static partial class FilteringControlExtensions {

        public static readonly DependencyProperty ColumnProperty = DependencyProperty.RegisterAttached(
            nameof(Column),
            typeof(object),
            typeof(FilteringControlExtensions),
            new PropertyMetadata(Column.PropertyChanged)
        );

        public static void SetColumn(DependencyObject element, object value) {
            element.SetValue(ColumnProperty, value);
        }
        public static object GetColumn(DependencyObject element) {
            return (object) element.GetValue(ColumnProperty);
        }

        private static class Column {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                if(sender is FilteringControl V1 && e.NewValue is GridViewColumn { } V2) {
                    V1.Prepare(V2);
                }
            }

        }


    }
}
