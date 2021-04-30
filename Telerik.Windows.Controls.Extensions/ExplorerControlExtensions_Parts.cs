using System;
using System.Linq;
using System.Windows.Controls;

namespace Telerik.Windows.Controls {
    public static partial class ExplorerControlExtensions {

        public static class Parts {
            public static Grid? ExplorerRootGrid(FileDialogs.ExplorerControl This) {
                var ret = This.ChildrenOfType<Grid>()
                        .Where(x => x.Name == "ExplorerRootGrid")
                        .FirstOrDefault()
                        ;

                return ret;
            }

            public static Grid? ConfigurationPane(FileDialogs.ExplorerControl This) {
                var ret = default(Grid?);

                if (ExplorerRootGrid(This) is { } V1) {
                    ret = V1.ChildrenOfType<Grid>()
                        .Where(x => x.Name == "ConfigurationPane")
                        .FirstOrDefault()
                        ;
                }


                return ret;

            }

            public static Grid? NavigationPane(FileDialogs.ExplorerControl This) {
                var ret = default(Grid?);

                if (ExplorerRootGrid(This) is { } V1) {
                    ret = V1.ChildrenOfType<Grid>()
                        .Where(x => x.Name.IsBlank())
                        .Skip(0)
                        .Take(1)
                        .FirstOrDefault()
                        ;
                }


                return ret;

            }

            public static Control? HistoryPane(FileDialogs.ExplorerControl This) {
                var ret = default(Control?);

                if (NavigationPane(This) is { } V1) {
                    ret = V1.ChildrenOfType<Control>()
                        .Where(x => x.Name == "PART_HistoryNavigationPane")
                        .FirstOrDefault()
                        ;
                }


                return ret;

            }

            public static Control? PathPane(FileDialogs.ExplorerControl This) {
                var ret = default(Control?);

                if (NavigationPane(This) is { } V1) {
                    ret = V1.ChildrenOfType<Control>()
                        .Where(x => x.Name == "PART_PathNavigationPane")
                        .FirstOrDefault()
                        ;
                }


                return ret;

            }


            public static Control? SearchPane(FileDialogs.ExplorerControl This) {
                var ret = default(Control?);

                if (NavigationPane(This) is { } V1) {
                    ret = V1.ChildrenOfType<Control>()
                        .Where(x => x.Name == "PART_SearchPane")
                        .FirstOrDefault()
                        ;
                }


                return ret;

            }

        }

    }
}
