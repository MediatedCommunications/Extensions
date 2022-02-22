using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Telerik.Windows.Controls {
    public abstract class MultiSelectorSelectedItemsBinder<TControl> : SelectedItemsBinder<TControl> where TControl : MultiSelector {
        public MultiSelectorSelectedItemsBinder(TControl Control, object Container) : base(Control, Container) {

        }

        public override void Control_Events_Enable(bool Enable) {
            Control.SelectionChanged -= this.Control_SelectionChanged;

            if (Enable) {
                Control.SelectionChanged += this.Control_SelectionChanged;
            }

        }

        private void Control_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            SelectedItems_Update(e.AddedItems.OfType<object>(), e.RemovedItems.OfType<object>(), true, false);
        }

        protected override void Control_SelectedItems_Add(IEnumerable<object> items) {
            foreach (var item in items) {
                Control.SelectedItems.Add(item);
            }
        }

        protected override List<object> Control_SelectedItems_Get() {
            return Control.SelectedItems.OfType<object>().ToList();
        }

        protected override void Control_SelectedItems_Remove(IEnumerable<object> items) {

            foreach (var item in items) {
                Control.SelectedItems.Remove(item);
            }

        }
    }


}
