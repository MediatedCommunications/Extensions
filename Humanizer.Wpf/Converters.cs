using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Humanizer.Wpf {

    public abstract class StringConverterBase : IValueConverter {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var ret = default(object?);
            if(value is { } V1) {
                ret = Convert($@"{V1}", parameter, culture);
            }

            return ret;
        }

        protected abstract string? Convert(string Input, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class DateHumanizer : DateConverterBase {
        public DateTimeOffset? DateToCompareAgainst { get; set; }

        protected override string? Convert(DateTime Input, object parameter, CultureInfo culture) {
            var CompareAgainst = DateToCompareAgainst?.UtcDateTime;
            var ret = Input.Humanize(default, CompareAgainst, culture);

            return ret;
        }

        protected override string? Convert(DateTimeOffset Input, object parameter, CultureInfo culture) {
            var ret = Input.Humanize(DateToCompareAgainst, culture);

            return ret;
        }
    }

    public abstract class DateConverterBase : IValueConverter {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var ret = default(object?);
            if (value is DateTime { } V1) {
                ret = Convert(V1, parameter, culture);
            } else if(value is DateTimeOffset { } V2) {
                ret = Convert(V2, parameter, culture);
            }

            return ret;
        }

        protected abstract string? Convert(DateTime Input, object parameter, CultureInfo culture);
        protected abstract string? Convert(DateTimeOffset Input, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class PluralConverter : StringConverterBase {

        public bool InputIsKnownToBeSignular { get; set; } = true;

        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Pluralize(InputIsKnownToBeSignular);
            return ret;
        }
    }


    public abstract class TimeSpanConverter : IValueConverter {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var ret = default(object?);
            if (value is TimeSpan { } V1) {
                ret = Convert(V1, parameter, culture);
            }

            return ret;
        }

        protected abstract string? Convert(TimeSpan Input, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }



    public class SingularConverter : StringConverterBase {

        public bool InputIsKnownToBeSignular { get; set; } = true;
        public bool SkipSimpleWords { get; set; } = false;

        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Singularize(InputIsKnownToBeSignular, SkipSimpleWords);
            return ret;
        }
    }

    public class TitleCaseConverter : StringConverterBase {
        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Titleize();
            return ret;
        }
    }

    public class PascalCaseConverter : StringConverterBase {
        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Pascalize();
            return ret;
        }
    }

    public class CamelCaseConverter : StringConverterBase {
        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Camelize();
            return ret;
        }
    }

    public class UnderscoreConverter : StringConverterBase {
        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Underscore();
            return ret;
        }
    }

    public class DashConverter : StringConverterBase {
        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Dasherize();
            return ret;
        }
    }

    public class HypenConverter : StringConverterBase {
        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Hyphenate();
            return ret;
        }
    }

    public class KebabConverter : StringConverterBase {
        protected override string? Convert(string Input, object parameter, CultureInfo culture) {
            var ret = Input.Kebaberize();
            return ret;
        }
    }

    public class TimespanConverter : TimeSpanConverter {

        public int Precision { get; set; } = 1;
        public Localisation.TimeUnit MaxUnit { get; set; } = Localisation.TimeUnit.Week;
        public Localisation.TimeUnit MinUnit { get; set; } = Localisation.TimeUnit.Millisecond;

        protected override string? Convert(TimeSpan Input, object parameter, CultureInfo culture) {
            var ret = TimeSpanHumanizeExtensions.Humanize(Input, Precision, culture, MaxUnit, MinUnit);
            return ret;
        }
    }

    public abstract class NumberConverter : IValueConverter {

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var ret = default(object?);
            if (value is int { } V1) {
                ret = Convert(V1, parameter, culture);
            
            } else if (value is long { } V2) {
                ret = Convert(V2, parameter, culture);
            
            } else if (value is double { } V3) {
                ret = Convert(V3, parameter, culture);

            }

            return ret;
        }

        protected abstract string? Convert(int Input, object parameter, CultureInfo culture);

        protected abstract string? Convert(long Input, object parameter, CultureInfo culture);

        protected abstract string? Convert(double Input, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class QuantityConverter : NumberConverter {

        public string Unit { get; set; } = "Unit";
        public ShowQuantityAs ShowQuantityAs { get; set; } = ShowQuantityAs.Numeric;

        private static string GetUnit(params object?[] parameters) {
            var tret = parameters.Where(x => x is { }).FirstOrDefault();
            var ret = $@"{tret}";

            return ret;
        }

        protected override string? Convert(int Input, object parameter, CultureInfo culture) {
            var ret = GetUnit(parameter, Unit).ToQuantity(Input, ShowQuantityAs);
            return ret;
        }

        protected override string? Convert(long Input, object parameter, CultureInfo culture) {
            var ret = GetUnit(parameter, Unit).ToQuantity(Input, ShowQuantityAs);
            return ret;
        }

        protected override string? Convert(double Input, object parameter, CultureInfo culture) {
            var ret = GetUnit(parameter, Unit).ToQuantity(Input);
            return ret;
        }
    }

    public class FileSizeConverter : NumberConverter {

        public string? Format { get; set; }

        protected override string? Convert(int Input, object parameter, CultureInfo culture) {
            var ret = Humanizer.ByteSizeExtensions.Bytes(Input).Humanize(Format);
            return ret;
        }

        protected override string? Convert(long Input, object parameter, CultureInfo culture) {
            var ret = Humanizer.ByteSizeExtensions.Bytes(Input).Humanize(Format);
            return ret;
        }

        protected override string? Convert(double Input, object parameter, CultureInfo culture) {
            var ret = Humanizer.ByteSizeExtensions.Bytes(Input).Humanize(Format);
            return ret;
        }
    }


}
