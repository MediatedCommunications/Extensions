using System;
using System.Windows;
using System.Collections.Generic;

namespace Telerik.Windows.Controls {
    public static class GridLengthParser {
        public static List<GridLength> Parse(string Input) {
            var ret = new List<GridLength>();

            var Parts = Input.Split(new[] { Strings.Comma, Strings.Semicolon });

            var Converter = new GridLengthConverter();

            foreach (var Part in Parts) {
                if (Converter.ConvertFromString(Part) is GridLength { } NewGridLength) {
                    ret.Add(NewGridLength);
                } else {
                    throw new FormatException($@"{Part} is not a valid length");
                }
            }


            return ret;
        }
    }

}
