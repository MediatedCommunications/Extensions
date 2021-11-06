namespace Microsoft.Data.SqlClient {
    public enum SqlDatabaseStatus {
        Online = 0,
        Restoring = 1,
        Recovering = 2,
        RecoveryPending = 3,
        Suspect = 4,
        Emergency  = 5,
        Offline = 6,
        Copying = 7,
        Secondary = 10,
    }

}
