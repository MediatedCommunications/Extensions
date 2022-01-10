namespace System.Data {
    public record IdReferenceJson<TKey> : IdResultJson<TKey> {

    }

    public record IdReferenceJson<TKey, TReference> : IdReferenceJson<TKey>  {
        
    }

    public record OptionalIdReferenceJson<TKey> : OptionalIdResultJson<TKey> {

    }

    public record OptionalIdReferenceJson<TKey, TReference> : OptionalIdResultJson<TKey> {

    }
}