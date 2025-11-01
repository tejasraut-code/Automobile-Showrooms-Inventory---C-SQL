using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileShowroom
{
    public class Invoice
    {
        public int InvoiceNo { get; set; }
        public string Date { get; set; }
        public string VehicleId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int AccessoryId { get; set; }
        public string IsLoan { get; set; }
        public float TotalAmount { get; set; }
    }

    public class Accessorise
    {
        public int AccessoryId { get; set; }
        public string AccessoryName { get; set; }
        public float Price { get; set; }
    }

    public partial class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleType { get; set; }
        public string VehicleBrand { get; set; }

        public string VehicleModel { get; set; }
        public float VehiclePrice { get; set; }
    }

    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }

    internal class Class1
    {
    }
}


using System;
using System.Data.SqlClient;

namespace AutomobileShowroom
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }

    public class CustomerRepository
    {
        private SqlConnection conn;

        public CustomerRepository(SqlConnection connection)
        {
            conn = connection;
        }

        public void AddCustomer(Customer cust)
        {
            string query = "INSERT INTO Customer (CustomerName, CustomerAddress, Contact) VALUES (@name, @address, @contact)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", cust.CustomerName);
            cmd.Parameters.AddWithValue("@address", cust.Address);
            cmd.Parameters.AddWithValue("@contact", cust.Contact);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Customer added successfully.");
        }

        public void ShowAllCustomers()
        {
            string query = "SELECT * FROM Customer";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Customer List ---");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["CustomerId"]}, Name: {reader["CustomerName"]}, Address: {reader["CustomerAddress"]}, Contact: {reader["Contact"]}");
            }
            reader.Close();
        }

        public void UpdateCustomer(Customer cust)
        {
            string query = "UPDATE Customer SET CustomerName = @name, CustomerAddress = @address, Contact = @contact WHERE CustomerId = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", cust.CustomerName);
            cmd.Parameters.AddWithValue("@address", cust.Address);
            cmd.Parameters.AddWithValue("@contact", cust.Contact);
            cmd.Parameters.AddWithValue("@id", cust.CustomerId);
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Customer updated successfully." : "Customer not found.");
        }

        public void DeleteCustomer(int id)
        {
            string query = "DELETE FROM Customer WHERE CustomerId = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Customer deleted successfully." : "Customer not found.");
        }
    }

    internal class Program
    {
        static string connStr = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=AutomobileShowroom;Integrated Security=True;";

        static void Main()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                CustomerRepository repo = new CustomerRepository(conn);

                while (true)
                {
                    Console.WriteLine("\n--- CUSTOMER CRUD MENU ---");
                    Console.WriteLine("1. Add Customer");
                    Console.WriteLine("2. Show All Customers");
                    Console.WriteLine("3. Update Customer");
                    Console.WriteLine("4. Delete Customer");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Address: ");
                            string address = Console.ReadLine();
                            Console.Write("Enter Contact: ");
                            string contact = Console.ReadLine();
                            repo.AddCustomer(new Customer { CustomerName = name, Address = address, Contact = contact });
                            break;

                        case "2":
                            repo.ShowAllCustomers();
                            break;

                        case "3":
                            Console.Write("Enter Customer ID to Update: ");
                            int updateId = int.Parse(Console.ReadLine());
                            Console.Write("Enter New Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Enter New Address: ");
                            string newAddress = Console.ReadLine();
                            Console.Write("Enter New Contact: ");
                            string newContact = Console.ReadLine();
                            repo.UpdateCustomer(new Customer { CustomerId = updateId, CustomerName = newName, Address = newAddress, Contact = newContact });
                            break;

                        case "4":
                            Console.Write("Enter Customer ID to Delete: ");
                            int deleteId = int.Parse(Console.ReadLine());
                            repo.DeleteCustomer(deleteId);
                            break;

                        case "5":
                            Console.WriteLine("Exiting program.");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        }
    }
}


switch ((ch)
)
{
    case 1:

        Console.WriteLine(" Customer Database ");
        Console.WriteLine(" 1.Add Customer      2.Update Customre Data      3.Delete Customer Data " +
            "   4.Show Customer DataBase. ");
        int customerch = Convert.ToInt32(Console.ReadLine());

        switch (customerch)
        {
            case 1: CustomerAdd(cDatabase); break;
            case 2: CustomerUpdate(cDatabase); break;
            case 3: CustomerDelete(cDatabase); break;
            case 4: CustomerShow(cDatabase); break;

        }

        break;
    case 2:
        break;




        ShowInvoice(conn);
    default:
        break;
}
