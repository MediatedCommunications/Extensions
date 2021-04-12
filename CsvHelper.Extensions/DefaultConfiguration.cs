using CsvHelper.Configuration;

namespace CsvHelper {
    static class DefaultConfiguration {
        public static CsvConfiguration Create() {
            var ret = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture) {

            };

            return ret;
        }
    }

}
