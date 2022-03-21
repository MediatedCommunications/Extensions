using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Telerik.Windows.Controls {
    public static partial class RadComboBoxExtensions2 {
        public static class Parts {
            public static TextBox? EditableTextBox(RadComboBox This) {
                var ret = This.ChildrenOfType<TextBox>()
                        .Where(x => x.Name == "PART_EditableTextBox")
                        .FirstOrDefault()
                        ;

                return ret;
            }

            public static Grid? VisualRoot(RadComboBox This) {
                var ret = This.ChildrenOfType<Grid>()
                        .Where(x => x.Name == "VisualRoot")
                        .FirstOrDefault()
                        ;

                return ret;
            }

            public static Popup? Popup(RadComboBox This) {
                var ret = This.ChildrenOfType<Popup>()
                        .Where(x => x.Name == "PART_Popup")
                        .FirstOrDefault()
                        ;

                return ret;
            }
        }
    }

}
