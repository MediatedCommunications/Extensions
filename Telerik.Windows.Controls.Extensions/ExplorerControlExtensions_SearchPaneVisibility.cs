using System;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class ExplorerControlExtensions {

        public static Visibility GetSearchPaneVisibility(FileDialogs.ExplorerControl obj) {
            return (Visibility)obj.GetValue(SearchPaneVisibilityProperty);
        }

        public static void SetSearchPaneVisibility(FileDialogs.ExplorerControl obj, Visibility value) {
            obj.SetValue(SearchPaneVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for Margin. This enables animation, styling, binding, etc…
        public static readonly DependencyProperty SearchPaneVisibilityProperty =
            DependencyProperty.RegisterAttached(
                nameof(SearchPaneVisibility),
                typeof(Visibility),
                typeof(ExplorerControlExtensions),
                new UIPropertyMetadata(Visibility.Visible, SearchPaneVisibility.PropertyChanged)
                );


        public static class SearchPaneVisibility {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                if (sender is FileDialogs.ExplorerControl P) {
                    P.Loaded -= Loaded;
                    P.Loaded += Loaded;

                    ApplyValues(sender);
                }

            }

            private static void Loaded(object sender, RoutedEventArgs e) {
                ApplyValues(sender);
            }

            private static void ApplyValues(object sender) {

                if (sender is FileDialogs.ExplorerControl explorer) {

                    if (Parts.SearchPane(explorer) is { } rootGrid) {

                        rootGrid.Visibility = GetSearchPaneVisibility(explorer);

                    }

                }
            }

        }

    }

}
