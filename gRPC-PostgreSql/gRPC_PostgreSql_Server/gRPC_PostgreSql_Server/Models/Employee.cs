using System;
namespace gRPC_PostgreSql_Server.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int? DepartmentId { get; set; }
    }
}
