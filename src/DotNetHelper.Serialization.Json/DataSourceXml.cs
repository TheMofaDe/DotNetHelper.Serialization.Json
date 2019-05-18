﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DotNetHelper.Serialization.Abstractions.Interface;
using DotNetHelper.Serialization.Json.Extension;
using Newtonsoft.Json;

namespace DotNetHelper.Serialization.Json
{
    public class DataSourceXml : ISerializer
    {
        private DataSourceJson SerializerJson { get; } 

        public Encoding Encoding { get; set; }

        public DataSourceXml()
        {
            Encoding = Encoding.UTF8;
            SerializerJson = new DataSourceJson(Encoding);
        }
        public DataSourceXml(Encoding encoding)
        {
            Encoding = encoding ?? Encoding.UTF8;
            SerializerJson = new DataSourceJson(Encoding);
        }

        public dynamic Deserialize(string xml)
        {
            xml.IsNullThrow(nameof(xml));
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            XDocument.Parse(xml)
            var json = JsonConvert.SerializeXmlNode(doc);
            return SerializerJson.Deserialize(json);
        }

        public dynamic Deserialize(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Encoding, false, bufferSize, leaveStreamOpen))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(SerializerJson.Settings);
                serializer.
                return serializer.Deserialize(reader, typeof(ExpandoObject));
            }
        }

        public T Deserialize<T>(string json) where T : class
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }

        public T Deserialize<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Encoding, false, bufferSize, leaveStreamOpen))
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

        public object Deserialize(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Encoding, false, bufferSize, leaveStreamOpen))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                return serializer.Deserialize(reader, type);
            }
        }

        public List<dynamic> DeserializeToList(string json)
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject<List<dynamic>>(json, Settings);
        }

        public List<dynamic> DeserializeToList(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Encoding, false, bufferSize, leaveStreamOpen))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                return serializer.Deserialize(reader, typeof(List<dynamic>)) as List<dynamic>;

            }
        }

        public List<T> DeserializeToList<T>(string json) where T : class
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject<List<T>>(json, Settings);
        }

        public List<T> DeserializeToList<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Encoding, false, bufferSize, leaveStreamOpen))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = JsonSerializer.Create(Settings);
                return serializer.Deserialize<List<T>>(reader);
            }
        }

        public List<object> DeserializeToList(string json, Type type)
        {
            json.IsNullThrow(nameof(json));
            return JsonConvert.DeserializeObject(json, type) as List<object>;
        }

        public List<object> DeserializeToList(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream, Encoding, false, bufferSize, leaveStreamOpen))
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
            SerializeToStream(obj, typeof(T), stream, bufferSize, leaveStreamOpen);
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