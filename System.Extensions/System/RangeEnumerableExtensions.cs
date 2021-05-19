namespace System.Collections.Generic {
    public static class RangeEnumerableExtensions {
        public static RangeEnumerable StartIs(this RangeEnumerable This, RangeEndpoint Value) {
            return This with {
                StartIs = Value,
            };
        }

        public static RangeEnumerable EndIs(this RangeEnumerable This, RangeEndpoint Value) {
            return This with {
                EndIs = Value,
            };
        }
    }

}
