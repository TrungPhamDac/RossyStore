using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RossyStore.Core.Models.Product_Model;
using RossyStore.Core.Repositories.Product_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RossyStore.API.Controllers.ProductsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private ProductDetailRepository productDetail_repo = new ProductDetailRepository();
        private ProductRepository product_repo = new ProductRepository();

        [HttpPost("{product_id}/Post")]
        public ActionResult<Product_Detail> CreateProductDetail(string product_id, Product_Detail productDetail)
        {
            if(!product_repo.isProductExist(product_id))
            {
                return NotFound($"Product with id '{product_id}' was not found.");
            }
            if(productDetail.product_detail_size < 34 || productDetail.product_detail_size > 43)
            {
                return BadRequest($"Invalid shoe size - [34;43]");
            }    
            if(productDetail_repo.isProductDetailExist(product_id, productDetail.product_detail_color, productDetail.product_detail_size))
            {
                return BadRequest($"Product detail with color '{productDetail.product_detail_color}' and size '{productDetail.product_detail_size}' has been existed.");
            }
            var Detail = productDetail_repo.Add_ProductDetail(product_id, productDetail);
            if (Detail == null)
                /*return StatusCode(409, $"Product detail with color '{productDetail.product_detail_color}' " +
                 $"and size '{productDetail.product_detail_size}' already exists");*/
                return BadRequest();
            return StatusCode(201, productDetail);
        }

        [HttpGet("{product_id}/{productDetail_id}")]
        public ActionResult<Product_Detail> GetProductDetail(string product_id, string productDetail_id)
        {
            if (!product_repo.isProductExist(product_id))
            {
                return NotFound($"Product with id '{product_id}' was not found.");
            }
            var productDetail = productDetail_repo.Get_ProductDetail(product_id, productDetail_id);
            if(productDetail == null)
            {
                return NotFound($"Product detail with ID: {productDetail_id} was not found.");
            }
            return Ok(productDetail);
        }

        [HttpGet("{product_id}/GetAll")]
        public ActionResult<List<Product_Detail>> GetAllProductDetails(string product_id)
        {
            if (!product_repo.isProductExist(product_id))
            {
                return NotFound($"Product with id '{product_id}' was not found.");
            }
            var allProductDetails = productDetail_repo.Get_AllProductDetails(product_id);
            if (allProductDetails == null)
            {
                return NotFound("No list found.");
            }
            return Ok(allProductDetails);
        }

        [HttpGet("{product_id}/GetAllId")]
        public ActionResult<List<string>> GetIdProductDetails(string product_id)
        {
            if (!product_repo.isProductExist(product_id))
            {
                return NotFound($"Product with id '{product_id}' was not found.");
            }
            var allProductDetails = productDetail_repo.GetIDProductDetails(product_id);
            if (allProductDetails == null)
            {
                return NotFound("No list found.");
            }
            return Ok(allProductDetails);
        }

        [HttpPut("{product_id}/Update1")]
        public ActionResult<Product_Detail> UpdateProductDetail(string product_id, Product_Detail product_Detail)
        {
            if (!product_repo.isProductExist(product_id))
            {
                return NotFound($"Product with id '{product_id}' was not found.");
            }
            if (!productDetail_repo.isProductDetailExist(product_id, product_Detail.id))
            {
                return NotFound($"Product detail with ID '{product_Detail.id}' was not found.");
            }
            var update = productDetail_repo.Update_ProductDetail(product_id, product_Detail);
            if(update == false)
                return BadRequest("Udpate failed.");
            return Ok(product_Detail);
        }


        [HttpPut("{product_id}/Update2")]
        public ActionResult<Product_Detail> UpdateProductDetail2(string product_id, _Product_detail detail)
        {
            /*if (quantity <= 0)
                return BadRequest("Not enough information to update.");*/
            if (!product_repo.isProductExist(product_id))
            {
                return NotFound($"Product with id '{product_id}' was not found.");
            }
            if (!productDetail_repo.isProductDetailExist(product_id, detail.product_detail_id))
            {
                return NotFound($"Product detail with ID '{detail.product_detail_id}' was not found.");
            }
            var update = productDetail_repo.Update_ProductDetail_Quantity(product_id, detail.product_detail_id, detail.quantity);
            if (update == false)
                return BadRequest("Update failed.");
            return Ok("Updated successfully.");
        }


        [HttpDelete("{product_id}/Delete/{productDetail_id}")]
        public ActionResult<Product_Detail> DeleteProductDetail(string product_id, string productDetail_id)
        {
            if (!product_repo.isProductExist(product_id))
            {
                return NotFound($"Product with id '{product_id}' was not found.");
            }
            if (!productDetail_repo.isProductDetailExist(product_id, productDetail_id))
            {
                return NotFound($"Product detail with ID '{productDetail_id}' was not found.");
            }
            productDetail_repo.Delete_ProductDetail(product_id, productDetail_id);
            return Ok("Delete successfully.");
        }

        [HttpDelete("{product_id}/Delete2")]
        public ActionResult<Product_Detail> DeleteProductDetail(string product_id, string color, int size)
        {
            if (!product_repo.isProductExist(product_id))
            {
                return NotFound($"Product with id '{product_id}' was not found.");
            }
            if (!productDetail_repo.isProductDetailExist(product_id, color, size))
            {
                return NotFound($"Product detail with color '{color}' and size '{size}' was not found.");
            }
            productDetail_repo.Delete_ProductDetail(product_id, color, size);
            return Ok("Delete successfully.");
        }
    }
}
