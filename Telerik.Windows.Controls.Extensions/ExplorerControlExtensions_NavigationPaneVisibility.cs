using System;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class ExplorerControlExtensions {

        public static Visibility GetNavigationPaneVisibility(FileDialogs.ExplorerControl obj) {
            return (Visibility)obj.GetValue(NavigationPaneVisibilityProperty);
        }

        public static void SetNavigationPaneVisibility(FileDialogs.ExplorerControl obj, Visibility value) {
            obj.SetValue(NavigationPaneVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for Margin. This enables animation, styling, binding, etc…
        public static readonly DependencyProperty NavigationPaneVisibilityProperty =
            DependencyProperty.RegisterAttached(
                nameof(NavigationPaneVisibility),
                typeof(Visibility),
                typeof(ExplorerControlExtensions),
                new UIPropertyMetadata(Visibility.Visible, NavigationPaneVisibility.PropertyChanged)
                );


        public static class NavigationPaneVisibility {

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

                    if (Parts.NavigationPane(explorer) is { } rootGrid) {

                        rootGrid.Visibility = GetNavigationPaneVisibility(explorer);

                    }

                }
            }

        }

    }

}
