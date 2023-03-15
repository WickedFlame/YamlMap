using System;
using System.Collections;
using System.Collections.Generic;

namespace YamlMap.Serialization.Mappers
{
    /// <summary>
    /// Gets mappers used to map values to objects
    /// </summary>
    public static class MapperFactory
    {
        private static readonly Dictionary<Type, Func<IObjectMapper>> _objectMappers = new Dictionary<Type, Func<IObjectMapper>>
        {
            {typeof(List<>), () => new GenericListMapper()},
            {typeof(Dictionary<,>), () => new GenericDictionaryMapper()},
            {typeof(IList), () => new ArrayMapper()}
        };

        /// <summary>
        /// Get the <see cref="IObjectMapper"/> containing the logic to map data to a type
        /// </summary>
        /// <param name="node"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IObjectMapper GetObjectMapper(object node, Type type)
        {
            var mapperType = node.GetType();
            if (mapperType.IsGenericType)
            {
                mapperType = mapperType.GetGenericTypeDefinition();
            }

            if (mapperType.IsArray)
            {
                // use the list mapper type for arrays
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
