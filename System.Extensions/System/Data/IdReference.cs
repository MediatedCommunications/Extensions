namespace System.Data {
    public record IdReferenceJson<TKey> : IdRecord<TKey> {

    }

    public record IdReferenceJson<TKey, TReference> : IdReferenceJson<TKey>  {
        
    }

    public record OptionalIdReferenceJson<TKey> : OptionalIdRecord<TKey> {

    }

    public record OptionalIdReferenceJson<TKey, TReference> : OptionalIdRecord<TKey> {

    }

}