using Firebase.Database;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using RossyStore.Core.DataConnection;
using RossyStore.Core.Interfaces.Employee_Interface;
using RossyStore.Core.Models.Employee_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.Employee_Repository
{
    public class EmployeeRepository : IEmployee
    {
        private IFirebaseClient client = new RealtimeFirebaseDB().Client;
        private FirebaseClient firebaseClient = new RealtimeFirebaseDB().FirebaseClient;
        private string RandomID()
        {
            string ranID = "EMP" + new Random().Next(1000000000, 2147483647).ToString();
            var list_IDEmployees = GetIDEmployees();
            if (list_IDEmployees == null) return ranID;
            foreach (var item in list_IDEmployees)
            {
                if (ranID == item)
                    return RandomID();
            }
            return ranID;
        }

        private List<string> GetIDEmployees()
        {
            var get_employees = client.Get("Employees/");
            Dictionary<string, Employee> list_employees = JsonConvert.DeserializeObject<Dictionary<string, Employee>>(get_employees.Body.ToString());
            if (list_employees == null) return null;
            List<string> list = new List<string>();
            foreach (var item in list_employees)
            {
                if (item.Value.id != null)
                {
                    list.Add(item.Value.id);
                }
            }
            return list;
        }
        public void Add_Employee(Employee employee)
        {
            employee.id = RandomID();
            client.Set("Employees/" + employee.id, employee);
        }

        public void Delete_Employee(string employee_id)
        {
            client.Delete("Employees/" + employee_id);
        }

        public List<Employee> Get_AllEmployees()
        {
            var get_all_employees = client.Get("Employees/");
            Dictionary<string, Employee> emloyees = JsonConvert.DeserializeObject<Dictionary<string, Employee>>(get_all_employees.Body.ToString());
            if (emloyees == null) return null;
            var list_employees = new List<Employee>();
            foreach (var item in emloyees)
            {
                list_employees.Add(item.Value);
            }
            return list_employees;
        }

        public Employee Get_Employee(string employee_id)
        {
            var get_employee = client.Get("Employees/" + employee_id);
            Employee employee = get_employee.ResultAs<Employee>();
            return employee;
        }

        public Employee Get_Employee_ByPhone(string phone)
        {
            return firebaseClient.Child("Employees")
                .OnceAsync<Employee>()
                .Result
                .Where(item => item.Object.employee_phoneNumber == phone)
                .Select(item => item.Object)
                .SingleOrDefault();
        }

        public List<Employee> Get_Employee_ByName(string name)
        {
            return firebaseClient.Child("Employees")
                .OnceAsync<Employee>()
                .Result
                .Where(item => item.Object.employee_name.ToLower().Contains(name.ToLower()))
                .Select(item => item.Object)
                .ToList();
        }

        public void Update_Employee(Employee employee)
        {
            client.Update("Employees/" + employee.id, employee);
        }

        public bool isPhoneNumberExist(string phoneNumber)
        {
            return firebaseClient.Child("Employees")
                 .OnceAsync<Employee>()
                 .Result
                 .Any(item => item.Object.employee_phoneNumber == phoneNumber);
        }

        public bool isEmailExist(string email)
        {
            return firebaseClient.Child("Employees")
                 .OnceAsync<Employee>()
                 .Result
                 .Any(item => item.Object.employee_email == email);
        }
    }
}
