using RossyStore.Core.Models.Product_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Interfaces.Product_Interface
{
    interface IProduct
    {
        List<Product> Get_AllProducts();
        Product Get_Product(string id);
        bool Add_Product(Product_toPost product);
        bool Update_Product(Product_toUpdate product);
        bool Delete_Product(string id);
    }
}
