using RossyStore.Core.Models.Product_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Models.ShoppingCart_Model
{
    //Use this model to post data to the database.
    public class ShoppingCart
    {
        public string id { get; set; }

        [Required]
        public string Product_id { get; set; }

        /*[Required]
        public string Product_detail_id { get; set; }*/

        [Required]
        public string Product_detail_color { get; set; }

        [Required]
        public int Product_detail_size { get; set; }

        [Required]
        public int Quantity { get; set; }

        public ShoppingCart(string product_id, string product_detail_color, int product_detail_size, int quantity)
        {
            this.Product_id = product_id;
            this.Product_detail_color = product_detail_color;
            this.Product_detail_size = product_detail_size;
            this.Quantity = quantity;
        }

        public ShoppingCart()
        {
        }
    }

    public class _ShoppingCart
    {
        [Required]
        public string id { get; set; }

        [Required]
        public string Product_detail_color { get; set; }

        [Required]
        public int Product_detail_size { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

    public class __ShoppingCart
    {
        [Required]
        public string Product_id { get; set; }
        
        [Required]
        public string Product_detail_color { get; set; }

        [Required]
        public int Product_detail_size { get; set; }

    }
}
