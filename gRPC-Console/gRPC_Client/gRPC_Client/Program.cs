using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using gRPC_Services;

namespace gRPC_Client
{
    class Program
    {
        //static void Main(string[] args)
        static async Task Main(string[] args)
        {
            // GreeterService
            //var input = new HelloRequest { Name = "Owamamwen" };
            //var channel = GrpcChannel.ForAddress("http://localhost:5000");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(input);
            //Console.WriteLine(reply.Message);
            //
            //
            // CustomersSerive
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var customerClient = new Customer.CustomerClient(channel);
            var clientRequested = new CustomerLookModel { UserId = 2 };
            var customer = await customerClient.GetCustomerInfoAsync(clientRequested);
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            // NewCustomer
            Console.WriteLine("\n New Customer List \n");

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} {currentCustomer.EmailAddress} {currentCustomer.Age} {currentCustomer.IsAlive}");
                }

                Console.ReadLine();

            }
        }
    }
}