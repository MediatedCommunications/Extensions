namespace System.Data {
    public static class CustomContent {
        public static T? GetContent<T>(object? Content) where T : class {
            var ret = default(T?);

            var tret = Content;
            
            while (tret is ICustomContent<T> V1) {
                tret = V1.GetNextContent();
                if(tret is T { } Result) {
                    ret = Result;
                }
            }

            return ret;
        }
    }

}