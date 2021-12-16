using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using RossyStore.Core.DataConnection;
using RossyStore.Core.Interfaces.Customer_Interface.IShoppingCart;
using RossyStore.Core.Models.Product_Model;
using RossyStore.Core.Models.ShoppingCart_Model;
using RossyStore.Core.Repositories.Product_Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.Customer_Repository.ShoppingCart_Repository
{
    public class ShoppingCartRepository : IShoppingCart
    {
        private IFirebaseClient client = new RealtimeFirebaseDB().Client;
        private ProductRepository product_repo = new ProductRepository();
        private ProductDetailRepository product_detail_repo = new ProductDetailRepository();
        private FirebaseClient firebaseClient = new RealtimeFirebaseDB().FirebaseClient;

        public bool Add_Product_to_Cart(string customer_id, string product_id, string product_detail_color, int product_detail_size, int quantity)
        {
            try
            {
                ShoppingCart info_item = new ShoppingCart(product_id, product_detail_color, product_detail_size, quantity);
                var response = client.Push($"Customers/{customer_id}/ShoppingCart/", info_item);
                info_item.id = response.Result.name;
                client.Set($"Customers/{customer_id}/ShoppingCart/" + info_item.id, info_item);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool Delete_ProductDetail(string customer_id, string product_id, string color, int size)
        {
            try
            {
                var key = GetKey(customer_id, product_id, color, size);
                if (key == null) return false;
                client.Delete($"Customers/{customer_id}/ShoppingCart/" + key);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete_ProductDetail(string customer_id, string cart_id)
        {
            try
            {
                client.Delete($"Customers/{customer_id}/ShoppingCart/" + cart_id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update_ProductDetail(string customer_id, string product_id, string color, int size, int quantity)
        {
            try
            {
                var key = GetKey(customer_id, product_id, color, size);
                if (key == null) return false;
                client.Set($"Customers/{customer_id}/ShoppingCart/{key}/Quantity", quantity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        public string GetKey(string customer_id, string product_id, string color, int size)
        {
            var key = firebaseClient.Child("Customers").Child(customer_id).Child("ShoppingCart")
                .OnceAsync<ShoppingCart>()
                .Result.Where(item => item.Object.Product_id == product_id 
                            && item.Object.Product_detail_color == color
                            && item.Object.Product_detail_size == size)
                .Select(temp => temp.Key).FirstOrDefault();
            if (key == null)
                return null;
            return key;

        }

        public ShoppingCart Get_ProductAndDetailInCart(string customer_id, string cart_id)
        {
            var Get = client.Get($"Customers/{customer_id}/ShoppingCart/{cart_id}/");
            ShoppingCart cart_info = Get.ResultAs<ShoppingCart>();
            if (cart_info == null) return null;
            return cart_info;
        }

        public List<ShoppingCart> GetAll_IDProductAndDetailInCart(string customer_id)
        {
            var Get = client.Get($"Customers/{customer_id}/ShoppingCart/");
            if (Get == null)
                return null;
            Dictionary<string, ShoppingCart> product_details = JsonConvert.DeserializeObject<Dictionary<string, ShoppingCart>>(Get.Body.ToString());
            if (product_details == null) return null;
            var List_result = new List<ShoppingCart>();
            foreach (var item in product_details)
            {
                List_result.Add(item.Value);
            }
            return List_result;
        }

        public List<ShoppingCart> GetAll_ShoppingCart(string customer_id)
        {
            return firebaseClient
                .Child("Customers")
                .Child(customer_id)
                .Child("ShoppingCart")
                .OnceAsync<ShoppingCart>()
                .Result
                .Select(item => item.Object)
                .ToList();
        }

        public bool isProductExistInCart(string customer_id, string product_id, string product_detail_color, int product_detail_size)
        {
            var product = firebaseClient
                .Child("Customers")
                .Child(customer_id)
                .Child("ShoppingCart")
                .OnceAsync<ShoppingCart>()
                .Result
                .Where(item => item.Object.Product_id == product_id 
                    && item.Object.Product_detail_color == product_detail_color
                    && item.Object.Product_detail_size == product_detail_size)
                .Select(temp => temp.Object).FirstOrDefault();
            if (product == null)
                return false;
            return true;
        }

        public bool isProductExistInCart(string customer_id, string cart_id)
        {
            return firebaseClient
                .Child("Customers")
                .Child(customer_id)
                .Child("ShoppingCart")
                .OnceAsync<ShoppingCart>()
                .Result
                .Any(item => item.Key == cart_id);    
        }
    }
}
