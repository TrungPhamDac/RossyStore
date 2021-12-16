using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Models.Product_Model
{
    public class Product_Detail
    {
        //ID will be automatically generated.
        public string id { get; set; }

        [Required]
        public string product_detail_color { get; set; }

        [Required]
        public int product_detail_size { get; set; }

        [Required]
        public int product_detail_quantity { get; set; }
    }

    public class _Product_detail
    {
        public string product_detail_id { get; set; }

        public int quantity { get; set; }

    }
}
