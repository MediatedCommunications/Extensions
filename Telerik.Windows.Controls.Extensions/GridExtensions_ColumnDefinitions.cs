using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace Telerik.Windows.Controls {

    public static partial class GridExtensions {

        public static readonly DependencyProperty ColumnDefinitionsProperty = DependencyProperty.RegisterAttached(
      nameof(ColumnDefinitions),
      typeof(object),
      typeof(GridExtensions),
      new PropertyMetadata(ColumnDefinitions.PropertyChanged)
  );

        public static void SetColumnDefinitions(Grid element, string value) {
            element.SetValue(ColumnDefinitionsProperty, value);
        }
        public static string? GetColumnDefinitions(Grid element) {
            return element.GetValue(ColumnDefinitionsProperty) as string;
        }

        private static class ColumnDefinitions {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                if (sender is Grid { } V1 && e.NewValue is string { } NewValue) {
                    V1.ColumnDefinitions.Clear();

                    foreach (var Value in GridLengthParser.Parse(NewValue)) {
                        var Column = new ColumnDefinition()
                        {
                            Width = Value
                        };

                        V1.ColumnDefinitions.Add(Column);


                    }
                }

            }
        }

    }

}
