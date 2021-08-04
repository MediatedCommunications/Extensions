namespace System.Collections.Generic {
    public static class WithIndexItem {
        public static WithIndexItem<T> Create<T>(int Index, T Item) {
            return new WithIndexItem<T>(Index, Item);
        }
    }

}
