using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using gRPC_PostgreSql_Server;

namespace gRPC_PostgreSql_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            //
            #region GetAllEmployee
            ////
            //await GetAllEmployee(channel);
            ////
            //static async Task GetAllEmployee(GrpcChannel channel)
            //{
            //    var allemploydisplay = new RemoteEmployee.RemoteEmployeeClient(channel);
            //    var emply = new Empty();
            //    var list = await allemploydisplay.GetAllEmployeesAsync(emply);
            //    //
            //    Console.WriteLine("All Employees Display\n");
            //    foreach (var item in list.Items)
            //    {
            //        Console.WriteLine($"{item.EmployeeId} {item.Surname} {item.Name} {item.Gender} {item.DepartmentId}");
            //    }
            //}
            //End GetAllEmployee
            #endregion
            //
            #region FindEmployeeById
            ////
            //await findEmployeeById(channel, 4);
            ////
            //static async Task findEmployeeById(GrpcChannel channel, int id)
            //{
            //    var _findEmployById = new RemoteEmployee.RemoteEmployeeClient(channel);
            //    var input = new EmployeeLookupModel { EmployeeId = id };
            //    var employeeReply = await _findEmployById.GetEmployeebyIdAsync(input);
            //    Console.WriteLine($"{employeeReply.Surname} {employeeReply.Name} {employeeReply.Gender} {employeeReply.DepartmentId}");
            //}
            #endregion
            //
            #region AddNewEmployee
            ////
            //EmployeeModel ojbEmployee = new EmployeeModel()
            //{
            //    EmployeeId = 7,
            //    Surname = "Reta",
            //    Name = "William",
            //    Gender = "F",
            //    DepartmentId = 2
            //};
            //await insertEmployee(channel, ojbEmployee);
            ////
            //static async Task insertEmployee(GrpcChannel channel, EmployeeModel _newEmployee)
            //{
            //    var employnew = new RemoteEmployee.RemoteEmployeeClient(channel);
            //    var reply = await employnew.InsertEmployeeAsync(_newEmployee);
            //    Console.WriteLine(reply.Result);
            //}
            ////End newEmployee
            #endregion
            //
            #region UpdateEmployee
            //// 
            //EmployeeModel updateEmployee = new EmployeeModel()
            //{
            //    EmployeeId = 7,
            //    Surname = "David",
            //    Name = "William",
            //    Gender = "M",
            //    DepartmentId = 2,
            //};
            //await _updateEmployee(channel, updateEmployee);
            ////
            //static async Task _updateEmployee(GrpcChannel channel, EmployeeModel _updateEmployee)
            //{
            //    var employupdate = new RemoteEmployee.RemoteEmployeeClient(channel);
            //    var reply = await employupdate.UpdateEmployeeAsync(_updateEmployee);
            //    Console.WriteLine(reply.Result);
            //}
            ////End updateEmployee
            #endregion

            #region DeleteEmployee
            //
            await deleteEmployee(channel, 7);
            //
            static async Task deleteEmployee(GrpcChannel channel, int id)
            {
                var employdelete = new RemoteEmployee.RemoteEmployeeClient(channel);
                var input = new EmployeeLookupModel { EmployeeId = id };
                var reply = await employdelete.DeleteEmployeeAsync(input);
                Console.WriteLine(reply.Result);
            }
            //End deleteEmployee
            #endregion

            //

            Console.ReadLine();
        }
    }
}
