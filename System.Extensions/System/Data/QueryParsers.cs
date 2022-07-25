namespace System.Data {
    public static class QueryParsers {
        public static QueryParser SqlServer { get; }
        public static QueryParser Odbc { get; }
        public static QueryParser Csv { get; }

        static QueryParsers() {
            SqlServer = new QueryParser_SqlServer();
            Odbc = new QueryParser_Odbc();
            Csv = new QueryParser_Csv();
        }
    }

}
