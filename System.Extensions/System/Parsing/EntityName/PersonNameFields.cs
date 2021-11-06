namespace System {
    [Flags]
    public enum PersonNameFields {
        None,
        Prefix = 1,
        FirstName = 2,
        MiddleName = 4,
        LastName = 8,
        Suffix = 16,

        AllNames = FirstName | MiddleName | LastName,
        All = Prefix | FirstName | MiddleName | LastName | Suffix,
    }


}
