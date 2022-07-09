using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSharpCore {
    public static class PdfDocumentExtensions {
        public static bool TrySave(this PdfDocument This, string FullPath) {
            var ret = false;

            using var TFS = System.IO.TemporaryFile.Create();

            try {
                This.Save(TFS.FullPath);

                System.IO.File2.Move(TFS.FullPath, FullPath);

                ret = true;
            } catch (Exception ex) {
                ex.Ignore();
            }

            return ret;
        }

    }
}
