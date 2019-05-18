using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetHelper.Serialization.Json.Tests.Models;

namespace DotNetHelper.Serialization.Json.Tests
{
    public static class MockData
    {

        public static Employee Employee { get; } = new Employee();
        public static string EmployeeAsJson { get; } = @"{""FirstName"":""Joseph"",""LastName"":""McNeal""}";

        public static List<Employee> EmployeeList { get; } = new List<Employee>() { Employee };
        public static string EmployeeAsJsonList { get; } = @"[{""FirstName"":""Joseph"",""LastName"":""McNeal""}]";

        public static Stream GetEmployeeAsStream(Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(EmployeeAsJson));
        }
        public static Stream GetEmployeeListAsStream(Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(EmployeeAsJsonList));
        }
    }
}
