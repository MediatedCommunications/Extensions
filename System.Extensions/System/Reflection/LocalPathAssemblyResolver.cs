namespace System.Reflection {
    public static class LocalPathAssemblyResolver {
        public static PathAssemblyResolver Default { get; private set; } = new PathAssemblyResolver(Assembly.GetEntryAssembly());
    }


}
