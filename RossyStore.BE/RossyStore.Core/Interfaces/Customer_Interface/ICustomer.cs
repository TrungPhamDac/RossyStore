using RossyStore.Core.Models.Customer_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Interfaces.Customer_Interface
{
    interface ICustomer
    {
        List<Customer> Get_AllCustomers();
        Customer Get_Customer(string customer_id);
        void Add_Customer(Customer customer);
        void Update_Customer(Customer customer);
        void Delete_Customer(string customer_id);
    }
}
