using System;
using System.Collections.Generic;
using gRPC_PostgreSql_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace gRPC_PostgreSql_Server.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasData(GetEmployees());
        }

        private static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>() {
               new Employee() {
                EmployeeId = 1,
                DepartmentId = 1,
                Surname = "Owamamwen",
                Name = "Ogunniyi",
                Gender = "M"
            },
                new Employee() {
                EmployeeId = 2,
                DepartmentId = 2,
                Surname = "Lorenzo",
                Name = "Nicolo",
                Gender = "M"
            },
                new Employee() {
                EmployeeId = 3,
                DepartmentId = 1,
                Surname = "Vincenzo",
                Name = "Luciano",
                Gender = "M"

            },
                new Employee() {
                EmployeeId = 4,
                DepartmentId = 2,
                Surname = "Filippo",
                Name = "Federica",
                Gender = "F"

            },
                new Employee() {
                EmployeeId = 5,
                DepartmentId = 3,
                Surname = "Stefano",
                Name = "Elisa",
                Gender = "F"

            },
                new Employee() {
                EmployeeId = 6,
                DepartmentId = 3,
                Surname = "Francesco",
                Name = "Anna",
                Gender = "F"
            }
            };
            return employees;
        }
    }
}
