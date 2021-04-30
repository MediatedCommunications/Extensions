using System;
using System.Windows;

namespace Telerik.Windows.Controls {
    public static partial class ButtonExtensions {
        public static readonly DependencyProperty ShellExecuteProperty = DependencyProperty.RegisterAttached(
         nameof(ShellExecute),
         typeof(string),
         typeof(ButtonExtensions),
         new PropertyMetadata("", ShellExecute.PropertyChanged)
         );



        public static void SetShellExecute(DependencyObject element, string value) {
            element.SetValue(ShellExecuteProperty, value);
        }
        public static string GetShellExecute(DependencyObject element) {
            return (string)element.GetValue(ShellExecuteProperty);
        }

        private static class ShellExecute {
            public static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {

                d.RemoveClickHandler(Button_Click);

                if (e.NewValue is string S && S.IsNotNullOrEmpty()) {
                    d.AddClickHandler(Button_Click);
                }
            }

            public static void Button_Click(object sender, RoutedEventArgs e) {

                if (sender is DependencyObject H) {
                    var URL = GetShellExecute(H);

                    if (URL.IsNotNullOrEmpty()) {
                        try {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                                FileName = URL,
                                UseShellExecute = true,
                            });

                        } catch (Exception ex) {
                            ex.Ignore();
                        }

                        e.Handled = true;
                    }

                }
            }

        }



        



       
    }

}
