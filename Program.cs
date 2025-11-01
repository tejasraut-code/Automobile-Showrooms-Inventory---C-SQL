using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;


namespace AutomobileShowroom
{

    public  class customer
    {
        public int CustomerId { get; set; } //pk
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int choice { get; set; }
    }
    public  class employee
    {
        public int EmployeeId { get; set; } //pk
        public string EmployeeName { get; set; }
    }
    public class vehicle
    {
        public int VehicleId { get; set; } //pk
        public string VehicleType { get; set; }
        public string VehicleBrand { get; set; }

        public string VehicleModel { get; set; }
        public float VehiclePrice { get; set; }
    }

    public class accessorise
    {
        public int AccessoryId { get; set; } //pk
        public string AccessoryName { get; set; }
        public float Price { get; set; }
    }
    public class invoice
    {
        public int InvoiceNo { get; set; } //pk
        public string Billdate { get; set; }
        public string VehicleId { get; set; } //fk
        public int CustomerId { get; set; } //fk
        public int EmployeeId { get; set; } //fk
        public int AccessoryId { get; set; } 
        public string IsLoan { get; set; }
        public float TotalAmount { get; set; }
    }
    public class CustomerDatabase
    {
        public SqlConnection conn;

        public CustomerDatabase(SqlConnection conn)
        {
            this.conn = conn;
        }

        public void AddCustomer(customer c)
        {
            string add= "insert into Customer(CustomerName,CustomerAddress,Customercontact) " +
                "values (@Name,@Address,@Contact )";
            SqlCommand cmd = new SqlCommand(add, conn);
            cmd.Parameters.AddWithValue("@Name",c.CustomerName);
            cmd.Parameters.AddWithValue("@Address",c.Address);
            cmd.Parameters.AddWithValue("@Contact", c.Contact);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Inserted");
        }

        public void Updatecustomer(customer c)
        {
            string query = "";
            SqlCommand cmd = new SqlCommand();

            switch (c.choice)
            { 
                case 1:
                    query = "update Customer set CustomerName=@Name where Customerid =@id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", c.CustomerName);
                    Console.WriteLine(" Name Update Successfull");
                    break;

                case 2:
                    query = "update Customer set CustomerAddress=@Address where Customerid =@id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Address", c.Address);
                    Console.WriteLine(" Address Update Successfull");
                    break;

                case 3:
                    query = "update Customer set Customercontact=@Contact where Customerid =@id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Contact", c.Contact);
                    Console.WriteLine(" Address Update Successfull");
                    break;

