using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Telerik.Windows.Controls {
    public static partial class ExplorerControlExtensions {
        public static bool GetIsFolderView(FileDialogs.ExplorerControl obj) {
            return (bool)obj.GetValue(IsFolderViewProperty);
        }

        public static void SetIsFolderView(FileDialogs.ExplorerControl obj, bool value) {
            obj.SetValue(IsFolderViewProperty, value);
        }

        // Using a DependencyProperty as the backing store for Margin. This enables animation, styling, binding, etc…
        public static readonly DependencyProperty IsFolderViewProperty =
            DependencyProperty.RegisterAttached(
                nameof(IsFolderView),
                typeof(bool),
                typeof(ExplorerControlExtensions),
                new UIPropertyMetadata(false, IsFolderView.PropertyChanged)
                );

        private static class IsFolderView {
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

                    if (Parts.ExplorerRootGrid(explorer) is { } rootGrid) {

                        
                        if (rootGrid.Children[2] is Grid { } internalGrid) {
                            var redundantElements = internalGrid.Children
                                .OfType<FrameworkElement>()
                                .Where(x => x.Name != "PART_TreeNavigationPane")
                                ;
                            foreach (var element in redundantElements) {
                                element.Visibility = Visibility.Collapsed;
                            }

                            internalGrid.ColumnDefinitions.Clear();
                            internalGrid.RowDefinitions.Clear();
                        }
                    }

                }
            }
        }

        

    }

}
