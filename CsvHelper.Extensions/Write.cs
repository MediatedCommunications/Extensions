using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;

namespace CsvHelper
{
    public static class Write {

        public static string ToString<T>(IEnumerable<T> Records, CsvConfiguration? Config = default) {
            var SW = new StringWriter();
            ToTextWriter(SW, Records, Config);

            var ret = SW.ToString();
            return ret;
        }

        public static void ToFile<T>(string FileName, IEnumerable<T> Records, CsvConfiguration? Config = default) {
            using var FS = File.Create(FileName);
            ToStream(FS, Records, Config);
        }

        public static void ToStream<T>(Stream Output, IEnumerable<T> Records, CsvConfiguration? Config = default) {
            using var TW = new StreamWriter(Output);
            ToTextWriter(TW, Records, Config);
        }

        public static void ToTextWriter<T>(TextWriter TW, IEnumerable<T> Records, CsvConfiguration? Config = default) {
            var MyConfig = Config ?? DefaultConfiguration.Create();
            
            using var Writer = new CsvWriter(TW, MyConfig);
            Writer.WriteCsv(Records);
        }

    }

}
