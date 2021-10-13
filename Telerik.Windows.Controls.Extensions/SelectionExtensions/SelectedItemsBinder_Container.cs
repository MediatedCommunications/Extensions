using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Windows.Controls {
    public abstract partial class SelectedItemsBinder {

        public void Container_Events_Enable() {
            Container_Events_Enable(true);
        }

        public void Container_Events_Disable() {
            Container_Events_Enable(false);
        }

        public void Container_Events_Enable(bool Enable) {
            if (Container is INotifyCollectionChanged V1) {
                V1.CollectionChanged -= this.Container_CollectionChanged;
                if (Enable) {
                    V1.CollectionChanged += this.Container_CollectionChanged;
                }
            }
        }

        protected void Container_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            var NewItems = (e.NewItems ?? new List<object>()).OfType<object>().ToList();
            var OldItems = (e.OldItems ?? new List<object>()).OfType<object>().ToList();

            SelectedItems_Update(NewItems, OldItems, false, true);
        }


        protected List<object> Container_SelectedItems_Get() {
            var ret = new List<object>();

            if (Container is IList V1) {
                ret.AddRange(V1.OfType<object>());
            }


            return ret;
        }

        protected void Container_SelectedItems_Set(List<object> ItemsThatShouldBeSelected) {
            OperationList(ItemsThatShouldBeSelected, Container_SelectedItems_Get(), out var ItemsToAdd, out var ItemsToRemove);

            SelectedItems_Update_Internal(ItemsToAdd, ItemsToRemove, true, false);

        }

        protected void Container_SelectedItems_Add(IEnumerable<object> items) {
            if (Container is IList V1) {

                foreach (var item in items) {
                    V1.Add(item);
                }

            }
        }

        protected void Container_SelectedItems_Remove(IEnumerable<object> items) {
            if (Container is IList V1) {

                foreach (var item in items) {
                    V1.Remove(item);
                }
            }
        }


    }
}
