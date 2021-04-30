using System;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class ExplorerControlExtensions {

        public static Visibility GetHistoryPaneVisibility(FileDialogs.ExplorerControl obj) {
            return (Visibility)obj.GetValue(HistoryPaneVisibilityProperty);
        }

        public static void SetHistoryPaneVisibility(FileDialogs.ExplorerControl obj, Visibility value) {
            obj.SetValue(HistoryPaneVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for Margin. This enables animation, styling, binding, etc…
        public static readonly DependencyProperty HistoryPaneVisibilityProperty =
            DependencyProperty.RegisterAttached(
                nameof(HistoryPaneVisibility),
                typeof(Visibility),
                typeof(ExplorerControlExtensions),
                new UIPropertyMetadata(Visibility.Visible, HistoryPaneVisibility.PropertyChanged)
                );


        public static class HistoryPaneVisibility {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                if (sender is FileDialogs.ExplorerControl P) {
                    P.Loaded -= Loaded;
                    P.Loaded += Loaded;

                    ApplyValues(sender);
                }

            }

            private static void Loaded(object sender, EventArgs e) {
                ApplyValues(sender);
            }

            private static void ApplyValues(object sender) {

                if (sender is FileDialogs.ExplorerControl explorer) {

                    if (Parts.HistoryPane(explorer) is { } rootGrid) {

                        rootGrid.Visibility = GetHistoryPaneVisibility(explorer);

                    }

                }
            }

        }

    }

}
