using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf.Annotations;

namespace PdfSharpCore.Pdf {



    public static class PdfDictionaryExtensions {
        public static PdfAnnotation ToAnnotation(this PdfDictionary This) {
            var ret = new CustomAnnotation();

            foreach (var item in This.Elements) {
                ret.Elements[item.Key] = item.Value;
            }


            return ret;
        }

        public static string GetStringValue(this PdfEntityType This) {
            var ret = This switch
            {
                PdfEntityType.Annotation => "/Annot",
                _ => throw new NotImplementedException(),
            };

            return ret;
        }

        public static string GetStringValue(this PdfAnnotationSubtype This) {
            var ret = This switch
            {
                PdfAnnotationSubtype.Circle => "/Circle",
                PdfAnnotationSubtype.FileAttachment => "/FileAttachment",
                PdfAnnotationSubtype.FreeText => "/FreeText",
                PdfAnnotationSubtype.Highlight => "/Highlight",
                PdfAnnotationSubtype.Ink => "/Ink",
                PdfAnnotationSubtype.Line => "/Line",
                PdfAnnotationSubtype.Link => "/Link",
                PdfAnnotationSubtype.Movie => "/Movie",
                PdfAnnotationSubtype.Popup => "/Popup",
                PdfAnnotationSubtype.Sound => "/Sound",
                PdfAnnotationSubtype.Square => "/Square",
                PdfAnnotationSubtype.Stamp => "/Stamp",
                PdfAnnotationSubtype.StrikeOut => "/StrikeOut",
                PdfAnnotationSubtype.Text => "/Text",
                PdfAnnotationSubtype.TrapNet => "/TrapNet",
                _ => throw new NotImplementedException(),
            };

            return ret;
        }


        public static PdfDictionary SetName(this PdfDictionary This, string Key, string Value) {
            This.Elements.SetName(Key, Value);
            return This;
        }

        public static PdfDictionary Set(this PdfDictionary This, string Key, string Value) {
            This.Elements.SetString(Key, Value);
            return This;
        }

        public static PdfDictionary Set(this PdfDictionary This, string Key, bool Value) {
            This.Elements.SetBoolean(Key, Value);
            return This;
        }

        public static PdfDictionary Set(this PdfDictionary This, string Key, int Value) {
            This.Elements.SetInteger(Key, Value);
            return This;
        }

        public static PdfDictionary Set(this PdfDictionary This, string Key, DateTime Value) {
            This.Elements.SetDateTime(Key, Value);
            return This;
        }


        public static PdfDictionary Set(this PdfDictionary This, string Key, XMatrix Value) {
            This.Elements.SetMatrix(Key, Value);
            return This;
        }
        public static PdfDictionary Set(this PdfDictionary This, string Key, PdfObject Value) {
            This.Elements.SetObject(Key, Value);
            return This;
        }
        public static PdfDictionary Set(this PdfDictionary This, string Key, PdfRectangle Value) {
            This.Elements.SetRectangle(Key, Value);
            return This;
        }

        public static PdfDictionary Set(this PdfDictionary This, string Key, PdfItem Value) {
            This.Elements.SetValue(Key, Value);
            return This;
        }

        public static PdfDictionary Set(this PdfDictionary This, string Key, double Value) {
            This.Elements.SetReal(Key, Value);
            return This;
        }



        public static PdfDictionary SetType(this PdfDictionary This) {
            return This.SetType("/Annot");
        }

        public static PdfDictionary SetType(this PdfDictionary This, PdfAnnotationSubtype Value) {
            return This
                .SetType(PdfEntityType.Annotation)
                .SetSubType(Value)
                ;
        }

        public static PdfDictionary SetType(this PdfDictionary This, PdfEntityType Value) {
            return This.SetType(Value.GetStringValue());
        }

        public static PdfDictionary SetType(this PdfDictionary This, string Value) {
            return This.SetName(PdfAnnotation.Keys.Type, Value);
        }

        public static PdfDictionary SetSubType(this PdfDictionary This, PdfAnnotationSubtype Value) {
            return This.SetSubType(Value.GetStringValue());
        }

        public static PdfDictionary SetSubType(this PdfDictionary This, string Value) {
            return This.SetName(PdfAnnotation.Keys.Subtype, Value);
        }

        public static PdfDictionary SetCreatedAt(this PdfDictionary This, DateTime Value) {
            return This.Set(PdfAnnotation.Keys.M, Value);
        }

        public static PdfDictionary SetUpdatedAt(this PdfDictionary This, DateTime Value) {
            return This.Set("/CreationDate", Value);
        }

        public static PdfDictionary SetAuthor(this PdfDictionary This, string Value) {
            return This.Set(PdfAnnotation.Keys.T, Value);
        }

        public static PdfDictionary SetSubject(this PdfDictionary This, string Value) {
            return This.Set(PdfAnnotation.Keys.Subj, Value);
        }
        public static PdfDictionary SetDescription(this PdfDictionary This, string Value) {
            return This.Set(PdfAnnotation.Keys.Contents, Value);
        }

        public static PdfDictionary SetState(this PdfDictionary This, string Value) {
            return This.Set("/State", Value);
        }


        public static PdfDictionary SetId(this PdfDictionary This) {
            return SetId(This, Guid.NewGuid().ToString("D"));
        }

        public static PdfDictionary SetId(this PdfDictionary This, string Value) {
            return This.Set(PdfAnnotation.Keys.NM, Value);
        }

        public static PdfDictionary SetRect(this PdfDictionary This, PdfRectangle Value) {
            return This.Set(PdfAnnotation.Keys.Rect, Value);
        }

        public static PdfDictionary SetFillColor(this PdfDictionary This, XColor Value) {
            return This
                .SetFillColor(Value.R, Value.G, Value.B)
                ;
        }

        public static PdfDictionary SetFillColor(this PdfDictionary This, byte R, byte G, byte B) {
            var NewValue = new PdfArray()
            {
                Elements = {
                    new PdfReal(R / 255.0),
                    new PdfReal(G / 255.0),
                    new PdfReal(B / 255.0)
                }
            };

            return This.Set("/IC", NewValue);
        }





        public static PdfDictionary SetOpacity(this PdfDictionary This) {
            return This.SetOpacity(true);
        }
        
        public static PdfDictionary SetOpacity(this PdfDictionary This, bool Value) {
            return This.SetOpacity(Value ? 255 : 0);
        }

        public static PdfDictionary SetOpacity(this PdfDictionary This, byte Value) {
            return This.SetOpacity(Value / 255.0);
        }

        public static PdfDictionary SetOpacity(this PdfDictionary This, XColor Value) {
            return This.SetOpacity(Value.A * 255.0);
        }

        public static PdfDictionary SetOpacity(this PdfDictionary This, double Value) {
            return This.Set(PdfAnnotation.Keys.CA, Value);
        }

        public static PdfDictionary SetColor(this PdfDictionary This, XColor Value) {
            return This
                .SetColor(Value.R, Value.G, Value.B)
                ;
        }

        public static PdfDictionary SetColor(this PdfDictionary This, byte R, byte G, byte B) {
            var NewValue = new PdfArray()
            {
                Elements = {
                    new PdfReal(R / 255.0),
                    new PdfReal(G / 255.0),
                    new PdfReal(B / 255.0)
                }
            };

            return This.Set(PdfAnnotation.Keys.C, NewValue);
        }





        public static PdfDictionary SetBorderStyle(this PdfDictionary This, double Thickness) {
            var Values = new PdfDictionary()
                .Set("/W", Thickness)
                ;
            


            return This.Set(PdfAnnotation.Keys.BS, Values);
        }


    }

}
