using System.Collections.Generic;
using System.Linq;

namespace System {
    public static class Params {

        public static T[] Append<T>(params T[] V1) {
            return Append(V1.AsEnumerable());
        }

        public static T[] Append<T>(IEnumerable<T>? V1) {
            var ret = new List<T>() {
                V1.EmptyIfNull()
            }.OfType<T>().ToArray();

            return ret;
        }

        public static T[] Append<T>(IEnumerable<T>? V1, params T[] V2) {
            var ret = new List<T>() {
                V1.EmptyIfNull(),
                V2
            }.OfType<T>().ToArray();

            return ret;
        }

        public static T[] Prepend<T>(params T[] V1) {
            return Prepend(V1.AsEnumerable());
        }

        public static T[] Prepend<T>(IEnumerable<T>? V1) {
            var ret = new List<T>() {
                V1.EmptyIfNull()
            }.OfType<T>().ToArray();

            return ret;
        }

        public static T[] Prepend<T>(IEnumerable<T>? V2, params T[] V1) {
            var ret = new List<T>() {
                V1,
                V2.EmptyIfNull()
            }.OfType<T>().ToArray();

            return ret;
        }

    }
}
