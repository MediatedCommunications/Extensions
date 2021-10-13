using System.Collections.Generic;
using System.Linq;

namespace Telerik.Windows.Controls {
    public abstract class DataControlSelectedItemsBinder<TControl> : SelectedItemsBinder<TControl> where TControl : DataControl {
        protected DataControlSelectedItemsBinder(TControl Control, object Container) : base(Control, Container) {

        }

        public override void Control_Events_Enable(bool Enable) {
            Control.SelectionChanged -= this.Control_SelectionChanged;
            if (Enable) {
                Control.SelectionChanged += this.Control_SelectionChanged;
            }
        }

        private void Control_SelectionChanged(object? sender, SelectionChangeEventArgs e) {
            SelectedItems_Update(e.AddedItems, e.RemovedItems, true, false);
        }

        protected override void Control_SelectedItems_Add(IEnumerable<object> items) {

            foreach (var item in items) {
                Control.SelectedItems.Add(item);
            }
            
        }
        protected override void Control_SelectedItems_Remove(IEnumerable<object> items) {

            foreach (var item in items) {
                Control.SelectedItems.Remove(item);
            }
        }

        protected override List<object> Control_SelectedItems_Get() {
            return Control.SelectedItems.ToList();
        }


    }

}
