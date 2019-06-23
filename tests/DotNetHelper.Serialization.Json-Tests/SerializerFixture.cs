using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using DotNetHelper.Serialization.Json.Tests.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DotNetHelper.Serialization.Json.Tests
{
    [TestFixture]
    [NonParallelizable] //since were sharing a single file across multiple test cases we don't want Parallelizable
    public class JsonSerializerTextFixture 
    {
        

        public DataSourceJson DataSource { get; set; } = new DataSourceJson();

        public JsonSerializerTextFixture()
        {

        }


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            DataSource = new DataSourceJson(Encoding.UTF8,new JsonSerializerSettings());
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {

        }



        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void Cleanup()
        {

        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_Json()
        {
            var json = DataSource.SerializeToString(MockData.Employee);
            EnsureGenericObjectMatchMockDataJson(json);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Json()
        {
            var json = DataSource.SerializeToString((object)MockData.Employee);
            EnsureGenericObjectMatchMockDataJson(json);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_My_Stream_And_Stream_Wont_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee,stream,1024,true);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_My_Stream_And_Stream_Is_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, stream, 1024, false);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsDispose(stream);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_My_Stream_And_Stream_Wont_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee,typeof(Employee), stream, 1024, true);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_My_Stream_And_Stream_Is_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, typeof(Employee), stream, 1024, false);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsDispose(stream);
        }




        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_Stream_And_Stream_Wont_Dispose()
        {

            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.Employee, 1024));
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_Stream_And_Stream_Is_Dispose()
        {

            var stream = DataSource.SerializeToStream(MockData.Employee, 1024);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }



        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Stream_And_Stream_Wont_Dispose()
        {

            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.Employee,MockData.Employee.GetType(), 1024));
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Stream_And_Stream_Is_Dispose()
        {

            var stream = DataSource.SerializeToStream(MockData.Employee, MockData.Employee.GetType(),1024);
            // TODO :: EnsureStreamMatchMockDataJson(stream);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }





        private void EnsureGenericObjectMatchMockDataJson(string json)
        {
            var equals = string.Equals(json, MockData.EmployeeAsJson, StringComparison.OrdinalIgnoreCase);
            Assert.IsTrue(equals, $"Test failed due to json not matching mock data json");
        }

        private void EnsureStreamIsNotDisposeAndIsAtEndOfStream(Stream stream)
        {
            try
            {
                if (stream.Position != stream.Length)
                {
                    Assert.Fail("The entire stream has not been read");
                }
            }
            catch (ObjectDisposedException disposedException)
            {
                Assert.Fail($"The stream has been disposed {disposedException.Message}");
            }

        }


        private void EnsureStreamIsDispose(Stream stream)
        {
            try
            {
                var position = stream.Position;
                Assert.Fail("The stream is not disposed.");
            }
            catch (ObjectDisposedException)
            {
                return;
            }
        }



        private bool CompareStreams(Stream a, Stream b)
        {
            if (a == null &&
                b == null)
                return true;
            if (a == null ||
                b == null)
            {
                throw new ArgumentNullException(
                    a == null ? "a" : "b");
            }

            if (a.Length < b.Length)
                return false;
            if (a.Length > b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                int aByte = a.ReadByte();
                int bByte = b.ReadByte();
                if (aByte.CompareTo(bByte) != 0)
                    return false;
            }

            return true;
        }
    }
}