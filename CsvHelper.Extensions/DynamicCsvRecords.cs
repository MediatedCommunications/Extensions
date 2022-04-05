namespace CsvHelper {
    public static class DynamicCsvRecords {
        public static DynamicCsvRecord None { get; }

        static DynamicCsvRecords() {
            None = new();
        }
    }


}
