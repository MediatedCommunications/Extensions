using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Telerik.Windows.Controls {
    public static partial class ExplorerControlExtensions {

        public static Visibility GetPathPaneVisibility(FileDialogs.ExplorerControl obj) {
            return (Visibility)obj.GetValue(PathPaneVisibilityProperty);
        }

        public static void SetPathPaneVisibility(FileDialogs.ExplorerControl obj, Visibility value) {
            obj.SetValue(PathPaneVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for Margin. This enables animation, styling, binding, etc…
        public static readonly DependencyProperty PathPaneVisibilityProperty =
            DependencyProperty.RegisterAttached(
                nameof(PathPaneVisibility),
                typeof(Visibility),
                typeof(ExplorerControlExtensions),
                new UIPropertyMetadata(Visibility.Visible, PathPaneVisibility.PropertyChanged)
                );


        public static class PathPaneVisibility {

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

                    if (Parts.PathPane(explorer) is { } rootGrid) {

                        rootGrid.Visibility = GetPathPaneVisibility(explorer);

                    }

                }
            }

        }

    }

}
