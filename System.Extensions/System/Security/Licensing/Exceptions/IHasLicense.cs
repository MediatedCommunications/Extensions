namespace System.Security.Licensing {
    public interface IHasLicense<T> {
        public T License { get; }
    }

}
