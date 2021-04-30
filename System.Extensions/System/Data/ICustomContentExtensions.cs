namespace System.Data {
    public static class ICustomContentExtensions {
        public static T? GetContent<T>(this ICustomContent<T>? This) where T:class {
            var ret = CustomContent.GetContent<T>(This);

            return ret;
        }
    }
}
