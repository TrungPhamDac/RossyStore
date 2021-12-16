using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RossyStore.Core.Repositories.Product_Repository;
using RossyStore.Core.Models.Product_Model;
using System.Threading;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace RossyStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductRepository products_repo = new ProductRepository();
        private ProductImageRepository image_repo = new ProductImageRepository();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //[Authorize(Roles = "admin, employee")]
        [HttpPost("Post1")]
        public async Task<ActionResult<Product>> CreateProduct1([FromForm] Product_toPost product, [FromForm] Product_Image Image, [FromForm] List_Product_Image listImage)
        {
            ActionResult temp = null;
            Thread thread = new Thread(() =>
            {
                var add = products_repo.Add_Product(product);
                if (!add)
                    temp = BadRequest("Add failed.");
            });
            thread.Start();
            thread.Join();
            if (temp != null)
                return temp;

            Thread thread1 = new Thread(async () =>
            {
                if (Image.Image != null)
                {
                    try
                    {
                        var image = Image.Image;
                        FileStream stream = null;
                        if (image.Length > 0)
                        {
                            string path = Path.Combine(_webHostEnvironment.WebRootPath, $"images_avatar\\");
                            if (Directory.Exists(path))
                            {
                                if (!Directory.GetFiles(path).Any(item => item.Contains(image.FileName)))
                                {
                                    using (stream = new FileStream(Path.Combine(path, image.FileName), FileMode.Create))
                                    {
                                        await image.CopyToAsync(stream);
                                    }
                                }
                                stream = new FileStream(Path.Combine(path, image.FileName), FileMode.Open);
                                try
                                {
                                    string URL = await image_repo.UploadToStorage(product.id, stream, image.FileName);
                                    image_repo.AddImageProduct(product.id, URL);
                                    //return Ok("Added successfully.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                stream.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //return BadRequest("Add failed.");
                    }
                }
            });
            

            /*Thread thread2 = new Thread(async () =>
            {*/
                if (listImage.ListImageDetail != null)
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, $"images\\");
                    if (Directory.Exists(path))
                    {
                        List<string> List_imageURL = new List<string>();

                        foreach (var item in listImage.ListImageDetail)
                        {
                            var image = item;
                            if (image.Length > 0)
                            {
                                if (!Directory.GetFiles(path).Any(item => item.Contains(image.FileName)))
                                {
                                    using var stream1 = new FileStream(Path.Combine(path, image.FileName), FileMode.Create);
                                    image.CopyTo(stream1);
                                    stream1.Close();
                                }
                                using var stream2 = new FileStream(Path.Combine(path, image.FileName), FileMode.Open);
                                string URL = await image_repo.UploadToStorage(product.id, stream2, image.FileName);
                                List_imageURL.Add(URL);
                                stream2.Close();
                            }
                        }

                        image_repo.AddImages(product.id, List_imageURL);
                    }
                }
            /*});
            thread2.Start();
            thread2.Join();*/
            thread1.Start();
            thread1.Join();

            return StatusCode(201, product);
        }

        [HttpPost("Post2")]
        public async Task<ActionResult<Product>> CreateProduct2([FromForm] Product_toPost product, [FromForm] string ImageURL, [FromForm] List<string> listImageURL)
        {
            ActionResult temp = null;
            Thread thread = new Thread(() =>
            {
                var add = products_repo.Add_Product(product);
                if (!add)
                    temp = BadRequest("Add failed.");
            });
            thread.Start();
            thread.Join();
            if (temp != null)
                return temp;

            Thread thread1 = new Thread(async () =>
            {
                if (ImageURL != null)
                {
                    try
                    {
                        var fileName = product.product_name + " avatar";
                        FileStream stream = null;
                        string path = Path.Combine(_webHostEnvironment.WebRootPath, $"images_avatar\\");
                        if (Directory.Exists(path))
                        {
                            if (!Directory.GetFiles(path).Any(item => item.Contains(fileName)))
                            {
                                image_repo.SaveImage(ImageURL, Path.Combine(path, fileName + ".jpg"));
                            }
                            stream = new FileStream(Path.Combine(path, fileName), FileMode.Open);
                            try
                            {
                                string URL = await image_repo.UploadToStorage(product.id, stream, fileName);
                                image_repo.AddImageProduct(product.id, URL);
                                //return Ok("Added successfully.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            stream.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //return BadRequest("Add failed.");
                    }
                }
            });


            /*Thread thread2 = new Thread(async () =>
            {*/
            if (listImageURL != null)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, $"images\\");
                if (Directory.Exists(path))
                {
                    List<string> List_imageURL = new List<string>();

                    foreach (var item in listImageURL)
                    {
                        var index = listImageURL.IndexOf(item) + 1;
                        var fileName = product.product_name + " images_detail_" + index;
                        var image = item;
                        if (image.Length > 0)
                        {
                            if (!Directory.GetFiles(path).Any(item => item.Contains(fileName)))
                            {
                                image_repo.SaveImage(ImageURL, Path.Combine(path, fileName + ".jpg"));

                            }
                            using var stream2 = new FileStream(Path.Combine(path, fileName), FileMode.Open);
                            string URL = await image_repo.UploadToStorage(product.id, stream2, fileName);
                            List_imageURL.Add(URL);
                            stream2.Close();
                        }
                    }

                    image_repo.AddImages(product.id, List_imageURL);
                }
            }
            /*});
            thread2.Start();
            thread2.Join();*/
            thread1.Start();
            thread1.Join();

            return StatusCode(201, product);
        }

        [HttpPost("Post3")]
        public ActionResult CreateProduct3(Product_toPost product)
        {
            var addproduct = products_repo.Add_Product(product);
            if (addproduct == false)
                return BadRequest();
            return Ok("Added successfully");
        }
        //[AllowAnonymous]
        [HttpGet("GetAll")]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var allproducts = products_repo.Get_AllProducts();
            if (allproducts == null)
            {
                return NotFound("No list found.");
            }
            return Ok(allproducts);
        }

        [HttpGet("GetAll_Id")]
        public ActionResult<List<string>> GetIDProducts()
        {
            var allproducts = products_repo.GetIDProducts();
            if (allproducts == null)
            {
                return NotFound("No list found.");
            }
            return Ok(allproducts);
        }

        [HttpGet("GetAll_ProductByType")]
        public ActionResult<List<Product>> GetProductsByType(string product_type)
        {
            if (product_type == null)
                return BadRequest("You have not entered the information you are looking for.");
            try
            {
                var allproducts = products_repo.Get_AllProducts_ByType(product_type);
                if (allproducts == null)
                {
                    return NotFound("No list found.");
                }
                return Ok(allproducts);
            }
            catch (Exception)
            {
                return BadRequest();
            } 
        }

        [HttpGet("GetAll_ProductByName")]
        public ActionResult<List<Product>> GetProductsByName(string product_name)
        {
            if (product_name == null)
                return BadRequest("You have not entered the information you are looking for.");
            try
            {
                var allproducts = products_repo.Get_AllProducts_ByName(product_name);
                if (allproducts == null)
                {
                    return NotFound("No list found.");
                }
                return Ok(allproducts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(string id)
        {
            var product = products_repo.Get_Product(id);
            if (product == null)
            {
                return NotFound($"Product with ID '{id}' was not found.");
            }
            return Ok(product);
        }

        //[Authorize(Roles = "admin, employee")]
        [HttpPut("Update")]
        public ActionResult<Product> UpdateProduct(Product_toUpdate product)
        {
            if (!products_repo.isProductExist(product.id))
            {
                return NotFound($"Product with ID '{product.id}' was not found.");
            }
            var update = products_repo.Update_Product(product);
            if (!update)
                return BadRequest("Update failed.");
            return Ok(product);
        }

        //[Authorize(Roles = "admin, employee")]
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteProduct(string id)
        {
            if(!products_repo.isProductExist(id))
            {
                return NotFound($"Product with ID '{id}' was not found.");
            }
            var delete = products_repo.Delete_Product(id);
            if (!delete)
                return BadRequest("Delete failed.");
            return Ok("Deleted successfully");
        }
    }
}
