using System;

namespace CsvHelper.Configuration
{
    public static class CsvConfigurationHelpers {

        public static class MissingFieldFound {
            public static void Ignore (MissingFieldFoundArgs args) {
                args.Ignore();

            }
        }
    }

}
