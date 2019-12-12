using System;
using System.Collections;
using System.Collections.Generic;

namespace WickedFlame.Yaml.Serialization.Mappers
{
    public class MapperFactory
    {
        private static readonly Dictionary<Type, Func<IObjectMapper>> _objectMappers = new Dictionary<Type, Func<IObjectMapper>>
        {
            {typeof(List<>), () => new GenericListMapper()},
            {typeof(Dictionary<,>), () => new GenericDictionaryMapper()},
            {typeof(IList), () => new ListMapper()}
        };

        public static IObjectMapper GetObjectMapper(object node)
        {
            var mapperType = node.GetType();
            if (mapperType.IsGenericType)
            {
                mapperType = mapperType.GetGenericTypeDefinition();
            }

            if (mapperType.IsArray)
            {
                mapperType = typeof(IList);
            }

            if (_objectMappers.ContainsKey(mapperType))
            {
                return _objectMappers[mapperType].Invoke();
            }

            return null;
        }
    }
}
