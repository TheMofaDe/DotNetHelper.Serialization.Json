using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using DotNetHelper.Serialization.Abstractions.Interface;
using DotNetHelper.Serialization.Json.Extension;
using Newtonsoft.Json;

namespace DotNetHelper.Serialization.Json
{
    public class DataSourceJson : ISerializer
    {
        public JsonSerializerSettings Settings { get; set; }

        public Encoding Encoding { get; set; }

        public DataSourceJson()
        {
            Settings = new JsonSerializerSettings();
            Encoding = Encoding.UTF8;
        }
        public DataSourceJson(JsonSerializerSettings settings)
        {
            Settings = settings ?? new JsonSerializerSettings();
            Encoding = Encoding ?? Encoding.UTF8;
        }
        public DataSourceJson(Encoding encoding, JsonSerializerSettings settings = null)
        {
            Settings = settings ?? new JsonSerializerSettings();
            Encoding = encoding ?? Encoding.UTF8;
        }

        public dynamic Deserialize(string json)
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject(json,typeof(ExpandoObject),Settings);
        }

        public dynamic Deserialize(Stream stream)  // stream will be dispose of 
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                return serializer.Deserialize(reader,typeof(ExpandoObject));
            }
        }

        public T Deserialize<T>(string json) where T : class
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject<T>(json,Settings);
        }

        public T Deserialize<T>(Stream stream) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                return serializer.Deserialize<T>(reader);
            }
        }

        public object Deserialize(string json, Type type)
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject(json, Settings);
        }

        public object Deserialize(Stream stream, Type type)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                return serializer.Deserialize(reader,type);
            }
        }

        public List<dynamic> DeserializeToList(string json)
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject<List<dynamic>>(json, Settings) ;
        }

        public List<dynamic> DeserializeToList(Stream stream)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                dynamic list = serializer.Deserialize(reader,typeof(List<dynamic>));
                return list;
            }
        }

        public List<T> DeserializeToList<T>(string json) where T : class
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject<List<T>>(json, Settings);
        }

        public List<T> DeserializeToList<T>(Stream stream) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                return serializer.Deserialize<List<T>>(reader);
            }
        }

        public List<object> DeserializeToList(string json, Type type)
        {
            json.IsNullThrow(nameof(json));
            dynamic test = JsonConvert.DeserializeObject(json,type);
            return test;
        }

        public List<object> DeserializeToList(Stream stream, Type type)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                return serializer.Deserialize(reader, typeof(List<object>)) as List<object>;
            }
        }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public void SerializeToStream<T>(T obj, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            obj.IsNullThrow(nameof(obj));
            stream.IsNullThrow(nameof(stream));
            SerializeToStream(obj, obj.GetType(), stream, bufferSize, leaveStreamOpen);
        }

        public void SerializeToStream(object obj, Type type, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            var serializer = JsonSerializer.Create(Settings);
            using (var sw = new StreamWriter(stream, Encoding, bufferSize, leaveStreamOpen))
            using (var jsonTextWriter = new JsonTextWriter(sw))
            {
                serializer.Serialize(jsonTextWriter, obj, type);
            }
        }

        public Stream SerializeToStream<T>(T obj, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            var memoryStream = new MemoryStream();
            var serializer = JsonSerializer.Create(Settings);
            using (var sw = new StreamWriter(memoryStream, Encoding, bufferSize, leaveStreamOpen))
            using (var jsonTextWriter = new JsonTextWriter(sw))
            {
                serializer.Serialize(jsonTextWriter, obj);
            }

            if (leaveStreamOpen)
                memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        public Stream SerializeToStream(object obj, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            var serializer = JsonSerializer.Create(Settings);
            var memoryStream = new MemoryStream();
            using (var sw = new StreamWriter(memoryStream, Encoding, bufferSize, leaveStreamOpen))
            using (var jsonTextWriter = new JsonTextWriter(sw))
            {
                serializer.Serialize(jsonTextWriter, obj, type);
            }
            if (leaveStreamOpen)
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
            }

            return memoryStream;
        }

        public string SerializeToString(object obj)
        {
            obj.IsNullThrow(nameof(obj));
            return JsonConvert.SerializeObject(obj, Settings);
        }

        public string SerializeToString<T>(T obj) where T : class
        {
            obj.IsNullThrow(nameof(obj));
            return JsonConvert.SerializeObject(obj, Settings);
        }

        public string SerializeToXml(string json)
        {
            var xml = JsonConvert.DeserializeXmlNode(json); // is node not note
            // or .DeserilizeXmlNode(myJsonString, "root"); // if myJsonString does not have a root
            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    xml.WriteTo(xmlTextWriter);
                    return stringWriter.ToString();
                }
            }
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
