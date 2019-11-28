// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NETCOREAPP3_0

using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Collections.Pooled
{
    internal sealed class PooledEnumerableJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            var genericTypeDef = typeToConvert.GetGenericTypeDefinition();

            return genericTypeDef == typeof(PooledList<>)
                || genericTypeDef == typeof(PooledSet<>)
                || genericTypeDef == typeof(PooledQueue<>)
                || genericTypeDef == typeof(PooledStack<>)
                || genericTypeDef == typeof(PooledCollection<>)
                || genericTypeDef == typeof(PooledObservableCollection<>);
        }

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            var valueType = type.GetGenericArguments()[0];
            var genericTypeDef = type.GetGenericTypeDefinition();

            Type? converterType = null;

            if (genericTypeDef == typeof(PooledList<>))
                converterType = typeof(PooledListConverterInner<>);
            else if (genericTypeDef == typeof(PooledSet<>))
                converterType = typeof(PooledSetConverterInner<>);
            else if (genericTypeDef == typeof(PooledQueue<>))
                converterType = typeof(PooledQueueConverterInner<>);
            else if (genericTypeDef == typeof(PooledStack<>))
                converterType = typeof(PooledStackConverterInner<>);
            else if (genericTypeDef == typeof(PooledCollection<>))
                converterType = typeof(PooledCollectionConverterInner<>);
            else if (genericTypeDef == typeof(PooledObservableCollection<>))
                converterType = typeof(PooledObservableCollectionConverterInner<>);

            if (converterType is null)
                throw new ArgumentException($"Unsupported type: {type.FullName}.", nameof(type));

#nullable disable
            var converter = (JsonConverter)Activator.CreateInstance(
                converterType.MakeGenericType(valueType),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter;
#nullable enable
        }

        private class PooledListConverterInner<TValue> : JsonConverter<PooledList<TValue>>
        {
            private readonly JsonConverter<TValue>? _valueConverter;
            private readonly Type _valueType;

            public PooledListConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

                // Cache the key and value types.
                _valueType = typeof(TValue);
            }

            public override PooledList<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                var value = new PooledList<TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        return value;
                    }

                    // Get the value.
                    TValue v;
                    if (_valueConverter != null)
                    {
                        v = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        v = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to collection.
                    value.Add(v);
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, PooledList<TValue> value, JsonSerializerOptions options)
            {
                writer.WriteStartArray();

                foreach (var item in value.Span)
                {
                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, item, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }
                }

                writer.WriteEndArray();
            }
        }

        private class PooledSetConverterInner<TValue> : JsonConverter<PooledSet<TValue>>
        {
            private readonly JsonConverter<TValue>? _valueConverter;
            private readonly Type _valueType;

            public PooledSetConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

                // Cache the key and value types.
                _valueType = typeof(TValue);
            }

            public override PooledSet<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                var value = new PooledSet<TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        return value;
                    }

                    // Get the value.
                    TValue v;
                    if (_valueConverter != null)
                    {
                        v = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        v = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to collection.
                    value.Add(v);
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, PooledSet<TValue> value, JsonSerializerOptions options)
            {
                writer.WriteStartArray();

                foreach (var item in value)
                {
                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, item, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }
                }

                writer.WriteEndArray();
            }
        }

        private class PooledQueueConverterInner<TValue> : JsonConverter<PooledQueue<TValue>>
        {
            private readonly JsonConverter<TValue>? _valueConverter;
            private readonly Type _valueType;

            public PooledQueueConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

                // Cache the key and value types.
                _valueType = typeof(TValue);
            }

            public override PooledQueue<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                var value = new PooledQueue<TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        return value;
                    }

                    // Get the value.
                    TValue v;
                    if (_valueConverter != null)
                    {
                        v = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        v = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to collection.
                    value.Enqueue(v);
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, PooledQueue<TValue> value, JsonSerializerOptions options)
            {
                writer.WriteStartArray();

                foreach (var item in value)
                {
                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, item, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }
                }

                writer.WriteEndArray();
            }
        }

        private class PooledStackConverterInner<TValue> : JsonConverter<PooledStack<TValue>>
        {
            private readonly JsonConverter<TValue>? _valueConverter;
            private readonly Type _valueType;

            public PooledStackConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

                // Cache the key and value types.
                _valueType = typeof(TValue);
            }

            public override PooledStack<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                var value = new PooledStack<TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        value.Reverse();
                        return value;
                    }

                    // Get the value.
                    TValue v;
                    if (_valueConverter != null)
                    {
                        v = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        v = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to collection.
                    value.Push(v);
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, PooledStack<TValue> value, JsonSerializerOptions options)
            {
                writer.WriteStartArray();

                foreach (var item in value)
                {
                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, item, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }
                }

                writer.WriteEndArray();
            }
        }

        private class PooledCollectionConverterInner<TValue> : JsonConverter<PooledCollection<TValue>>
        {
            private readonly JsonConverter<TValue>? _valueConverter;
            private readonly Type _valueType;

            public PooledCollectionConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

                // Cache the key and value types.
                _valueType = typeof(TValue);
            }

            public override PooledCollection<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                using var items = new PooledList<TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        return new PooledCollection<TValue>(items);
                    }

                    // Get the value.
                    TValue v;
                    if (_valueConverter != null)
                    {
                        v = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        v = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to collection.
                    items.Add(v);
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, PooledCollection<TValue> value, JsonSerializerOptions options)
            {
                writer.WriteStartArray();

                foreach (var item in value)
                {
                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, item, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }
                }

                writer.WriteEndArray();
            }
        }

        private class PooledObservableCollectionConverterInner<TValue> : JsonConverter<PooledObservableCollection<TValue>>
        {
            private readonly JsonConverter<TValue>? _valueConverter;
            private readonly Type _valueType;

            public PooledObservableCollectionConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

                // Cache the key and value types.
                _valueType = typeof(TValue);
            }

            public override PooledObservableCollection<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                using var items = new PooledList<TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        return new PooledObservableCollection<TValue>(items);
                    }

                    // Get the value.
                    TValue v;
                    if (_valueConverter != null)
                    {
                        v = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        v = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to collection.
                    items.Add(v);
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, PooledObservableCollection<TValue> value, JsonSerializerOptions options)
            {
                writer.WriteStartArray();

                foreach (var item in value)
                {
                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, item, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, item, options);
                    }
                }

                writer.WriteEndArray();
            }
        }
    }
}

#endif