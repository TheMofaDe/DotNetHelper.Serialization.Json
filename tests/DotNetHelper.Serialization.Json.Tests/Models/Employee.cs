namespace DotNetHelper.Serialization.Json.Tests.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Employee()
        {
            FirstName = "Joseph";
            LastName = "McNeal";
        }
    }
}
