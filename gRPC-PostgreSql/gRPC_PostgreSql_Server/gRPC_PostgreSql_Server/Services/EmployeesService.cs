using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using gRPC_PostgreSql_Server.Context;
using gRPC_PostgreSql_Server.Models;
using Microsoft.Extensions.Logging;

namespace gRPC_PostgreSql_Server.Services
{
    public class EmployeesService : RemoteEmployee.RemoteEmployeeBase
    {
        private readonly ILogger<EmployeesService> _logger;
        private readonly EmployeeContext _context;

        public EmployeesService(ILogger<EmployeesService> logger, EmployeeContext context)
        {
            _logger = logger;
            _context = context;
        }
        //
        #region GetAllEmployees
        public override async Task<EmployeeList> GetAllEmployees(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Display all employees");

            EmployeeList list = new EmployeeList();

            try
            {
                List<EmployeeModel> employeeList = new List<EmployeeModel>();

                var _employee = _context.Employees.ToList();

                foreach (var e in _employee)
                {
                    employeeList.Add(new EmployeeModel()
                    {
                        EmployeeId = e.EmployeeId,
                        Surname = e.Surname,
                        Name = e.Name,
                        Gender = e.Gender,
                        DepartmentId = (int)e.DepartmentId,
                    });
                }
                list.Items.AddRange(employeeList);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }
            return await Task.FromResult(list);
        }
        #endregion
        //
        #region GetEmployeebyId
        public override async Task<EmployeeModel> GetEmployeebyId(EmployeeLookupModel request, ServerCallContext context)
        {
            // from employee.proto
            EmployeeModel objemployee = new EmployeeModel();

            // EmployeeContext.cs from contructor
            var _employee = await _context.Employees.FindAsync(request.EmployeeId);

            _logger.LogInformation("Employee response");

            if (_employee != null)
            {
                objemployee.EmployeeId = _employee.EmployeeId;
                objemployee.Surname = _employee.Surname;
                objemployee.Name = _employee.Name;
                objemployee.Gender = _employee.Gender;
                objemployee.DepartmentId = (int)_employee.DepartmentId;
            }
            return await Task.FromResult(objemployee);
        }
        #endregion
        //
        #region InsertEmployee
        public override async Task<Reply> InsertEmployee(EmployeeModel request, ServerCallContext context)
        {
            var em = await _context.Employees.FindAsync(request.EmployeeId);
            if(em != null)
            {
                return await Task.FromResult(new Reply() {
                    Result = $"Employee {request.Surname} {request.Name} {request.Gender} {request.DepartmentId}",
                    IsOk = false
                });
            }
            Employee _employee = new Employee()
            {
                EmployeeId = request.EmployeeId,
                Surname = request.Surname,
                Name = request.Name,
                Gender = request.Gender,
                DepartmentId = request.DepartmentId
            };
            _logger.LogInformation("Insert Employee");

            try
            {
                _context.Employees.Add(_employee);
                int reValues = _context.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }
            return await Task.FromResult(new Reply
            {
                Result = $"Employee {request.Surname} {request.Name} {request.Gender} {request.DepartmentId} Successfully insert.",
                IsOk = true
            });
        }
        #endregion
        //
        #region UpdateEmployee
        public override async Task<Reply> UpdateEmployee(EmployeeModel request, ServerCallContext context)
        {
            var em = await _context.Employees.FindAsync(request.EmployeeId);
            if (em == null)
            {
                return await Task.FromResult(new Reply()
                {
                    Result = $"Employee {request.Surname} {request.Name} {request.Gender} {request.DepartmentId} Can't be found.",
                    IsOk = false
                });
            }

            em.EmployeeId = request.EmployeeId;
            em.Surname = request.Surname;
            em.Name = request.Name;
            em.Gender = request.Gender;
            em.DepartmentId = request.DepartmentId;

            _logger.LogInformation("Update Employee");

            try
            {
                int reValues = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }
            return await Task.FromResult(new Reply
            {
                Result = $"Employee {request.Surname} {request.Name} {request.Gender} {request.DepartmentId} Successfully update",
                IsOk = true
            });
        }
        #endregion
        //
        #region DeleteEmployee
        public override async Task<Reply> DeleteEmployee(EmployeeLookupModel request, ServerCallContext context)
        {
            var em = await _context.Employees.FindAsync(request.EmployeeId);
            if (em == null)
            {
                return await Task.FromResult(new Reply()
                {
                    Result = $"Employee {request.EmployeeId} Can't be found.",
                    IsOk = false
                });
            }

            _logger.LogInformation("Delete Employee");

            try
            {
                _context.Employees.Remove(em);
                int reValues = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }
            return await Task.FromResult(new Reply
            {
                Result = $"Employee {request.EmployeeId} Successfully delete",
                IsOk = true
            });
        }
        #endregion
    }
}
