using RossyStore.Core.Models.Product_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Interfaces.Product_Interface
{
    interface IProductDetail
    {
        List<Product_Detail> Get_AllProductDetails(string product_id);
        Product_Detail Get_ProductDetail(string product_id, string detail_id);
        Product_Detail Add_ProductDetail(string product_id, Product_Detail productDetail);
        bool Update_ProductDetail(string product_id, Product_Detail productDetail);
        void Delete_ProductDetail(string product_id, string detail_id);
    }
}
