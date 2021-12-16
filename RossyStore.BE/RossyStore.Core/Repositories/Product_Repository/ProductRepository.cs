using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using RossyStore.Core.DataConnection;
using RossyStore.Core.Interfaces.Product_Interface;
using RossyStore.Core.Models.Product_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.Product_Repository
{
    public class ProductRepository : IProduct
    {
        private IFirebaseClient client = new RealtimeFirebaseDB().Client;
        private FirebaseClient firebaseClient = new RealtimeFirebaseDB().FirebaseClient;
        private ProductDetailRepository productDetail_repo = new ProductDetailRepository();
        private ProductImageRepository productImage_repo = new ProductImageRepository();


        private string RandomID()
        {
            string ranID = "PRO" + new Random().Next(1000000000, 2147483647).ToString();
            if (isProductExist(ranID))
            {
                return RandomID();
            }
            return ranID;
        }

        public List<string> GetIDProducts()
        {
            /*var get_products = client.Get("Products/");
            Dictionary<string, Product> list_product = JsonConvert.DeserializeObject<Dictionary<string, Product>>(get_products.Body.ToString());
            if (list_product == null) return null;
            List<string> list = new List<string>();
            foreach (var item in list_product)
            {
                if (item.Value.product_id != null)
                {
                    list.Add(item.Value.product_id);
                }
            }
            return list;*/
            var product = firebaseClient
                .Child("Products")
                .OnceAsync<Product>()
                .Result
                .Select(item => item.Object.id)
                .ToList();
            return product;
        }
        public bool Add_Product(Product_toPost product)
        {
            try
            {
                product.id = RandomID();
                client.Set("Products/" + product.id, product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete_Product(string id)
        {
            try
            {
                client.Delete("Products/" + id);
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }

        public List<Product> Get_AllProducts()
        {
            /*var get_all_products = client.Get("Products/");
            Dictionary<string, Product> products = JsonConvert.DeserializeObject<Dictionary<string, Product>>(get_all_products.Body.ToString());
            if (products == null) return null;
            var list_products = new List<Product>();
            foreach (var item in products)
            {
                Thread t = new Thread(() =>
                {
                    new Thread(() => { item.Value.product_images = productImage_repo.GetAllProductDetailImages(item.Value.product_id); });
                    new Thread(() => { item.Value.product_sizes = productDetail_repo.GetAllSizes(item.Value.product_id); });
                    new Thread(() => { item.Value.product_colors = productDetail_repo.GetAllColors(item.Value.product_id); });
                    new Thread(() => { item.Value.totalProducts = productDetail_repo.TotalProducts(item.Value.product_id); });
                    
                    list_products.Add(item.Value);
                });
                t.Start();
            }
            return list_products;*/
            var products = firebaseClient.Child("Products").OnceAsync<Product>().Result.Select(item => item.Object).ToList();
            return products;
        }

        public Product Get_Product(string product_id)
        {
            var product = firebaseClient
                .Child("Products")
                .OnceAsync<Product>()
                .Result
                .Where(item => item.Object.id == product_id)
                .Select(item => item.Object)
                .SingleOrDefault();
            return product;
        }

        public bool Update_Product(Product_toUpdate product)
        {
            try
            {
                client.Update("Products/" + product.id, product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> Get_AllProducts_ByType(string product_type)
        {
            var product = firebaseClient
                .Child("Products")
                .OnceAsync<Product>()
                .Result
                .Where(item => item.Object.product_type == product_type)
                .Select(item => item.Object)
                .ToList();
            return product;
        }

        public List<Product> Get_AllProducts_ByName(string product_name)
        {
            var product = firebaseClient
                .Child("Products")
                .OnceAsync<Product>()
                .Result
                .Where(item => (item.Object.product_name.ToLower().Contains(product_name.ToLower())))
                .Select(item => item.Object)
                .ToList();
            return product;
        }

        public bool isProductExist(string product_id)
        {
            var product = firebaseClient
                .Child("Products")
                .OnceAsync<Product>()
                .Result
                .Any(item => item.Object.id == product_id);
            return product;
           
        }

        public int getTotalProduct(string product_id)
        {
            return firebaseClient
                .Child("Products")
                .Child(product_id)
                .OnceSingleAsync<Product>()
                .Result.totalProducts;
        }
    }
}
