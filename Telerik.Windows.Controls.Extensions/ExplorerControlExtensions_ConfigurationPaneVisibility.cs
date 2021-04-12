﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Telerik.Windows.Controls {
    public static partial class ExplorerControlExtensions {

        public static Visibility GetConfigurationPaneVisibility(FileDialogs.ExplorerControl obj) {
            return (Visibility)obj.GetValue(ConfigurationPaneVisibilityProperty);
        }

        public static void SetConfigurationPaneVisibility(FileDialogs.ExplorerControl obj, Visibility value) {
            obj.SetValue(ConfigurationPaneVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for Margin. This enables animation, styling, binding, etc…
        public static readonly DependencyProperty ConfigurationPaneVisibilityProperty =
            DependencyProperty.RegisterAttached(
                nameof(ConfigurationPaneVisibility),
                typeof(Visibility),
                typeof(ExplorerControlExtensions),
                new UIPropertyMetadata(Visibility.Visible, ConfigurationPaneVisibility.PropertyChanged)
                );


        public static class ConfigurationPaneVisibility {

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

                    if (Parts.ConfigurationPane(explorer) is { } rootGrid) {

                        rootGrid.Visibility = GetConfigurationPaneVisibility(explorer);

                    }

                }
            }

        }

    }

}