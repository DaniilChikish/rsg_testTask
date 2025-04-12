using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace Utility
{
    public class IgnoreUnexpectedArraysConverter<T> : IgnoreUnexpectedArraysConverterBase
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }
    }

    public class IgnoreUnexpectedArraysConverter : IgnoreUnexpectedArraysConverterBase
    {
        readonly IContractResolver _resolver;

        public IgnoreUnexpectedArraysConverter(IContractResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException();
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsPrimitive || objectType == typeof(string))
                return false;
            return _resolver.ResolveContract(objectType) is JsonObjectContract;
        }
    }

    public abstract class IgnoreUnexpectedArraysConverterBase : JsonConverter
    {
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
                        return null;
                    case JsonToken.Comment:
                        continue;
                    case JsonToken.StartArray:
                        var array = JArray.Load(reader);
                        if (array.Count > 0)
                        {
                            existingValue = array.ToObject(objectType);
                            //throw new JsonSerializationException(string.Format("Array was not empty."));
                        }

                        if (existingValue == null)
                            return contract.DefaultCreator();
                        else
                            return existingValue;
                    case JsonToken.StartObject:
                        // Prevent infinite recursion by using Populate()
                        if (existingValue == null)
                            existingValue = contract.DefaultCreator();
                     
                        serializer.Populate(reader, existingValue);
                        return existingValue;
                    default:
                        throw new JsonSerializationException($"Unexpected token {reader.TokenType}");
                }
            } while (reader.Read());

            throw new JsonSerializationException("Unexpected end of JSON.");
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}