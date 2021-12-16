using Firebase.Database;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using RossyStore.Core.DataConnection;
using RossyStore.Core.Interfaces.Customer_Interface;
using RossyStore.Core.Models.Customer_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.Customer_Repository
{
    public class CustomerRepository : ICustomer
    {
        private IFirebaseClient client = new RealtimeFirebaseDB().Client;
        private FirebaseClient firebaseClient = new RealtimeFirebaseDB().FirebaseClient;
        private string RandomID()
        {
            string ranID = "CUS" + new Random().Next(1000000000, 2147483647).ToString();
            if (this.isCustomerExist(ranID))
            {
                return RandomID();
            }
            return ranID;
        }


        private List<string> GetIDCustomers()
        {
            var get_customers = client.Get("Customers/");
            Dictionary<string, Customer> customers = JsonConvert.DeserializeObject<Dictionary<string, Customer>>(get_customers.Body.ToString());
            if (customers == null) return null;
            List<string> list = new List<string>();
            foreach (var item in customers)
            {
                if (item.Value.id != null)
                {
                    list.Add(item.Value.id);
                }
            }
            return list;
        }
        public void Add_Customer(Customer customer)
        {
            customer.id = RandomID();
            client.Set("Customers/" + customer.id, customer);
        }

        public void Delete_Customer(string customer_id)
        {
            client.Delete("Customers/" + customer_id);
        }

        public List<Customer> Get_AllCustomers()
        {
            var get_all_customers = client.Get("Customers/");
            Dictionary<string, Customer> customers = JsonConvert.DeserializeObject<Dictionary<string, Customer>>(get_all_customers.Body.ToString());
            if (customers == null) return null;
            var list_customers = new List<Customer>();
            foreach (var item in customers)
            {
                list_customers.Add(item.Value);
            }
            return list_customers;
        }

        public Customer Get_Customer(string customer_id)
        {
            var get_customer = client.Get("Customers/" + customer_id);
            Customer customer = get_customer.ResultAs<Customer>();
            return customer;
        }

        public List<Customer> Get_Customer_byName(string customer_name)
        {
            return firebaseClient
                .Child("Customers")
                .OnceAsync<Customer>()
                .Result
                .Where(item => item.Object.customer_name.ToLower().Contains(customer_name.ToLower()))
                .Select(item => item.Object)
                .ToList();
        }

        public Customer Get_Customer_byPhone(string customer_phone)
        {
            return firebaseClient
                .Child("Customers")
                .OnceAsync<Customer>()
                .Result
                .Where(item => item.Object.customer_phoneNumber == customer_phone)
                .Select(item => item.Object)
                .SingleOrDefault();
        }

        public void Update_Customer(Customer customer)
        {
            client.Update("Customers/" + customer.id, customer);
        }

        public bool isCustomerExist(string customer_id)
        {
            /*var task = Task<Customer>.Run(async () =>
            {
                return await firebaseClient.Child("Customers").OnceAsync<Customer>().ConfigureAwait(false);
            });
            var customer = task.Result.Where(item => item.Object.customer_id == customer_id).Select(temp => temp.Object).FirstOrDefault();
            if (customer == null)
                return false;
            return true;*/
            /*var customer = firebaseClient.Child("Customers")
                .OnceAsync<Customer>()
                .Result.Where(item => item.Object.customer_id == customer_id)
                .Select(temp => temp.Object).FirstOrDefault();
            if (customer == null)
                return false;
            return true;*/
            var customer = firebaseClient.Child("Customers")
                .OnceAsync<Customer>()
                .Result
                .Any(item => item.Object.id == customer_id);
            return customer;
        }

        public bool isPhoneNumberExist(string phone)
        {
            return firebaseClient
                .Child("Customers")
                .OnceAsync<Customer>()
                .Result
                .Any(item => item.Object.customer_phoneNumber == phone);
        }

        public bool isEmailExist(string email)
        {
            return firebaseClient
                .Child("Customers")
                .OnceAsync<Customer>()
                .Result
                .Any(item => item.Object.customer_email == email);
        }
    }
}
