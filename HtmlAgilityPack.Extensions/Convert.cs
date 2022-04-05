using System;

namespace HtmlAgilityPack {
    public static class Convert {
        public static string FromHtmlToTxt(string Html) {
            var ret = "";
            try {
                var Doc = new HtmlAgilityPack.HtmlDocument();
                Doc.LoadHtml(Html);

                var Node = Doc.DocumentNode;
                Node = Node.SelectSingleNode("/html/body") ?? Node;

                ret = Node.InnerText;
            } catch { }

            try {
                ret = HtmlAgilityPack.HtmlEntity.DeEntitize(ret);
            } catch(Exception ex) {
                ex.Ignore();
            }


            return ret;
        }

    }

}
