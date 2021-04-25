#if NET452

#else

using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetHelper.Serialization.Json.Converters;

namespace DotNetHelper.Serialization.Json
{
    public static class JsonHelper
    {
        public static JsonSerializerOptions DefaultOptions => new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false,
            PropertyNamingPolicy = null,
            Converters = { new Int32Converter(), new DecimalConverter(), new LongToStringConverter(), new JsonStringEnumConverter() }
        };
    }
}

#endif