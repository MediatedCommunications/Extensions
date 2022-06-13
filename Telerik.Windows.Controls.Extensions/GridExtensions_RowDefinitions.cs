using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace Telerik.Windows.Controls {

    public static partial class GridExtensions {

        public static readonly DependencyProperty RowDefinitionsProperty = DependencyProperty.RegisterAttached(
      nameof(RowDefinitions),
      typeof(object),
      typeof(GridExtensions),
      new PropertyMetadata(RowDefinitions.PropertyChanged)
  );

        public static void SetRowDefinitions(Grid element, string value) {
            element.SetValue(RowDefinitionsProperty, value);
        }
        public static string? GetRowDefinitions(Grid element) {
            return element.GetValue(RowDefinitionsProperty) as string;
        }

        private static class RowDefinitions {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                if (sender is Grid { } V1) {
                    V1.RowDefinitions.Clear();

                    if (e.NewValue is string { } NewValue) {
                        foreach (var Value in GridLengthParser.Parse(NewValue)) {
                            var Row = new RowDefinition()
                            {
                                Height = Value
                            };

                            V1.RowDefinitions.Add(Row);
                        }
                    }
                }

            }
        }

    }

}
