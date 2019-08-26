using Extension.Template;
using Newtonsoft.Json;
using System;

namespace WebAPI.Template
{
    public class EnumToStringConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
              JsonSerializer serializer)
        {
            if (reader.Value == null || string.IsNullOrEmpty(reader.Value.ToString()))
            {
                return null;
            }
            return Enum.Parse(objectType, reader.Value.ToString().ToPascalCase());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum || ((Nullable.GetUnderlyingType(objectType) != null) && Nullable.GetUnderlyingType(objectType).IsEnum);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString().ToSnakeCase());
        }
    }
}