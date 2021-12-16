using RossyStore.Core.Models.Employee_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Interfaces.Employee_Interface
{
    interface IEmployee
    {
        List<Employee> Get_AllEmployees();
        Employee Get_Employee(string employee_id);
        void Add_Employee(Employee employee);
        void Update_Employee(Employee employee);
        void Delete_Employee(string employee_id);
    }
}