                case 4:
                    query = "update Customer set CustomerName=@Name,CustomerAddress=@Address,Customercontact=@Contact where Customerid =@id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", c.CustomerName);
                    cmd.Parameters.AddWithValue("@Address", c.Address);
                    cmd.Parameters.AddWithValue("@Contact", c.Contact);
                    //cmd.ExecuteNonQuery();
                    Console.WriteLine("Update Successfull");
                    break;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
            cmd.Parameters.AddWithValue("@Id", c.CustomerId);
            cmd.ExecuteNonQuery();

        }

        public void Deletecustomre( int id)
        {
            string del = "delete from Customer where Customerid=@Id";
            SqlCommand cmd =new SqlCommand(del, conn);
            cmd.Parameters.AddWithValue("@Id",id);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Deleted Successfully");
        }

        public void Showcustomer()
        {
            string show = "select * from Customer";
            SqlCommand cmd = new SqlCommand(show, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("-------------------------------------------------------------------------------");
            while (reader.Read())
            {
                Console.WriteLine($" CustomerId: {reader["CustomerId"]}, Customer Name:{reader["CustomerName"]}" +
                    $"  Address:{reader["CustomerAddress"]},  Contact:{reader["Customercontact"]} ");
                Console.WriteLine("-------------------------------------------------------------------------------");
            }
            reader.Close();
        }
    }


    public class GenerateInvoice
    {
       public SqlConnection conn;
       public GenerateInvoice(SqlConnection conn)
            { this.conn = conn; }
        //1.Generate Bill
       public void BillGenerate(invoice i)
        {
            string query = "insert into Records(VehicleId,customerId,EmployeeId,IsLoan,TotalAmount)" +
                "values (@VehicleId,@CustomerId,@EmployeeId,@IsLoan,@TotalAmount)";
            SqlCommand cmd = new SqlCommand (query, conn);
            cmd.Parameters.AddWithValue("@VehicleId", i.VehicleId);
            cmd.Parameters.AddWithValue("@CustomerId",i.CustomerId);
            cmd.Parameters.AddWithValue("@EmployeeId", i.EmployeeId);
            cmd.Parameters.AddWithValue("@IsLoan",i.IsLoan);
            cmd.Parameters.AddWithValue("@TotalAmount",i.TotalAmount);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Bill Added in Records");
       }
        //2.Sales Database
        public void ShowRecordsDatabase()
        {
            string query = "select * from Records";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader r = cmd.ExecuteReader();
            Console.WriteLine("-------------------------------------------------------------------------------");
            while (r.Read())
            {
                Console.WriteLine($" InvoiceNo.:{r["InvoiceNo"]}, BillDate:{r["BillDate"]}, Vehicle ID:{r["VehicleId"]}" +
                 $" CustomreId:{r["CustomerId"]}, Employee ID:{r["EmployeeId"]},\n Loan Status:{r["IsLoan"]}, TotalAmount:{r["TotalAmount"]}");
                Console.WriteLine("-------------------------------------------------------------------------------");
            }
            r.Close();
        }

        //InvoiceforCustomer
        public void customerinvoice(invoice i)
        {
            string query = "select r.InvoiceNo,r.BillDate,r.VehicleId,v.VehicleType,v.VehicleBrand," +
                "VehicleModel, r.CustomerId,c.CustomerName,c.CustomerAddress,c.Customercontact," +
                "e.EmployeeId,e.EmployeeName, r.IsLoan,r.TotalAmount from Records r" +
                "join Employee e on e.EmployeeId=r.EmployeeId" +
                "join Vehicle v on v.VehicleId=r.VehicleId  " +
                "join Customer c on c.Customerid=r.CustomerId where c.Customerid=@CustomerId;";
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    Console.WriteLine($" Invoice for Customer:{r["r.CustomerId"]},\n" +
                        $"InvoiceNo.:{r["r.InvoiceNo"]}, BillDate:{r["r.BillDate"]}, VehicleId:{r["r.VehicleId"]}, Vehicle Type:{r["v.VehicleType"]}" +
                        $"VehicleBrand:{r["v.VehicleBrand"]},VehicleModel:{r["v.VehicleModel"]},CustomerId:{r["c.CustomerId"]},CustomerName:{r["c.CustomerName"]}," +
                        $"CustomerAddress:{r["c.CustomerAddress"]},Customercontact:{r["c.Customercontact"]},EmployeeId:{r["r.EmployeeId"]}," +
                        $"EmployeeName:{r["e.EmployeeName"]},Vehicle on loan:{r["r.isLoan"]},Total Bill:{r["r.TotalAmount"]}");
                }
                r.Close();
            }
        }
    }


    public class VehicleDatabase
    { 
        public SqlConnection conn;

        public VehicleDatabase(SqlConnection conn)
        {
            this.conn = conn;
        }

        public void ShowVehicledatabase()
        {
            string query = "select * from Vehicle";
            SqlCommand cmd = new SqlCommand(query,conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("-------------------------------------------------------------------------------");
            while (reader.Read())
            {
                Console.WriteLine($"Vehicle ID:{reader["VehicleId"]}, VehicleType:{reader["VehicleType"]}," +
                    $" Vehicle Brand:{reader["VehicleBrand"]}, VehicleModel:{reader["VehicleModel"]}," +
                    $" Vehicle Price:{reader["VehiclePrice"]}");
                Console.WriteLine("-------------------------------------------------------------------------------");
            }
        }
    }



    internal class Program
    {
        static string s1 = "Data Source=MSI\\SQLEXPRESS;Initial Catalog = AutomobileShowroom; Integrated Security = True;";
        static SqlConnection conn = new SqlConnection(s1);

        static void Main()
        {
            conn.Open();
            CustomerDatabase cDatabase = new CustomerDatabase(conn);
            GenerateInvoice invoice = new GenerateInvoice(conn);
            VehicleDatabase vehicledb = new VehicleDatabase(conn);

            Console.WriteLine(" Automobile Showroom \n");
            //Console.WriteLine("1.Customer Page  2. Admin Page");
            //Console.WriteLine(" 0.Buy Vehicle  1.Customer Database     2. Invoice Database     3.Check Records");


            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Invoice Database \n  0.Vehicle's Details  1.Generate Bill    2.SalesDatabase      3.Get Invoice For Customer");
            Console.WriteLine("-------------------------------------------------------------------------------");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 0:
                    //CustomerAdd(cDatabase);
                    databasevehicle(vehicledb);
                    Console.WriteLine("Enter Vehicle ID:");
                    string vhip = Console.ReadLine();

                    break;
                case 1:
                    InvoiceGenerat(invoice);
                    break;
                case 2:
                    SalesDatabase(invoice);
                    break;
                case 3:
                    Invoiceforcustomre(invoice);
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }

            //int ch = Convert.ToInt32(Console.ReadLine());

            ////Customre Database
            //if(ch == 1)
            //{
            //    Console.WriteLine(" Customer Database ");
            //    Console.WriteLine(" 1.Add Customer      2.Update Customre Data      3.Delete Customer Data " +
            //        "   4.Show Customer DataBase. ");
            //    int customerch = Convert.ToInt32(Console.ReadLine());
            //    switch(customerch)
            //    {
            //        case 1:
            //            CustomerAdd(cDatabase);
            //            break;
            //        case 2:
            //            CustomerUpdate(cDatabase);
            //            break;
            //        case 3:
            //            CustomreDelete(cDatabase);
            //            break;
            //        case 4:
            //            CustomerShow(cDatabase);
            //            //cDatabase.Showcustomer();
                        
            //            break;

            //    }

            //}

            ////Invoice Database
            //else if(ch == 2)
            //{
            //    Console.WriteLine("-------------------------------------------------------------------------------");
            //    Console.WriteLine("Invoice Database \n  1.Generate Bill    2.SalesDatabase      3.Get Invoice For Customer");
            //    Console.WriteLine("-------------------------------------------------------------------------------");
            //    int choice = Convert.ToInt32(Console.ReadLine());
            //    switch (choice)
            //    {
            //        case 1:
            //            InvoiceGenerat(invoice);
            //            break;
            //        case 2:
            //            SalesDatabase(invoice);
            //            break;
            //        case 3:
            //            Invoiceforcustomre(invoice);
            //            break;
            //        default:
            //            Console.WriteLine("Invalid Choice");
            //            break;
            //    }
            //}

            ////Show Records
            //else if(ch == 3)
            //{
            //    ShowRecords(conn);
            //}
            //else
            //{
            //    Console.WriteLine("Invalid Input");
            //}

        }


        //invoice database
        static void InvoiceGenerat(GenerateInvoice invoice)
        {
            Console.WriteLine(" Enter Vehicle ID:    eg:'VHE001'");
            string vehicleid = Console.ReadLine();

            Console.WriteLine(" Enter Customre ID:");
            int customerid = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(" Enter Employee ID:");
            int empid = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(" Loan Status \n 1.YES    2.No ");
            int ch = Convert.ToInt32(Console.ReadLine());

            string loan = "";
            if (ch == 1)
            {
                loan = "YES";
            }
            else if(ch == 2)
            {
                loan = "No";
            }
            else
            {
                Console.WriteLine("Invalid Choices");
            }

            Console.WriteLine("Enter Total Amount:");
            float amount = Convert.ToInt64(Console.ReadLine());

            invoice billinvoice = new invoice
            {
                VehicleId = vehicleid,
                CustomerId = customerid,
                EmployeeId = empid,
                IsLoan = loan,
                TotalAmount = amount,
            };
            invoice.BillGenerate(billinvoice);
        }
        static void SalesDatabase(GenerateInvoice invoice)
        {
            invoice.ShowRecordsDatabase();
        }
        static void Invoiceforcustomre(GenerateInvoice invoice)
        {
            Console.WriteLine(" Enter Customre ID:");
            int customerid = Convert.ToInt32(Console.ReadLine());

            invoice custinvoice = new invoice
            {
                CustomerId = customerid,
            };
            invoice.customerinvoice(custinvoice);
        }


        //customre Database
        static void CustomerAdd(CustomerDatabase cDatabase)
        {

            Console.WriteLine("Enter Customer Name");
            string Name = Console.ReadLine();

            Console.WriteLine("Enter Customer Address");
            string Address = Console.ReadLine();

            Console.WriteLine("Enter Customre Contact");
            string Contact = Console.ReadLine();

            customer newcustomer = new customer
            {
                CustomerName = Name,
                Address = Address,
                Contact = Contact,
            };
            cDatabase.AddCustomer(newcustomer);
        }
        static void CustomerUpdate(CustomerDatabase cDatabase)
        {

            Console.WriteLine(" Update Customre Data \n " +
                " 1.Update Name     2.Update Address    3.Update Contact    4. Update All   0.Exit");
            int ch = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Customre Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            if (ch == 1)
            {
                Console.WriteLine("Enter New Name");
                string Name = Console.ReadLine();

                customer update = new customer
                {
                    CustomerName = Name,
                    CustomerId = id,
                    choice = ch,
                };
                cDatabase.Updatecustomer(update);
            }
            else if(ch == 2)
            {
                Console.WriteLine("Enter New Address: ");
                string Address = Console.ReadLine();
                customer update = new customer
                {
                    Address = Address,
                    CustomerId = id,
                    choice = ch,
                };
                cDatabase.Updatecustomer(update);
            }
            else if(ch == 3)
            {
                Console.WriteLine("Enter New Contact: ");
                string Contact = Console.ReadLine();
                customer update = new customer
                {
                    Contact = Contact,
                    CustomerId = id,
                    choice = ch,
                };
                cDatabase.Updatecustomer(update);
            }
            
            else if(ch == 4)
            {
                Console.WriteLine("Enter New Name");
                string Name = Console.ReadLine();

                Console.WriteLine("Enter New Address: ");
                string Address = Console.ReadLine();

                Console.WriteLine("Enter New Contact: ");
                string Contact = Console.ReadLine();

                customer update = new customer
                {
                    CustomerName = Name,
                    Address = Address,
                    Contact = Contact,
                    CustomerId = id,
                    choice = ch,
                };
                cDatabase.Updatecustomer(update);
            }
            else
            {
                Console.WriteLine("Invalid choise");
                return;
            }


            
        }
        static void CustomerShow(CustomerDatabase cDatabase)
        {
            cDatabase.Showcustomer();
        }
        static void CustomreDelete(CustomerDatabase cDatabase)
        {
            Console.WriteLine("Enter Customer Id to Delete Coustomer Data");
            int id = Convert.ToInt32(Console.ReadLine());
            cDatabase.Deletecustomre( id);

        }



        //Vehicle Database
        static void databasevehicle(VehicleDatabase  vehicledb)
        {
             vehicledb.ShowVehicledatabase();
        }

        

        //ShowRecords
        static void ShowRecords(SqlConnection conn)
        {
            string query = "select InvoiceNo,BillDate,r.VehicleId,v.VehicleBrand,v.VehicleType," +
                "v.VehicleModel,r.CustomerId,c.CustomerName,c.CustomerAddress" +
                ",r.IsLoan,r.TotalAmount  from Records r " +
                "inner join Vehicle v on r.VehicleId = v.VehicleId " +
                "inner join Customer c on r.CustomerId = c.Customerid;";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                Console.WriteLine(" InvoiceNo: "+reader[0]+"  " + "BillDate: "+reader[1]+"  "+"VehicleId: "+ 
                    reader[3] +"  "+"Brand: " + reader[4]+"   " +"Model: "+ 
                    reader[5] + " \n "+"CustomerId: " + reader[6] + "  " +"CustomerName: "+ reader[7] + "  " 
                    +"CustomerAddress: "+ reader[8] + "\n "
                    +"Is Loan: "+ reader[9] + "    "+ "Price :Rs."+ reader[10] + "\n");
            }
        }


      
    }


}
    