using Firebase.Database;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using RossyStore.Core.DataConnection;
using RossyStore.Core.Interfaces.Order_Interface;
using RossyStore.Core.Models.Customer_Model;
using RossyStore.Core.Models.Order_Model;
using RossyStore.Core.Models.ShoppingCart_Model;
using RossyStore.Core.Repositories.Customer_Repository;
using RossyStore.Core.Repositories.Customer_Repository.ShoppingCart_Repository;
using RossyStore.Core.Repositories.Product_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.Order_Repository
{
    public class OrderRepository : IOrder
    {
        private IFirebaseClient client = new RealtimeFirebaseDB().Client;
        private ShoppingCartRepository shoppingCart_repo = new ShoppingCartRepository();
        private CustomerRepository customer_repo = new CustomerRepository();
        private ProductDetailRepository productDetail_repo = new ProductDetailRepository();
        private FirebaseClient firebaseClient = new RealtimeFirebaseDB().FirebaseClient;

        private string Random_Order_ID(string customer_id)
        {
            string ranID = "ORDER" + new Random().Next(1000000, 9999999).ToString();
            if (this.isOrderExist(ranID))
            {
                return Random_Order_ID(customer_id);
            }
            return ranID;
        }

        public bool isOrderExist(string order_id)
        {
            return firebaseClient.Child("Orders").OnceAsync<Order>().Result.Any(item => item.Object.id == order_id);
        }

        public Order Add_Order(_Customer customer, List<_ShoppingCart> listCart)
        {
            Order order = new Order();
            order.customer = customer;
            order.id = Random_Order_ID(customer.id);
            order.order_date = DateTime.Now;
            order.list_cart = listCart;
            order.order_total = 0;
            foreach (var item in listCart)
            {
                var price = new ProductRepository().Get_Product(item.id).product_price;
                order.order_total += price;
            }
            Thread thread2 = new Thread(() =>
            {
                foreach (var item in listCart)
                {
                    Thread t = new Thread(() =>
                    {
                        int ProductDetail_Quantity_InStock = productDetail_repo.GetProductDetailQuantity(item.id, item.Product_detail_color, item.Product_detail_size);
                        new Thread(() => { productDetail_repo.Update_ProductDetail_Quantity(item.id, item.Product_detail_color, item.Product_detail_size, (ProductDetail_Quantity_InStock - item.Quantity)); }).Start();
                        new Thread(() => { shoppingCart_repo.Delete_ProductDetail(customer.id, item.id, item.Product_detail_color, item.Product_detail_size); }).Start();
                    });
                    t.Start();
                    t.Join();
                }
            });
            thread2.Start();
            thread2.Join();

            Thread thread3 = new Thread(() => { client.Set($"Orders/" + order.id, order); });
            thread3.Start();
            //client.Set($"Orders/" + order.id, order);
            return order;
        }

        public Order Add_Order2(_Customer customer, List<_ShoppingCart> listCart)
        {
            Order order = new Order();
            order.customer = customer;
            order.id = Random_Order_ID(customer.id);
            order.order_date = DateTime.Now;
            order.list_cart = listCart;
            order.order_total = 0;
            foreach (var item in listCart)
            {
                var price = new ProductRepository().Get_Product(item.id).product_price;
                order.order_total += price;
            }
            Thread thread2 = new Thread(() =>
            {
                foreach (var item in listCart)
                {
                    Thread t = new Thread(() =>
                    {
                        int ProductDetail_Quantity_InStock = productDetail_repo.GetProductDetailQuantity(item.id, item.Product_detail_color, item.Product_detail_size);
                        new Thread(() => { productDetail_repo.Update_ProductDetail_Quantity(item.id, item.Product_detail_color, item.Product_detail_size, (ProductDetail_Quantity_InStock - item.Quantity)); }).Start();
                        //new Thread(() => { shoppingCart_repo.Delete_ProductDetail(customer.id, item.id, item.Product_detail_color, item.Product_detail_size); }).Start();
                    });
                    t.Start();
                    t.Join();
                }
            });
            thread2.Start();
            thread2.Join();

            Thread thread3 = new Thread(() => { client.Set($"Orders/" + order.id, order); });
            thread3.Start();
            thread3.Join();
            //client.Set($"Orders/" + order.id, order);
            return order;
        }
        public Order Get_Order(string order_id)
        {
            var Get = client.Get($"Orders/{order_id}/");
            Order order = Get.ResultAs<Order>();
            return order;
        }

        public List<Order> Get_AllOrders()
        {
            return firebaseClient
                .Child("Orders")
                .OnceAsync<Order>()
                .Result
                .Select(item => item.Object)
                .ToList();
        }


        public List<Order> Get_InfoOrder_byName(string customer_name)
        {
            var listOrder = firebaseClient
                .Child("Orders")
                .OnceAsync<Order>()
                .Result
                .Where(item => item.Object.customer.customer_name.ToLower().Contains(customer_name.ToLower()))
                .Select(item => item.Object)
                .ToList();
            return listOrder;
        }

        public List<Order> Get_InfoOrder_byphone(string customer_phone)
        {
            var listOrder = firebaseClient
                .Child("Orders")
                .OnceAsync<Order>()
                .Result
                .Where(item => item.Object.customer.customer_phoneNumber == customer_phone)
                .Select(item => item.Object)
                .ToList();
            return listOrder;
        }
        public bool Delete_Order(string id)
        {
            try
            {
                client.Delete($"Orders/" + id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
