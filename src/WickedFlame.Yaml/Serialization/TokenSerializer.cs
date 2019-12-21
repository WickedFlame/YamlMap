using System;
using System.Collections;
using System.Text;

namespace WickedFlame.Yaml.Serialization
{
    public class TokenSerializer
    {
        public string Serialize<T>(T item)
        {
            var sb = new StringBuilder();

            SerializeNode(item, sb, 0);

            return sb.ToString().Trim();
        }

        private void SerializeNode<T>(T item, StringBuilder sb, int indentation)
        {
            if (item is IList list)
            {
                foreach (var itm in list)
                {
                    if (itm.GetType().IsClass && !(itm is string))
                    {
                        var lstSb = new StringBuilder();
                        
                        SerializeNode(itm, lstSb, indentation + 2);

                        sb.Append($"- ".Indent(indentation));
                        sb.AppendLine(lstSb.ToString().Trim());
                        continue;
                    }

                    sb.AppendLine($"- {itm}".Indent(indentation));
                }

                return;
            }

            if (item is IDictionary dict)
            {
                foreach (DictionaryEntry itm in dict)
                {
                    if (itm.Value.GetType().IsClass && !(itm.Value is string))
                    {
                        sb.AppendLine($"{itm.Key}:".Indent(indentation));
                        SerializeNode(itm.Value, sb, indentation + 2);
                        continue;
                    }

                    sb.AppendLine($"{itm.Key}: {itm.Value}".Indent(indentation));
                }

                return;
            }


            var properties = item.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(item);
                if (value == null)
                {
                    continue;
                }

                var name = property.Name;
                if (value.GetType().IsClass && !(value is string))
                {
                    sb.AppendLine($"{name}:".Indent(indentation));
                    SerializeNode(value, sb, indentation + 2);
                    continue;
                }

                sb.AppendLine($"{name}: {value}".Indent(indentation));
            }
        }
    }
}
