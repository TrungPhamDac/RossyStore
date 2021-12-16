using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Models.Product_Model
{
    public class Product
    {
        //ID will be automatically generated.
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string product_name { get; set; }

        [Required]
        public double product_price { get; set; }

        [Required]
        public string product_type { get; set; }

        public string imageURL { get; set; }

        public List<string> product_images { get; set; }
        public List<string> product_colors { get; set; }

        public List<int> product_sizes { get; set; }

        public int totalProducts { get; set; }

        //public List<Product_Detail> product_details { get; set; }
    }

    public class Product_toPost
    {
        //ID will be automatically generated.
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string product_name { get; set; }

        [Required]
        public double product_price { get; set; }

        [Required]
        public string product_type { get; set; }

        public string imageURL { get; set; }

        public List<string> product_images { get; set; }
    }

    public class Product_toUpdate
    {
        public string id { get; set; }
        public string product_name { get; set; }
        public string product_type { get; set; }
        public double product_price { get; set; }
    }
}
