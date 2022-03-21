namespace System.Data {
    public static class IdResultJson {
        public static IdResultJson<TKey> Create<TKey>(TKey Id) {
            return new IdResultJson<TKey>
            {
                Id = Id
            };
        }
    }

}