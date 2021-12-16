using RossyStore.Core.Models.Customer_Model;
using RossyStore.Core.Models.ShoppingCart_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Models.Order_Model
{
    public class Order
    {
        //Use this model to post data to the database.
        //ID will be automatically generated.
        public string id { get; set; }

        /*public string customer_id { get; set; }

        public string customer_name { get; set; }

        public string customer_phoneNumber { get; set; }

        public string customer_address { get; set; }*/

        public _Customer customer { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime order_date { get; set; }

        [Required]
        public List<_ShoppingCart> list_cart { get; set; }

        public double order_total { get; set; }

    }

    public class _Order
    {
        public _Customer customer { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime order_date { get; set; }

        [Required]
        public List<_ShoppingCart> list_cart { get; set; }
    }
}
