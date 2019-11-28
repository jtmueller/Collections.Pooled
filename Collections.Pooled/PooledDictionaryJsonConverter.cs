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
    internal sealed class PooledDictionaryJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            if (typeToConvert.GetGenericTypeDefinition() != typeof(PooledDictionary<,>))
            {
                return false;
            }

            return typeToConvert.GetGenericArguments()[0] == typeof(string);
        }

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            var args = type.GetGenericArguments();
            var keyType = args[0];
            var valueType = args[1];

            if (keyType != typeof(string))
                throw new ArgumentException("A pooled dictionary with a key type other than string is not supported by the default converter.");

#nullable disable
            var converter = (JsonConverter)Activator.CreateInstance(
                typeof(PooledDictionaryConverterInner<>).MakeGenericType(valueType),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter;
#nullable enable
        }

        private class PooledDictionaryConverterInner<TValue> : JsonConverter<PooledDictionary<string, TValue>> 
        {
            private readonly JsonConverter<TValue>? _valueConverter;
            private readonly Type _valueType;

            public PooledDictionaryConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

                // Cache the key and value types.
                _valueType = typeof(TValue);
            }

            public override PooledDictionary<string, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                var value = new PooledDictionary<string, TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return value;
                    }

                    // Get the key.
                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new JsonException();
                    }

                    string propertyName = reader.GetString();

                    // Get the value.
                    TValue v;
                    if (_valueConverter != null)
                    {
                        reader.Read();
                        v = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        v = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to dictionary.
                    value.Add(propertyName, v);
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, PooledDictionary<string, TValue> dictionary, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                foreach (var (key, value) in dictionary)
                {
                    writer.WritePropertyName(key);

                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, value, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, value, options);
                    }
                }

                writer.WriteEndObject();
            }
        }
    }
}

#endif