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
        public void Test_DeserializeJsonToDynamic()
        {
            var dyn = DataSource.Deserialize(MockData.EmployeeAsJson);
            EnsureDynamicObjectMatchMockData(dyn);
        }
        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeStreamToDynamic()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var dyn = DataSource.Deserialize(stream);
            EnsureDynamicObjectMatchMockData(dyn);
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeJsonToGeneric()
        {
            var employee = DataSource.Deserialize<Employee>(MockData.EmployeeAsJson);
            EnsureGenericObjectMatchMockData(employee);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeStreamToGeneric()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var dyn = DataSource.Deserialize<Employee>(stream);
            EnsureGenericObjectMatchMockData(dyn);
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeJsonStringToTypedObject()
        {
            var employee = DataSource.Deserialize(MockData.EmployeeAsJson, typeof(Employee));
            dynamic dyn = employee;
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeStreamToTypedObject()
        {
            var stream = MockData.GetEmployeeAsStream(DataSource.Encoding);
            var employee = DataSource.Deserialize(stream, typeof(Employee));
            dynamic dyn = employee;
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName.ToString(), dyn.LastName.ToString());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeJsonToListDynamic()
        {
            var employees = DataSource.DeserializeToList(MockData.EmployeeAsJsonList);
            EnsureFirstNameAndLastNameMatchMockData(employees.First().FirstName.ToString(), employees.First().LastName.ToString());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeStreamToDynamicList()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            var dyn = DataSource.DeserializeToList(stream);
            EnsureDynamicObjectMatchMockData(dyn.First());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeJsonToGenericList()
        {
            var employees = DataSource.DeserializeToList<Employee>(MockData.EmployeeAsJsonList);
            EnsureGenericObjectMatchMockData(employees.First());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeStreamToGenericList()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            var dyn = DataSource.DeserializeToList<Employee>(stream);
            EnsureDynamicObjectMatchMockData(dyn.First());
        }


        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeJsonToTypedObjectList()
        {
            var list = DataSource.DeserializeToList(MockData.EmployeeAsJsonList,typeof(List<Employee>));
            EnsureDynamicObjectMatchMockData(list.First());
        }

        [Author("Joseph McNeal Jr", "josephmcnealjr@gmail.com")]
        [Test]
        public void Test_DeserializeStreamToTypedObjectList()
        {
            var stream = MockData.GetEmployeeListAsStream(DataSource.Encoding);
            List<dynamic> list = DataSource.DeserializeToList(stream, typeof(List<Employee>));
            EnsureFirstNameAndLastNameMatchMockData(list.First().FirstName.ToString(), list.First().LastName.ToString());
        }


        private void EnsureFirstNameAndLastNameMatchMockData(string firstName,string lastName)
        {
            if (firstName.Equals(MockData.Employee.FirstName) && lastName.Equals(MockData.Employee.LastName))
            {
                // Assert.Pass("Dynamic Object matches expected results");
            }
            else
            {
                Assert.Fail("Dynamic Object doesn't matches expected results");
            }
        }

        private void EnsureDynamicObjectMatchMockData(dynamic dyn)
        {
            EnsureFirstNameAndLastNameMatchMockData(dyn.FirstName, dyn.LastName);
        }

        private void EnsureGenericObjectMatchMockData(Employee employee)
        {
            EnsureFirstNameAndLastNameMatchMockData(employee.FirstName, employee.LastName);
        }

        private void ShouldMatchMockJsonString(string json)
        {
            var equals = string.Equals(json, MockData.EmployeeAsJson, StringComparison.OrdinalIgnoreCase);
            Assert.IsTrue(equals, $"Test failed due to json not matching mock data");
        }
        private void ShouldMatchMockObject(Employee employee)
        {
            var equals = employee.FirstName == MockData.Employee.FirstName && employee.LastName == MockData.Employee.LastName;
            Assert.IsTrue(equals, $"Test failed due to json not matching mock data");
        }
        private void EnsureEmployeeValueMatch(dynamic employee)
        {
            var equals = employee.FirstName == MockData.Employee.FirstName && employee.LastName == MockData.Employee.LastName;
            Assert.IsTrue(equals, $"Test filed due to json not matching mock data");
        }


    }
}