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
            {typeof(IList), () => new ArrayMapper()}
        };

        public static IObjectMapper GetObjectMapper(object node, Type type)
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

            if (!_objectMappers.ContainsKey(type))
            {
                _objectMappers.Add(type, () => new DefaultMapper(type));
            }

            return _objectMappers[type].Invoke();
        }
    }
}
