﻿using CoCSharp.Network;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace coc_api
{
    public class MessageMapper
    {
        public MessageMapper()
        {
            _cache = new Dictionary<Type, FieldMap[]>();
        }

        private Dictionary<Type, FieldMap[]> _cache;

        public FieldMap[] Map(Message message)
        {
            var type = message.GetType();
            return MapType(type);
        }

        private FieldMap[] MapType(Type type)
        {
            var map = default(FieldMap[]);
            // Look up if we've already cached this type.
            if (_cache.TryGetValue(type, out map))
                return map;

            var fieldMaps = new List<FieldMap>();
            var fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var fieldMap = MapField(fields[i]);
                fieldMaps.Add(fieldMap);
            }

            map = fieldMaps.ToArray();
            // Register the map to cache.
            _cache.Add(type, map);

            return map;
        }

        private FieldMap MapField(FieldInfo field)
        {
            var fieldMap = new FieldMap();
            fieldMap.Name = field.Name;
            fieldMap.Field = field;

            // Map fields with returning type which inherits from MessageComponent recursively.
            if (field.FieldType.BaseType == typeof(MessageComponent))
                fieldMap.Child = MapType(field.FieldType);

            return fieldMap;
        }
    }

    public class FieldMap
    {
        public FieldMap()
        {
            // Space
        }

        public string Name;
        public FieldInfo Field;
        public FieldMap[] Child;
    }
}
