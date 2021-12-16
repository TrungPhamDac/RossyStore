
using RossyStore.Core.Models.Product_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Models.Customer_Model
{
    public class Customer
    {
        //ID will be automatically generated.
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string customer_name { get; set; }

        [Required]
        [Phone]
        [StringLength(maximumLength: 11, MinimumLength = 10)]
        public string customer_phoneNumber { get; set; }

        [Required]
        public string customer_address { get; set; }

        [Required]
        [EmailAddress]
        public string customer_email { get; set; }

        public string customer_password { get; set; }

    }

    public class _Customer
    {
        public string id { get; set; }

        public string customer_name { get; set; }

        public string customer_email { get; set; }

        public string customer_phoneNumber { get; set; }

        public string customer_address { get; set; }

    }
}
