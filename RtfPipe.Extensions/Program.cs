using BracketPipe;

namespace RtfPipe {
    public static class RtfConvert {
        public static string ToMarkdown(string source) {
            using var w = new System.IO.StringWriter();
            using var md = new MarkdownWriter(w); 
            Rtf.ToHtml(source, md);
            md.Flush();
            return w.ToString();
        }

        public static string ToText(string source) {
            using var w = new System.IO.StringWriter();
            using var md = new PlainTextWriter(w); 
            Rtf.ToHtml(source, md);
            md.Flush();
            return w.ToString();
        }

        public static string ToHtml(string source) {
            using var w = new System.IO.StringWriter();
            Rtf.ToHtml(source);
            return w.ToString();
        }

    }
}
