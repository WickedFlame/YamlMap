using System;
using System.Collections;
using System.Text;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Serializer that creates tokens of objects
    /// </summary>
    public class TokenSerializer
    {
	    /// <summary>
        /// Serialize the item to a string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
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
                    if (itm.GetType().IsClass && itm is not string)
                    {
                        var lstSb = new StringBuilder();
                        
                        SerializeNode(itm, lstSb, indentation + 2);

                        sb.Append($"- ".Indent(indentation));
                        sb.AppendLine(lstSb.ToSerializeableString().Trim());
                        continue;
                    }

                    sb.AppendLine($"- {itm.ToSerializeableString()}".Indent(indentation));
                }

                return;
            }

            if (item is IDictionary dict)
            {
                foreach (DictionaryEntry itm in dict)
                {
                    if (itm.Value.GetType().IsClass && itm.Value is not string)
                    {
                        sb.AppendLine($"{itm.Key}:".Indent(indentation));
                        SerializeNode(itm.Value, sb, indentation + 2);
                        continue;
                    }

                    sb.AppendLine($"{itm.Key}: {itm.Value.ToSerializeableString()}".Indent(indentation));
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

                if (value is Type type)
                {
	                sb.AppendLine($"{name}: {type}, {type.Assembly.GetName().Name}".Indent(indentation));
	                continue;
				}

                if (value.GetType().IsClass && value is not string)
                {
	                sb.AppendLine($"{name}:".Indent(indentation));
	                SerializeNode(value, sb, indentation + 2);
	                continue;
                }

                sb.AppendLine($"{name}: {value.ToSerializeableString()}".Indent(indentation));
            }
        }
    }
}
