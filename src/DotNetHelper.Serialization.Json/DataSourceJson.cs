#if NET452 

#else


using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml;
using DotNetHelper.Serialization.Abstractions.Interface;
using DotNetHelper.Serialization.Json.Extension;


namespace DotNetHelper.Serialization.Json
{
    public class DataSourceJson : ISerializer
    {
        public JsonSerializerOptions Settings { get; set; }

        public Encoding Encoding { get; set; }

        public DataSourceJson()
        {
            Settings = JsonHelper.DefaultOptions;
            Encoding = Encoding.UTF8;
        }
        public DataSourceJson(JsonSerializerOptions settings)
        {
            Settings = settings ?? JsonHelper.DefaultOptions;
            Encoding = Encoding.UTF8;
        }
        public DataSourceJson(Encoding encoding, JsonSerializerOptions settings = null)
        {
            Settings = settings ?? JsonHelper.DefaultOptions;
            Encoding = encoding ?? Encoding.UTF8;
        }

        public dynamic Deserialize(string json)
        {
            json.IsNullThrow(nameof(json));
            return JsonSerializer.Deserialize<ExpandoObject>(json,Settings);
        }

        public dynamic Deserialize(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)  
        {
            stream.IsNullThrow(nameof(stream));
            using (var sr = new StreamReader(stream,Encoding,false,bufferSize,leaveStreamOpen)){
              // https://github.com/dotnet/runtime/issues/1574
            var obj = JsonSerializer.DeserializeAsync<ExpandoObject>(stream, Settings)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            return obj;
            }
        }

        public T Deserialize<T>(string json) where T : class
        {
            json.IsNullThrow(nameof(json));
            return JsonSerializer.Deserialize<T>(json, Settings);
        }

        public T Deserialize<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            // https://github.com/dotnet/runtime/issues/1574
            var obj = JsonSerializer.DeserializeAsync<T>(stream, Settings).ConfigureAwait(false).GetAwaiter()
                .GetResult();
            if(!leaveStreamOpen)
                stream.Dispose();
            return obj;
        }

        public object Deserialize(string json, Type type)
        {
            json.IsNullThrow(nameof(json));
            var obj = JsonSerializer.Deserialize(json, type, Settings);
            return obj;
        }

        public object Deserialize(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            // https://github.com/dotnet/runtime/issues/1574
            var obj = JsonSerializer.DeserializeAsync(stream,type,Settings).ConfigureAwait(false).GetAwaiter()
                .GetResult();
            if (!leaveStreamOpen)
                stream.Dispose();
            return obj;
        }


        public void SerializeToStream<T>(T obj, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            obj.IsNullThrow(nameof(obj));
            stream.IsNullThrow(nameof(stream));
            JsonSerializer.SerializeAsync<T>(stream, obj, Settings).ConfigureAwait(false).GetAwaiter().GetResult();
            if(leaveStreamOpen is false)
                stream?.Dispose();
        }

        public void SerializeToStream(object obj, Type type, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            obj.IsNullThrow(nameof(obj));
            JsonSerializer.SerializeAsync(stream, obj, Settings).ConfigureAwait(false).GetAwaiter().GetResult();
            if(leaveStreamOpen is false)
                stream?.Dispose();
        }

        public Stream SerializeToStream<T>(T obj, int bufferSize = 1024) where T : class
        {
            obj.IsNullThrow(nameof(obj));
            var stream = new MemoryStream();
            JsonSerializer.SerializeAsync<T>(stream, obj, Settings).ConfigureAwait(false).GetAwaiter().GetResult();
            return stream;
        }

        public Stream SerializeToStream(object obj, Type type, int bufferSize = 1024)
        {
            obj.IsNullThrow(nameof(obj));
            var stream = new MemoryStream();
            JsonSerializer.SerializeAsync(stream,obj,Settings).ConfigureAwait(false).GetAwaiter().GetResult();
            return stream;
        }

        public string SerializeToString(object obj)
        {
            return JsonSerializer.Serialize(obj, Settings);
        }

        public string SerializeToString<T>(T obj) where T : class
        {
           return JsonSerializer.Serialize<T>(obj, Settings);
        }

        public List<dynamic> DeserializeToList(string json)
        {
            json.IsNullThrow(nameof(json));
            var list = JsonSerializer.Deserialize<List<ExpandoObject>>(json, Settings);
            var dynamicList = new List<dynamic>();
            dynamicList.AddRange(list);
            return dynamicList;
        }

        public List<dynamic> DeserializeToList(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            // https://github.com/dotnet/runtime/issues/1574
            var list = JsonSerializer.DeserializeAsync<List<ExpandoObject>>(stream, Settings).ConfigureAwait(false).GetAwaiter()
                .GetResult();

            if(leaveStreamOpen is false)
                stream?.Dispose();

            var dynamicList = new List<dynamic>();
            dynamicList.AddRange(list);
            return dynamicList;
        }

        public List<T> DeserializeToList<T>(string json) where T : class
        {
            json.IsNullThrow(nameof(json));
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        public List<T> DeserializeToList<T>(Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            stream.IsNullThrow(nameof(stream));
            // https://github.com/dotnet/runtime/issues/1574
            var list = JsonSerializer.DeserializeAsync<List<T>>(stream, Settings).ConfigureAwait(false).GetAwaiter()
                .GetResult();
            if (!leaveStreamOpen)
                stream.Dispose();
            return list;
        }

        public List<object> DeserializeToList(string json, Type type)
        {
            json.IsNullThrow(nameof(json));
            // https://github.com/dotnet/runtime/issues/1574
            var list = JsonSerializer.Deserialize(json, type,Settings);
            if (list is IEnumerable<object> listOfObjects)
            {
                return listOfObjects.AsList();
            }
            return null;
        }

        public List<object> DeserializeToList(Stream stream, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            stream.IsNullThrow(nameof(stream));
            // https://github.com/dotnet/runtime/issues/1574
            // https://github.com/dotnet/runtime/issues/31274
            var list = JsonSerializer.DeserializeAsync(stream,type, Settings).ConfigureAwait(false).GetAwaiter()
                .GetResult();
            if(!leaveStreamOpen)
                stream.Dispose();
            if (list is IEnumerable<object> listOfObjects)
            {
                return listOfObjects.AsList();
            }
            throw new InvalidOperationException($"Failed to DeserializeToList of type {type.FullName}");
        }

   

    }
}

#endif