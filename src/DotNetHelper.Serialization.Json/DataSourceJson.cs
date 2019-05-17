using System;
using System.Collections.Generic;
using System.IO;
using DotNetHelper.Serialization.Abstractions.Interface;

namespace DotNetHelper.Serialization.Json
{
    public class DataSourceJson : ISerializer
    {
        public dynamic Deserialize(string content)
        {
            throw new NotImplementedException();
        }

        public dynamic Deserialize(Stream content)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string content) where T : class
        {
            throw new NotImplementedException();

        }

        public T Deserialize<T>(Stream stream) where T : class
        {
            throw new NotImplementedException();
        }

        public object Deserialize(string content, Type type)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(Stream stream, Type type)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> DeserializeToList(string content)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> DeserializeToList(Stream stream)
        {
            throw new NotImplementedException();
        }

        public List<T> DeserializeToList<T>(string content) where T : class
        {
            throw new NotImplementedException();
        }

        public List<T> DeserializeToList<T>(Stream stream) where T : class
        {
            throw new NotImplementedException();
        }

        public List<object> DeserializeToList(string content, Type type)
        {
            throw new NotImplementedException();
        }

        public List<object> DeserializeToList(Stream stream, Type type)
        {
            throw new NotImplementedException();
        }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public void SerializeToStream<T>(T obj, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            throw new NotImplementedException();
        }

        public void SerializeToStream(object obj, Type type, Stream stream, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            throw new NotImplementedException();
        }

        public Stream SerializeToStream<T>(T obj, int bufferSize = 1024, bool leaveStreamOpen = false) where T : class
        {
            throw new NotImplementedException();
        }

        public Stream SerializeToStream(object obj, Type type, int bufferSize = 1024, bool leaveStreamOpen = false)
        {
            throw new NotImplementedException();
        }

        public string SerializeToString(object obj)
        {
            throw new NotImplementedException();
        }

        public string SerializeToString<T>(T obj) where T : class
        {
            throw new NotImplementedException();
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
