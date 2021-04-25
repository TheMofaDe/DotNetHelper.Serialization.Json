using System;
using System.IO;
using System.Text;
using DotNetHelper.Serialization.Json.Tests.Models;
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
#if NET452
            DataSource = new DataSourceJson(Encoding.UTF8);
#else
            DataSource = new DataSourceJson(Encoding.UTF8, JsonHelper.DefaultOptions);
#endif

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
            DataSource.SerializeToStream(MockData.Employee, stream, 1024, true);
            stream.Seek(0, SeekOrigin.Begin);
            EnsureStreamMatchMockDataJson(stream, MockData.GetEmployeeAsStream(DataSource.Encoding));
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }



        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_My_Stream_And_Stream_Is_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, stream, 1024, false);
            EnsureStreamIsDispose(stream);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_My_Stream_And_Stream_Wont_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, typeof(Employee), stream, 1024, true);
            EnsureStreamMatchMockDataJson(stream, MockData.GetEmployeeAsStream(DataSource.Encoding));
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_My_Stream_And_Stream_Is_Dispose()
        {
            var stream = new MemoryStream();
            DataSource.SerializeToStream(MockData.Employee, typeof(Employee), stream, 1024, false);
            EnsureStreamIsDispose(stream);
        }




        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_Stream_And_Stream_Wont_Dispose()
        {

            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.Employee, 1024));
            EnsureStreamMatchMockDataJson(stream, MockData.GetEmployeeAsStream(DataSource.Encoding));
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_List_To_Stream_And_Stream_Wont_Dispose()
        {
            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.EmployeeList, 1024));
            EnsureStreamMatchMockDataJson(stream, MockData.GetEmployeeListAsStream(DataSource.Encoding));
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Generic_To_Stream_And_Stream_Is_Dispose()
        {
            var stream = DataSource.SerializeToStream(MockData.Employee, 1024);
            EnsureStreamMatchMockDataJson(stream, MockData.GetEmployeeAsStream(DataSource.Encoding));
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }



        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Stream_And_Stream_Wont_Dispose()
        {

            var stream = Stream.Synchronized(DataSource.SerializeToStream(MockData.Employee, MockData.Employee.GetType(), 1024));
            EnsureStreamMatchMockDataJson(stream, MockData.GetEmployeeAsStream(DataSource.Encoding));
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Serialize_Object_To_Stream_And_Stream_Is_Dispose()
        {
            var stream = DataSource.SerializeToStream(MockData.Employee, MockData.Employee.GetType(), 1024);
            EnsureStreamMatchMockDataJson(stream, MockData.GetEmployeeAsStream(DataSource.Encoding));
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


        private void EnsureStreamMatchMockDataJson(Stream stream, Stream streamToMatch)
        {
            stream.Seek(0, SeekOrigin.Begin);
            Assert.IsTrue(CompareStreams(stream, streamToMatch), "Stream doesn't match");
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

            var c = new StreamReader(a, DataSource.Encoding).ReadToEnd();
            var d = new StreamReader(b, DataSource.Encoding).ReadToEnd();
            return c.Equals(d, StringComparison.CurrentCulture);
            //if (a.Length < b.Length)
            //    return false;
            //if (a.Length > b.Length)
            //    return false;

            //for (int i = 0; i < b.Length; i++)
            //{
            //    int aByte = a.ReadByte();
            //    int bByte = b.ReadByte();
            //    if (aByte.CompareTo(bByte) != 0)
            //        return false;
            //}
        }
    }
}