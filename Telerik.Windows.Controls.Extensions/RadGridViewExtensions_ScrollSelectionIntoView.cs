using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls.GridView;

namespace Telerik.Windows.Controls {
    public static partial class RadGridViewExtensions {

        public static readonly DependencyProperty ScrollSelectionIntoViewProperty = DependencyProperty.RegisterAttached(
             nameof(ScrollSelectionIntoView),
             typeof(bool),
             typeof(RadGridViewExtensions),
             new PropertyMetadata(false, ScrollSelectionIntoView.PropertyChanged)
             );


        public static void SetScrollSelectionIntoView(GridViewDataControl element, bool value) {
            element.SetValue(ScrollSelectionIntoViewProperty, value);
        }

        public static bool GetScrollSelectionIntoView(GridViewDataControl element) {
            return (bool)element.GetValue(ScrollSelectionIntoViewProperty);
        }

        private static class ScrollSelectionIntoView {

            public static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
                if (d is GridViewDataControl G) {

                    G.SelectionChanged -= Handler;

                    if (e.NewValue is bool B && B) {
                        G.AddHandler(DataControl.SelectionChangedEvent, new EventHandler<SelectionChangeEventArgs>(Handler), true);
                    }
                }
            }


            private static void Handler(object? sender, SelectionChangeEventArgs e) {

                if (sender is GridViewDataControl G && e.AddedItems.Count > 0) {
                    G.ScrollIntoView(e.AddedItems.FirstOrDefault());
                }
            }

        }

       

    }
}
