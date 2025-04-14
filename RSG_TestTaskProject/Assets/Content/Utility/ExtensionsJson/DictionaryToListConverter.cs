using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utility
{
    /// <summary>
    /// Used for those situations where we receive dictionary where we awaiting array without any keys.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class DictionaryToListConverter<TValue> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var contract = serializer.ContractResolver.ResolveContract(objectType);

            do
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (reader.TokenType)
                {
                    case JsonToken.Null:
                    case JsonToken.String:
                        return new List<TValue>();
                    case JsonToken.Comment:
                        continue;
                    case JsonToken.StartArray:
                        var array = JArray.Load(reader);
                        if (array.Count > 0)
                        {
                            existingValue = array.ToObject(objectType);
                        }

                        if (existingValue == null)
                            return contract.DefaultCreator();
                        else 
                            return existingValue;
                    case JsonToken.StartObject:
                        // Prevent infinite recursion by using Populate()
                        if (existingValue == null)
                            existingValue = contract.DefaultCreator();

                        var dictionary = new Dictionary<string, TValue>();

                        serializer.Populate(reader, dictionary);

                        var list = (List<TValue>) existingValue;

                        list.AddRange(dictionary.Values);

                        return list;
                    default:
                        throw new JsonSerializationException($"Unexpected token {reader.TokenType}");
                }
            } while (reader.Read());

            throw new JsonSerializationException("Unexpected end of JSON.");
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(Dictionary<string, TValue>).IsAssignableFrom(objectType);
        }
    }
}