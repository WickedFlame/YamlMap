using System;
using System.Globalization;

namespace WickedFlame.Yaml
{
    public class TypeConverter
    {
        public static object Convert(Type type, string value)
        {
            if (type == typeof(string))
            {
                return value.Trim();
            }
            
            if (type == typeof(bool) || type == typeof(bool?))
            {
                if (value != null)
                {
                    return ParseBoolean(value);
                }

                return null;
            }

            if (type == typeof(long) || type == typeof(long?))
            {
                if (long.TryParse(value, out var l))
                {
                    return l;
                }

                return null;
            }
            
            if (type == typeof(int) || type == typeof(int?))
            {
                if (int.TryParse(value, out var i))
                {
                    return i;
                }

                return null;
            }
            
            if (type == typeof(decimal))
            {
                return decimal.Parse(value);
            }
            
            if (type == typeof(double) || type == typeof(double?))
            {
                if (double.TryParse(value, out var b))
                {
                    return b;
                }

                return null;
            }
            
            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                if (DateTime.TryParse(value, out var date))
                {
                    return date;
                }

                if (DateTime.TryParseExact(value, "yyyyMMdd", new CultureInfo("de-CH"), DateTimeStyles.AssumeLocal, out date))
                {
                    return date;
                }

                return null;
            }
            
            if (type == typeof(Guid))
            {
                if (Guid.TryParse(value, out var guid))
                {
                    return guid;
                }

                if (Guid.TryParseExact(value, "B", out guid))
                {
                    return guid;
                }

                return null;
            }
            
            if (type == typeof(Type))
            {
                return Type.GetType(value);
            }

            return null;
        }

        public static bool ParseBoolean(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return false;
            }

            switch (value.ToString().ToLowerInvariant())
            {
                case "1":
                case "y":
                case "yes":
                case "true":
                    return true;

                case "0":
                case "n":
                case "no":
                case "false":
                default:
                    return false;
            }
        }
    }
}
