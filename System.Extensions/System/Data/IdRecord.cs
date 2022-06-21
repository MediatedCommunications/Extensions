namespace System.Data {
    public static class IdRecord {
        public static IdRecord<TKey> Create<TKey>(TKey Id) {
            return new IdRecord<TKey>
            {
                Id = Id
            };
        }
    }

}