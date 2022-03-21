using System;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class RadComboBoxExtensions2 {

        public static readonly DependencyProperty EditableTextBoxVisibilityProperty = DependencyProperty.RegisterAttached(
            nameof(EditableTextBoxVisibility),
            typeof(Visibility),
            typeof(RadComboBoxExtensions2),
            new PropertyMetadata(Visibility.Visible, EditableTextBoxVisibility.PropertyChanged)
        );

        public static void SetEditableTextBoxVisibility(RadComboBox element, Visibility value) {
            element.SetValue(EditableTextBoxVisibilityProperty, value);
        }
        public static Visibility GetEditableTextBoxVisibility(RadComboBox element) {
            return (Visibility) element.GetValue(EditableTextBoxVisibilityProperty);
        }

        private static class EditableTextBoxVisibility {

            public static void PropertyChanged(object sender, DependencyPropertyChangedEventArgs e) {
                if(sender is RadComboBox { } V1) {
                    V1.Loaded -= Loaded;
                    V1.Loaded += Loaded;

                    ApplyValues(sender);
                }
            }

            private static void Loaded(object sender, RoutedEventArgs e) {
                ApplyValues(sender);
            }

            private static void ApplyValues(object sender) {

                if (sender is RadComboBox V1) {
                    if (Parts.EditableTextBox(V1) is { } EditableTextBox) {
                        EditableTextBox.Visibility = GetEditableTextBoxVisibility(V1);
                    }

                    if(Parts.VisualRoot(V1) is { } VisualRoot) {
                        VisualRoot.ColumnDefinitions[0].Width = new GridLength(0);
                    }

                    if (Parts.Popup(V1) is { } Popup) {
                        
                    }

                }
            }

        }


    }
}
