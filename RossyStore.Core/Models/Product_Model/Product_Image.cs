using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Models.Product_Model
{
    public class Product_Image
    {
        //public string Image_Name { get; set; }
        public IFormFile Image { get; set; }
    }

    public class List_Product_Image
    {
        public List<IFormFile> ListImageDetail { get; set; }

    }
}
