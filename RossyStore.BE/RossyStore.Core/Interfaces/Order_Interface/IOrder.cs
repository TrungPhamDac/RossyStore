using RossyStore.Core.Models.Order_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Interfaces.Order_Interface
{
    interface IOrder
    {
        List<Order> Get_AllOrders();
        Order Get_Order(string order_id);
        //Order Add_Order(string customer_id);
        bool Delete_Order(string order_id);
    }
}
