using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using RossyStore.Core.DataConnection;
using RossyStore.Core.Interfaces.Product_Interface;
using RossyStore.Core.Models.Product_Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.Product_Repository
{
    public class ProductImageRepository : IProductImage
    {
        private IFirebaseClient client = new RealtimeFirebaseDB().Client;
        private StorageFirebase storate = new StorageFirebase();
        private FirebaseClient firebaseClient = new RealtimeFirebaseDB().FirebaseClient;


        public void AddImageProduct(string Product_ID, string imageURL)
        {
            client.Set($"Products/{Product_ID}/imageURL", imageURL);
        }

        public void AddImages(string Product_ID, string Filename, string imageURL)
        {
            client.Set($"Products/{Product_ID}/Images/{Filename}", imageURL);
        }

        public void AddImages(string Product_ID, List<string> imageURL)
        {
            var product_image = firebaseClient
                .Child("Products")
                .Child(Product_ID)
                .OnceSingleAsync<Product>()
                .Result.product_images;
            if(product_image == null)
            {
                client.Set($"Products/{Product_ID}/product_images", imageURL);
            }
            else
            {
                foreach (var item in imageURL)
                {
                    product_image.Add(item);
                }
                client.Set($"Products/{Product_ID}/product_images", product_image);   
            }
        }

        public string GetImage(string Product_ID, string FileName)
        {
            var getImageURL = client.Get($"Products/{Product_ID}/Images/" + FileName);
            string URL = getImageURL.Body.ToString();
            return URL;
        }

        public List<string> GetAllProductDetailImages(string product_id)
        {
            /*var getImages = client.Get($"Products/{product_id}/Images/");
            Dictionary<string, string> images = JsonConvert.DeserializeObject<Dictionary<string, string>>(getImages.Body.ToString());
            if (images == null) return null;
            var list_images = new List<string>();
            foreach (var item in images)
            {
                list_images.Add(item.Value);
            }
            return list_images;*/
            var images = firebaseClient.Child("Products").Child(product_id).Child("Images")
                .OnceAsync<string>().Result.Select(item => item.Object);
            if (images == null)
                return null;
            /*List<string> list = new List<string>();
            foreach (var item in images)
            {
                list.Add(item.Object);
            }
            return list;*/
            return images.ToList<string>();
        }

        public bool DeleteImage(string Product_ID, string FileName)
        {
            try
            {
                client.Delete($"Products/{Product_ID}/Images/" + FileName);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void UpadteImage(string Product_ID, string FileName, string imageURL)
        {
            var upadteImageURL = client.Set($"Products/{Product_ID}/Images/{FileName}", imageURL);
        }

        public async Task<string> UploadToStorage(string Product_ID, FileStream stream, string Filename)
        {
            
            var auth = new FirebaseAuthProvider(new FirebaseConfig(storate.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(storate.AuthEmail, storate.AuthPassword);

            var cancellation = new CancellationTokenSource();
            try
            {
                var task = new FirebaseStorage(
                storate.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("images")
                .Child(Filename)
                .PutAsync(stream, cancellation.Token);

                string imageURL = await task;
                //client.SetAsync($"Products/{Product_ID}/Images/" + Filename, Filename);
                return imageURL;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void SaveImage(string imageUrl, string path)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(imageUrl);
                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        // If you want it as Png
                        //yourImage.Save("path_to_your_file.png", ImageFormat.Png);

                        // If you want it as Jpeg
                        yourImage.Save(path, ImageFormat.Jpeg);
                    }
                }

            }
        }
    }
}
