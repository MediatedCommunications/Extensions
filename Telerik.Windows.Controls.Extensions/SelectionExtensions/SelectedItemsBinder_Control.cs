using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Windows.Controls {
    public abstract partial class SelectedItemsBinder {
        public void Control_Events_Enable() {
            Control_Events_Enable(true);
        }

        public void Control_Events_Disable() {
            Control_Events_Enable(false);
        }

        public abstract void Control_Events_Enable(bool Enable);

        protected abstract List<object> Control_SelectedItems_Get();

        protected void Control_SelectedItems_Set(List<object> ItemsThatShouldBeSelected) {
            OperationList(ItemsThatShouldBeSelected, Control_SelectedItems_Get(), out var ItemsToAdd, out var ItemsToRemove);

            SelectedItems_Update_Internal(ItemsToAdd, ItemsToRemove, false, true);

        }

        protected abstract void Control_SelectedItems_Add(IEnumerable<object> items);
        protected abstract void Control_SelectedItems_Remove(IEnumerable<object> item);

        
    }
}
