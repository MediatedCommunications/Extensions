using System.Collections.ObjectModel;

namespace System.Collections.Generic {
    public static class EnumerableExtensions_ToCollections {
        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T>? This) {
            var ret = new LinkedList<T>(This.Coalesce());

            return ret;
        }

        public static Queue<T> ToStack<T>(this IEnumerable<T>? This) {
            var ret = new Queue<T>(This.Coalesce());

            return ret;
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T>? This) {
            var ret = new Queue<T>(This.Coalesce());

            return ret;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T>? This) {
            return new ObservableCollection<T>(This.Coalesce());
        }

    }

}
