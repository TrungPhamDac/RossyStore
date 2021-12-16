using Firebase.Database;
using RossyStore.Core.DataConnection;
using RossyStore.Core.Interfaces.User_Interface;
using RossyStore.Core.Models.Customer_Model;
using RossyStore.Core.Models.Employee_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.User_Repository
{
    public class UserRepository : IUser
    {
        private FirebaseClient firebaseClient = new RealtimeFirebaseDB().FirebaseClient;
        /*public bool CheckUser(string username, string password)
        {
            return username.Equals("admin") && password.Equals("123");
        }*/

        public bool CheckUser(string email, string password)
        {
            return firebaseClient
                .Child("Customers")
                .OnceAsync<Customer>()
                .Result
                .Any(item => item.Object.customer_email == email && item.Object.customer_password == password);
        }

        public Customer GetCustomer(string email, string password)
        {
            return firebaseClient
               .Child("Customers")
               .OnceAsync<Customer>()
               .Result
               .Where(item => item.Object.customer_email == email && item.Object.customer_password == password)
               .Select(item => item.Object)
               .SingleOrDefault();
        }

        public Employee GetEmployee(string email, string password)
        {
            return firebaseClient
               .Child("Employees")
               .OnceAsync<Employee>()
               .Result
               .Where(item => item.Object.employee_email == email && item.Object.employee_password == password)
               .Select(item => item.Object)
               .SingleOrDefault();
        }
    }
}
