using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace Telerik.Windows.Controls {
    public static partial class PasswordBoxExtensions {

        public static readonly DependencyProperty PasswordProperty =
                   DependencyProperty.RegisterAttached(nameof(Password), typeof(string), typeof(PasswordBoxExtensions), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, Password.PropertyChanged, Password.CoerceValue));

        public static string GetPassword(DependencyObject dp) {
            return (string)dp.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dp, string value) {
            dp.SetValue(PasswordProperty, value);
        }

        private static class Password {

            public static object CoerceValue(DependencyObject d, object baseValue) {
                EnableEvents(d);

                return baseValue;
            }

            public static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
                var NewValue = (e.NewValue as string).Coalesce();

                SetPasswordInternal(d, NewValue);
            }


            private static void EnableEvents(object d) {
                EnableEvents(d, true);
            }

            private static void DisableEvents(object d) {
                EnableEvents(d, false);
            }

            private static void EnableEvents(object d, bool Enable) {
                if (d is PasswordBox PB) {
                    PB.PasswordChanged -= PasswordBox_PasswordChanged;
                    if (Enable) {
                        PB.PasswordChanged += PasswordBox_PasswordChanged;
                    }
                } else if (d is RadPasswordBox PB2) {
                    PB2.PasswordChanged -= PasswordBox_PasswordChanged;
                    if (Enable) {
                        PB2.PasswordChanged += PasswordBox_PasswordChanged;
                    }
                }


            }


            private static void SetValue(object d, string NewValue) {
                if (d is PasswordBox V1) {
                    V1.Password = NewValue;
                } else if (d is RadPasswordBox V2) {
                    V2.Password = NewValue;
                }
            }

            private static string GetValue(object d) {
                var ret = string.Empty;

                if (d is PasswordBox V1) {
                    ret = V1.Password;
                } else if (d is RadPasswordBox V2) {
                    ret = V2.Password;
                }

                return ret;
            }


            private static void SetPasswordInternal(object d, string NewValue) {

                DisableEvents(d);
                SetValue(d, NewValue);
                EnableEvents(d);

            }

            private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
                DisableEvents(sender);

                if (sender is DependencyObject V1) {
                    var Password = GetValue(sender);
                    SetPassword(V1, Password);
                }

                EnableEvents(sender);

            }

        }

    }

}
