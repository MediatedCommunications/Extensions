namespace System.Collections.Generic {



    public static class RangeExtensions {
        
        public static RangeEnumerable AsEnumerable(this Range This) {
            return new RangeEnumerable() {
                Range = This
            };
        }

    }

}
