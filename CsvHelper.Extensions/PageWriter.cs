using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CsvHelper {

    public record PageWriterOptions : DisplayRecord {
        public string FolderPath { get; init; } = string.Empty;
        public string FileName { get; init; } = string.Empty;
        public string FileNameExtension { get; init; } = "csv";

        public bool Page { get; init; }
        public string Page_Suffix { get; init; } = @" - {0:0000}";
        public long Page_ItemCount_Max { get; init; }
        public long Page_FileSize_Max { get; init; }
    }

    public static class PageWriter {
        public static void Write<TCsv>(PageWriterOptions Options, IEnumerable<TCsv> Records) {
            using var Writer = new PageWriter<TCsv>(Options);

            Writer.Write(Records);

        }
    }

    public class PageWriter<TCsv> : IDisposable {
        protected PageWriterOptions Options { get; }

        public PageWriter(PageWriterOptions Options) {
            this.Options = Options;
        }

        public void Close() {
            Writer?.Flush();
            Writer?.Dispose();
            TW?.Close();
            FS?.Close();

            Writer = default;
            TW = default;
            FS = default;

            this.CurrentFileName = string.Empty;
        }


        public void Write(IEnumerable<TCsv> Records) {
            foreach (var Record in Records) {
                Write(Record);
            }
        }

        public void Write(TCsv Record) {
            if (FS == default) {
                Directory.CreateDirectory(Options.FolderPath);

                var FN = Options.FileName;

                if (Options.Page) {
                    var FNTag = string.Format(Options.Page_Suffix, this.CurrentPageIndex);
                    FN += FNTag;
                }

                if (Options.FileNameExtension.IsNotBlank()) {
                    FN += $@".{Options.FileNameExtension}";
                }

                FN = System.IO.Path.Combine(Options.FolderPath, FN);


                FS = File.Create(FN);
                TW = new StreamWriter(FS);
                Writer = new CsvWriter(TW, System.Globalization.CultureInfo.InvariantCulture);

                Writer.WriteHeader<TCsv>();
                Writer.NextRecord();

                CurrentFileName = FN;
            }

            if (Writer is { }) {
                Writer.WriteRecord(Record);
                Writer.NextRecord();
                TotalItemsWritten += 1;
                CurrentPageItemsWritten += 1;
            }

            var NextPage = Options.Page
                && (false
                    || (Options.Page_ItemCount_Max > 0 && CurrentPageItemsWritten >= Options.Page_ItemCount_Max)
                    || (Options.Page_FileSize_Max > 0 && FS?.Length >= Options.Page_FileSize_Max)
                );

            if (NextPage) {
                Close();

                CurrentPageIndex += 1;
                CurrentPageItemsWritten = 0;

            }

        }

        public void Dispose() {
            Close();
            GC.SuppressFinalize(this);
        }

        private FileStream? FS { get; set; }
        private StreamWriter? TW { get; set; }
        private CsvWriter? Writer { get; set; }

        public string CurrentFileName { get; private set; } = string.Empty;
        
        public long CurrentPageIndex { get; private set; }
        public long CurrentPageItemsWritten { get; private set; }
        public long TotalItemsWritten { get; private set; }

    }

}
