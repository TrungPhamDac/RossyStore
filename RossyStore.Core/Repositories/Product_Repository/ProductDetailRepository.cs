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
    public class ProductDetailRepository : IProductDetail
    {
        private IFirebaseClient client = new RealtimeFirebaseDB().Client;
        private FirebaseClient firebaseClient = new RealtimeFirebaseDB().FirebaseClient;


        private string Random_Detail_ID(string product_id)
        {
            string ranID = "DET" + new Random().Next(1000000, 9999999).ToString();
            if(isProductDetailExist(product_id, ranID))
            {
                return Random_Detail_ID(product_id);
            }
            return ranID;
        }

        public List<string> GetIDProductDetails(string product_id)
        {
            var product_detail = firebaseClient.Child("Products")
                                            .Child(product_id)
                                            .Child("Details")
                                            .OrderByKey()
                                            .OnceAsync<Product_Detail>()
                                            .Result
                                            .Select(item => item.Object.id);
            if (product_detail == null)
                return null;
            return product_detail.ToList();

        }

        public Product_Detail Add_ProductDetail(string product_id, Product_Detail productDetail)
        {
            try
            {
                string ID = Random_Detail_ID(product_id);
                productDetail.id = ID;
                var product = firebaseClient
                    .Child("Products")
                    .Child(product_id)
                    .OnceSingleAsync<Product>()
                    .Result;
                Thread thread_addSize = new Thread(() =>
                {
                    if (product.product_sizes != null)
                    {
                        if (!product.product_sizes.Contains(productDetail.product_detail_size))
                        {
                            product.product_sizes.Add(productDetail.product_detail_size);
                            client.Set($"Products/{product_id}/product_sizes", product.product_sizes);
                        }
                    }
                    else
                    {
                        product.product_sizes = new List<int>();
                        product.product_sizes.Add(productDetail.product_detail_size);
                        client.Set($"Products/{product_id}/product_sizes", product.product_sizes);
                    }
                });

                Thread thread_addColor = new Thread(() =>
                {
                    if (product.product_colors != null)
                    {
                        if (!product.product_colors.Contains(productDetail.product_detail_color))
                        {
                            product.product_colors.Add(productDetail.product_detail_color);
                            client.Set($"Products/{product_id}/product_colors", product.product_colors);
                        }
                    }
                    else
                    {
                        product.product_colors = new List<string>();
                        product.product_colors.Add(productDetail.product_detail_color);
                        client.Set($"Products/{product_id}/product_colors", product.product_colors);
                    }
                });

                Thread thread_addDetail = new Thread(() =>
                {
                    client.Set($"Products/{product_id}/Details/" + productDetail.id, productDetail);
                });

                Thread thread_addTotal = new Thread(() =>
                {
                    client.Set($"Products/{product_id}/totalProducts", product.totalProducts + productDetail.product_detail_quantity);
                });

                /*thread_addColor.IsBackground = true;
                thread_addDetail.IsBackground = true;
                thread_addSize.IsBackground = true;
                thread_addTotal.IsBackground = true;*/
                thread_addColor.Start();
                thread_addDetail.Start();
                thread_addSize.Start();
                thread_addTotal.Start();

                return productDetail;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public void Delete_ProductDetail(string product_id, string detail_id)
        {
            var productDetail = this.Get_ProductDetail(product_id, detail_id);
            /*var productDetailColor = getProductDetailColor(product_id, detail_id);
            var productDetailSize = getProductDetailSize(product_id, detail_id);*/
            Thread t_delete = new Thread(() =>
            {
                client.Delete($"Products/{product_id}/Details/" + detail_id);
            });
            ProductRepository product_repo = new ProductRepository();
            Thread t_color = new Thread(() =>
            {
                if (!this.isColorExist(product_id, productDetail.product_detail_color))
                {

                    var list_color = product_repo.Get_Product(product_id).product_colors;
                    list_color.Remove(productDetail.product_detail_color);
                    client.Set($"Products/{product_id}/product_colors", list_color);
                }
            });
            Thread t_size = new Thread(() =>
            {
                if (!this.isSizeExist(product_id, productDetail.product_detail_size))
                {
                    var list_size = product_repo.Get_Product(product_id).product_sizes;
                    list_size.Remove(productDetail.product_detail_size);
                    client.Set($"Products/{product_id}/product_sizes", list_size);
                }
            });
            Thread t_total = new Thread(() =>
            {
                var totalProduct = product_repo.getTotalProduct(product_id);
                client.Set($"Products/{product_id}/totalProducts", totalProduct - productDetail.product_detail_quantity);
            });
            /*t_delete.IsBackground = true;
            t_size.IsBackground = true;
            t_total.IsBackground = true;
            t_color.IsBackground = true;*/
            t_color.Start();
            t_size.Start();
            t_delete.Start();
            t_total.Start();
        }

        public void Delete_ProductDetail(string product_id, string color, int size)
        {
            var productDetail = this.Get_ProductDeTail(product_id, color, size);
            Thread t_delete = new Thread(() =>
            {
                client.Delete($"Products/{product_id}/Details/" + productDetail.id);
            });
            ProductRepository product_repo = new ProductRepository();
            Thread t_color = new Thread(() =>
            {
                if (!this.isColorExist(product_id, productDetail.product_detail_color))
                {

                    var list_color = product_repo.Get_Product(product_id).product_colors;
                    list_color.Remove(productDetail.product_detail_color);
                    client.Set($"Products/{product_id}/product_colors", list_color);
                }
            });
            Thread t_size = new Thread(() =>
            {
                if (!this.isSizeExist(product_id, productDetail.product_detail_size))
                {
                    var list_size = product_repo.Get_Product(product_id).product_sizes;
                    list_size.Remove(productDetail.product_detail_size);
                    client.Set($"Products/{product_id}/product_sizes", list_size);
                }
            });
            Thread t_total = new Thread(() =>
            {
                var totalProduct = product_repo.getTotalProduct(product_id);
                client.Set($"Products/{product_id}/totalProducts", totalProduct - productDetail.product_detail_quantity);
            });
            /*t_delete.IsBackground = true;
            t_size.IsBackground = true;
            t_total.IsBackground = true;
            t_color.IsBackground = true;*/
            t_color.Start();
            t_size.Start();
            t_delete.Start();
            t_total.Start();
        }

        public List<Product_Detail> Get_AllProductDetails(string product_id)
        {
            var get_all_product_details = client.Get($"Products/{product_id}/Details/");
            Dictionary<string, Product_Detail> product_details = JsonConvert.DeserializeObject<Dictionary<string, Product_Detail>>(get_all_product_details.Body.ToString());
            if (product_details == null) return null;
            var list_product_detail = new List<Product_Detail>();
            foreach (var item in product_details)
            {
                list_product_detail.Add(item.Value);
            }
            return list_product_detail;
        }

        public string getProductDetailColor(string product_id, string detail_id)
        {
            var color = firebaseClient
                .Child("Products")
                .Child(product_id)
                .Child("Details")
                .Child(detail_id)
                .OnceSingleAsync<Product_Detail>().Result.product_detail_color;
            if (color == null)
                return null;
            return color;
        }

        public int getProductDetailSize(string product_id, string detail_id)
        {
            var size = firebaseClient
                .Child("Products")
                .Child(product_id)
                .Child("Details")
                .Child(detail_id)
                .OnceSingleAsync<Product_Detail>().Result.product_detail_size;
            return size;
        }

        public Product_Detail Get_ProductDetail(string product_id, string detail_id)
        {
            var get_product_detail = client.Get($"Products/{product_id}/Details/" + detail_id);
            Product_Detail product_detail = get_product_detail.ResultAs<Product_Detail>();
            return product_detail;
        }

        public Product_Detail Get_ProductDeTail(string product_id, string color, int size)
        {
            var product_detail = firebaseClient
                .Child("Products")
                .Child(product_id)
                .Child("Details")
                .OnceAsync<Product_Detail>()
                .Result
                .Where(item => item.Object.product_detail_color == color && item.Object.product_detail_size == size)
                .Select(temp => temp.Object).SingleOrDefault();
            if (product_detail == null)
                return null;
            return product_detail;
        }

        /*public int Get_ProductDetail_Quantity(string product_id, string detail_id)
        {
            var get_product_detail = client.Get($"Products/{product_id}/Details/" + detail_id);
            Product_Detail product_detail = get_product_detail.ResultAs<Product_Detail>();
            return product_detail.product_detail_quantity;
        }*/

        public bool Update_ProductDetail(string product_id, Product_Detail productDetail)
        {
            try
            {
                client.Update($"Products/{product_id}/Details/" + productDetail.id, productDetail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update_ProductDetail_Quantity(string product_id, string productDetail_id, int quantity)
        {
            try
            {
                client.Set($"Products/{product_id}/Details/{productDetail_id}/product_detail_quantity", quantity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update_ProductDetail_Quantity(string product_id, string color, int size, int quantity)
        {
            var productDetail_id = this.GetIDProductDetail(product_id, color, size);
            int totalProduct = 0;
            int productDetailQuantity = 0;

            Thread get1 = new Thread(() =>
            {
                totalProduct = new ProductRepository().getTotalProduct(product_id);
            });
            Thread get2 = new Thread(() =>
            {
                productDetailQuantity = this.GetProductDetailQuantity(product_id, productDetail_id);
            });

            get1.Start();
            get2.Start();
            get1.Join();
            get2.Join();

            Thread t1 = new Thread(() =>
            {
                client.Set($"Products/{product_id}/Details/{productDetail_id}/product_detail_quantity", quantity);
            });

            Thread t2 = new Thread(() =>
            {
                client.Set($"Products/{ product_id}/totalProducts", totalProduct - productDetailQuantity + quantity);
            });

            t1.Start();
            t2.Start();
        }

        public List<int> GetAllSizes(string product_id)
        {
            var product_detail = firebaseClient.Child("Products")
                                            .Child(product_id)
                                            .Child("Details")
                                            .OnceAsync<Product_Detail>()
                                            .Result
                .Select(temp => temp.Object.product_detail_size);
            if (product_detail == null)
                return null;

            return product_detail.ToList<int>();
        }

        public List<string> GetAllColors(string product_id)
        {
            var product_detail = firebaseClient.Child("Products")
                                            .Child(product_id)
                                            .Child("Details")
                                            .OnceAsync<Product_Detail>()
                                            .Result
                .Select(temp => temp.Object.product_detail_color);
            if (product_detail == null)
                return null;
            return product_detail.ToList<string>();
        }

        public string GetIDProductDetail(string product_id, string color, int size)
        {
            var product_detail = firebaseClient
                .Child("Products")
                .Child(product_id)
                .Child("Details")
                .OnceAsync<Product_Detail>()
                .Result
                .Where(item => item.Object.product_detail_color == color && item.Object.product_detail_size == size)
                .Select(temp => temp.Object).FirstOrDefault();
            if (product_detail == null)
                return null;
            return product_detail.id;
        }

        public int GetProductDetailQuantity(string product_id, string detail_id)
        {
            var product_detail = firebaseClient.Child("Products")
                                .Child(product_id)
                                .Child("Details")
                                .Child(detail_id)
                                .OnceSingleAsync<Product_Detail>()
                                .Result;
            if (product_detail == null)
                return 0;
            return product_detail.product_detail_quantity;
        }

        public int GetProductDetailQuantity(string product_id, string color, int size)
        {
            var product_detail = firebaseClient.Child("Products")
                                .Child(product_id)
                                .Child("Details")
                                .OnceAsync<Product_Detail>()
                                .Result
                                .Where(item => item.Object.product_detail_color == color && item.Object.product_detail_size == size)
                                .Select(item => item.Object.product_detail_quantity)
                                .SingleOrDefault();
            if (product_detail == null)
                return 0;
            return product_detail;
        }

        public int TotalProducts(string product_id)
        {
            var product_detail = firebaseClient.Child("Products")
                                            .Child(product_id)
                                            .Child("Details")
                                            .OnceAsync<Product_Detail>()
                                            .Result
                .Select(temp => temp.Object.product_detail_quantity);
            if (product_detail == null)
                return 0;
            return product_detail.Sum();
        }

        public bool isColorExist(string product_id, string color)
        {
            var product_detail = firebaseClient.Child("Products")
                                            .Child(product_id)
                                            .Child("Details")
                                            .OnceAsync<Product_Detail>()
                                            .Result
                                            .Any(item => item.Object.product_detail_color == color);
            return product_detail;
        }

        public bool isSizeExist(string product_id, int size)
        {
            var product_detail = firebaseClient.Child("Products")
                                            .Child(product_id)
                                            .Child("Details")
                                            .OnceAsync<Product_Detail>()
                                            .Result
                                            .Any(item => item.Object.product_detail_size == size);
            return product_detail;
        }

        public bool isProductDetailExist(string product_id, string product_detail_id)
        {
            var product_detail = firebaseClient
                .Child("Products")
                .Child(product_id)
                .Child("Details")
                .OnceAsync<Product_Detail>()
                .Result
                .Any(item => item.Object.id == product_detail_id);
            return product_detail;
        }

        public bool isProductDetailExist(string product_id, string color, int size)
        {
            var product_detail = firebaseClient.Child("Products")
                                    .Child(product_id)
                                    .Child("Details")
                                    .OnceAsync<Product_Detail>()
                                    .Result
                                    .Any(item => item.Object.product_detail_color == color && item.Object.product_detail_size == size);
            return product_detail;
        }

        public bool isColorAndSizeExist(string product_id, string color, int size)
        {
            var product_detail = firebaseClient.Child("Products")
                .Child(product_id)
                .Child("Details")
                .OnceAsync<Product_Detail>()
                .Result
                .Any(item => item.Object.product_detail_color == color && item.Object.product_detail_size == size);
            return product_detail;
        }


    }
}
