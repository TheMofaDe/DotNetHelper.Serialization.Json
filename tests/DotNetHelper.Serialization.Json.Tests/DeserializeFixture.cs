using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DotNetHelper.Serialization.Json.Tests.Models;
using NUnit.Framework;

namespace DotNetHelper.Serialization.Json.Tests
{
    [TestFixture]
    [NonParallelizable] //since were sharing a single file across multiple test cases we don't want Parallelizable
    public class JsonDeserializerTextFixture
    {


        public DataSourceJson DataSource { get; set; } = new DataSourceJson();

        public JsonDeserializerTextFixture()
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
        public void Test_Deserialize_Json_To_Dynamic()
        {
            var dyn = DataSource.Deserialize(MockData.EmployeeAsJson);
            EnsureDynamicObjectMatchMockData(dyn);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Dynamic_And_Stream_Is_Dispose()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var dyn = DataSource.Deserialize(stream);
            EnsureDynamicObjectMatchMockData(dyn);
            EnsureStreamIsDispose(stream);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Dynamic_And_Stream_Wont_Dispose()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var dyn = DataSource.Deserialize(stream, 1024, true);
            EnsureDynamicObjectMatchMockData(dyn);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Json_To_Generic()
        {
            var employee = DataSource.Deserialize<Employee>(MockData.EmployeeAsJson);
            EnsureGenericObjectMatchMockData(employee);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Generic_And_Stream_Is_Dispose()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var dyn = DataSource.Deserialize<Employee>(stream);
            EnsureGenericObjectMatchMockData(dyn);
            EnsureStreamIsDispose(stream);

        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Generic()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var dyn = DataSource.Deserialize<Employee>(stream, 1024, true);
            EnsureGenericObjectMatchMockData(dyn);
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);

        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Json_To_Typed_Object()
        {
            var employee = DataSource.Deserialize(MockData.EmployeeAsJson, typeof(Employee));
            dynamic dyn = employee;
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
        }

        // TODO ADDED
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Json_To_Typed_Object_Of_List()
        {
            var employees = DataSource.Deserialize(MockData.EmployeeAsJsonList, typeof(List<Employee>));
            var employeesStronglyType = employees as List<Employee>;
            EnsureFirstNameAndLastNameMatchMockData(employeesStronglyType.First().FirstName, employeesStronglyType.First().LastName);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Json_To_Strongly_Typed_Object_Of_List()
        {
            var employees = DataSource.DeserializeToList(MockData.EmployeeAsJsonList, typeof(List<Employee>));
            if (!employees.Any()) Assert.Fail(" didn't deserialize correctly");
            var employee = employees.First() as Employee;
            EnsureFirstNameAndLastNameMatchMockData(employee?.FirstName, employee?.LastName);
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Typed_Object_And_Stream_Is_Dispose()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var employee = DataSource.Deserialize(stream, typeof(Employee));
            dynamic dyn = employee;
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
            EnsureStreamIsDispose(stream);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Typed_Object_And_Stream_Wont_Dispose()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var employee = DataSource.Deserialize(stream, typeof(Employee), 1024, true);
            dynamic dyn = employee;
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }





        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Json_To_Dynamic_List()
        {
            var employees = DataSource.DeserializeToList(MockData.EmployeeAsJsonList);
            EnsureFirstNameAndLastNameMatchMockData(employees.First().FirstName.ToString(), employees.First().LastName.ToString());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Dynamic_List_And_Stream_Is_Dispose()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            var dyn = DataSource.DeserializeToList(stream);
            EnsureDynamicObjectMatchMockData(dyn.First());
            EnsureStreamIsDispose(stream);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Dynamic_List_And_Stream_Wont_Dispose()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            var dyn = DataSource.DeserializeToList(stream, 1024, true);
            EnsureDynamicObjectMatchMockData(dyn.First());
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Json_To_Generic_List()
        {
            var employees = DataSource.DeserializeToList<Employee>(MockData.EmployeeAsJsonList);
            EnsureGenericObjectMatchMockData(employees.First());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Generic_List_And_Stream_Is_Dispose()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            var dyn = DataSource.DeserializeToList<Employee>(stream);
            EnsureDynamicObjectMatchMockData(dyn.First());
            EnsureStreamIsDispose(stream);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Generic_List_And_Stream_Wont_Dispose()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            var dyn = DataSource.DeserializeToList<Employee>(stream, 1024, true);
            EnsureDynamicObjectMatchMockData(dyn.First());
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Typed_Object_List_And_Stream_Is_Dispose()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            List<dynamic> list = DataSource.DeserializeToList(stream, typeof(List<Employee>));
            EnsureFirstNameAndLastNameMatchMockData(list.First().FirstName.ToString(), list.First().LastName.ToString());
            EnsureStreamIsDispose(stream);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_Deserialize_Stream_To_Typed_Object_List_And_Stream_Wont_Dispose()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            List<dynamic> list = DataSource.DeserializeToList(stream, typeof(List<Employee>), 1024, true);
            EnsureFirstNameAndLastNameMatchMockData(list.First().FirstName.ToString(), list.First().LastName.ToString());
            EnsureStreamIsNotDisposeAndIsAtEndOfStream(stream);
        }


        private void EnsureFirstNameAndLastNameMatchMockData(string firstName, string lastName)
        {
            if (firstName.Equals(MockData.Employee.FirstName) && lastName.Equals(MockData.Employee.LastName))
            {

            }
            else
            {
                Assert.Fail("Dynamic Object doesn't matches expected results");
            }
        }

        private void EnsureDynamicObjectMatchMockData(dynamic dyn)
        {
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
        }

        private void EnsureGenericObjectMatchMockData(Employee employee)
        {
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
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




    }
}