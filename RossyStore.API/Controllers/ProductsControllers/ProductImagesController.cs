using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RossyStore.Core.Models.Product_Model;
using RossyStore.Core.Repositories.Product_Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RossyStore.API.Controllers.ProductsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductImagesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private ProductImageRepository image_repo = new ProductImageRepository();
        private ProductRepository products_repo = new ProductRepository();

        [HttpPost("{Product_ID}/Post")]
        public async Task<ActionResult> PostImageProduct(string Product_ID, [FromForm] List_Product_Image Image)
        {
            if (Image.ListImageDetail != null)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, $"images\\");
                if (Directory.Exists(path))
                {
                    List<string> List_imageURL = new List<string>();

                    foreach (var item in Image.ListImageDetail)
                    {
                        var image = item;
                        if (image.Length > 0)
                        {
                            if (!Directory.GetFiles(path).Any(item => item.Contains(image.FileName)))
                            {
                                using var stream1 = new FileStream(Path.Combine(path, image.FileName), FileMode.Create);
                                await image.CopyToAsync(stream1);
                                stream1.Close();
                            }
                            using var stream2 = new FileStream(Path.Combine(path, image.FileName), FileMode.Open);
                            string URL = await image_repo.UploadToStorage(Product_ID, stream2, image.FileName);
                            List_imageURL.Add(URL);
                            stream2.Close();
                        }
                    }
                    
                    image_repo.AddImages(Product_ID, List_imageURL);
                }                 
            }

            return Ok("Added successfully.");
        }

        [HttpPost("{Product_ID}/PostWithImageURL1")]
        public ActionResult Post(string Product_ID, string Filename, string imageURL)
        {
            var product = products_repo.Get_Product(Product_ID);
            if (product == null)
            {
                return NotFound($"Product with ID '{Product_ID}' was not found.");
            }
            image_repo.AddImages(Product_ID, Filename, imageURL);
            return Ok("Added successfully.");
        }



        [HttpGet("{Product_ID}/Get")]
        public ActionResult GetImage(string Product_ID, string FileName)
        {
            var product = products_repo.Get_Product(Product_ID);
            if (product == null)
            {
                return NotFound($"Product with ID '{Product_ID}' was not found.");
            }
            var URI = image_repo.GetImage(Product_ID, FileName);
            if (URI == "null" || URI == null || URI == "")
            {
                return NotFound($"File with name '{FileName}' was not found.");
            }
            return Ok(URI);
        }

        [HttpGet("{Product_ID}/GetProductDetailImages")]
        public ActionResult<List<string>> GetImages(string Product_ID)
        {
            var product = products_repo.Get_Product(Product_ID);
            if (product == null)
            {
                return NotFound($"Product with ID '{Product_ID}' was not found.");
            }
            var images = image_repo.GetAllProductDetailImages(Product_ID);
            if (images == null) return NotFound("No list found.");
            return Ok(images);
        }

        [HttpPut("{Product_ID}/UpdateWithImageURL")]
        public ActionResult UpdateImage(string Product_ID, string FileName, string imageURL)
        {
            var result = GetImage(Product_ID, FileName);
            try
            {
                var getImage = ((OkResult)result).StatusCode;
                if (getImage == Ok().StatusCode)
                {
                    image_repo.UpadteImage(Product_ID, FileName, imageURL);
                }
            }
            catch (Exception)
            {
                var getImage = ((NotFoundObjectResult)result).StatusCode;
                if (getImage != Ok().StatusCode)
                {
                    return result;
                }
            }
            return Ok("Updated successfully.");
        }

        [HttpDelete("{Product_ID}/Delete")]
        public ActionResult DeleteImage(string Product_ID, string FileName)
        {
            try
            {
                image_repo.DeleteImage(Product_ID, FileName);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok("Deleted successfully.");
        }
    }
}
