#if NET452 

#else

using System;
using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetHelper.Serialization.Json.Converters
{
    public class LongToStringConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                if (Utf8Parser.TryParse(span, out long number, out var bytesConsumed) && span.Length == bytesConsumed)
                    return number;

                if (long.TryParse(reader.GetString(), out number))
                    return number;
            }

            return reader.GetInt64();
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}

#endif