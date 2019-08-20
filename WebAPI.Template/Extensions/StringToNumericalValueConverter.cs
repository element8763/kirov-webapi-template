using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace WebAPI.Template
{
    public class StringToNumericalValueConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
          JsonSerializer serializer)
        {
            if ((reader.ValueType == typeof(string) || reader.ValueType == null) && string.IsNullOrEmpty((string)reader.Value))
            {
                return null;
            }
            JToken jt = JToken.ReadFrom(reader);

            if (typeof(int?).Equals(objectType) || typeof(int).Equals(objectType))
            {
                return jt.Value<int>();
            }

            if (typeof(decimal?).Equals(objectType) || typeof(decimal).Equals(objectType))
            {
                return jt.Value<decimal>();
            }

            if (typeof(double?).Equals(objectType) || typeof(double).Equals(objectType))
            {
                return jt.Value<double>();
            }

            return jt.Value<long>();

        }

        public override bool CanConvert(Type objectType)
        {
            if (typeof(int?).Equals(objectType) ||
              typeof(decimal?).Equals(objectType) ||
              typeof(double?).Equals(objectType) ||
              typeof(long?).Equals(objectType) ||
              typeof(int).Equals(objectType) ||
              typeof(decimal).Equals(objectType) ||
              typeof(double).Equals(objectType) ||
              typeof(long).Equals(objectType))
            {
                return true;
            }
            return false;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}