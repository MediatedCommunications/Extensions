using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Windows.Controls {
    public abstract partial class SelectedItemsBinder {
        protected object Container { get; }

        protected SelectedItemsBinder(object Container) {
            this.Container = Container;
        }


        private void OperationList(List<object> ItemsThatShouldBeSelected, List<object> CurrentlySelectedItems, out List<object> ItemsToAdd, out List<object> ItemsToRemove) {
            ItemsToAdd = (
                from x in ItemsThatShouldBeSelected
                where !CurrentlySelectedItems.Contains(x)
                select x
                ).ToList();

            ItemsToRemove = (
                from x in CurrentlySelectedItems
                where !ItemsThatShouldBeSelected.Contains(x)
                select x
                ).ToList();

        }

        public void Events_Enable() {
            Events_Enable(true);
        }

        public void Events_Disable() {
            Events_Enable(false);
        }

        public void Events_Enable(bool Enable) {
            this.Container_Events_Enable(Enable);
            this.Control_Events_Enable(Enable);
        }



        protected void SelectedItems_Set(IEnumerable<object>? SelectedItems, bool ForContainer, bool ForControl) {
            var ToSet = SelectedItems.EmptyIfNull().ToList();
            
            Container_Events_Disable();
            Control_Events_Disable();

            if (ForContainer) {
                Container_SelectedItems_Set(ToSet);
            }

            if (ForControl) {
                Control_SelectedItems_Set(ToSet);
            }



            Control_Events_Enable();
            Container_Events_Enable();
        }


        protected void SelectedItems_Update_Internal(IEnumerable<object>? Add, IEnumerable<object>? Remove, bool ForContainer, bool ForControl) {
            if (ForContainer) {
                Container_SelectedItems_Add(Add.EmptyIfNull());
            }

            if (ForControl) {
                Control_SelectedItems_Add(Add.EmptyIfNull());
            }


            if (ForContainer) {
                Container_SelectedItems_Remove(Remove.EmptyIfNull());
            }

            if (ForControl) {
                Control_SelectedItems_Remove(Remove.EmptyIfNull());
            }
        }

        protected void SelectedItems_Update(IEnumerable<object>? Add, IEnumerable<object>? Remove, bool ForContainer, bool ForControl) {
            Container_Events_Disable();
            Control_Events_Disable();

            SelectedItems_Update_Internal(Add, Remove, ForContainer, ForControl);

            Control_Events_Enable();
            Container_Events_Enable();

        }
    }

    public abstract class SelectedItemsBinder<TControl> : SelectedItemsBinder { 
        protected TControl Control { get; }

        public SelectedItemsBinder(TControl Control, object Container) : base(Container) {
            this.Control = Control;

            //Copy items from the container into the UI
            SelectedItems_Set(Container_SelectedItems_Get(), false, true);
        }

    }

}
