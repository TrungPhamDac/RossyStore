using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RossyStore.Core.Models.ShoppingCart_Model;
using RossyStore.Core.Models.Product_Model;
using RossyStore.Core.Repositories.Customer_Repository;
using RossyStore.Core.Repositories.Customer_Repository.ShoppingCart_Repository;
using RossyStore.Core.Repositories.Product_Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace RossyStore.API.Controllers.CustomerControllers.ShoppingCartController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ShoppingCartRepository shoppingCart_repo = new ShoppingCartRepository();
        private ProductDetailRepository productDetail_repo = new ProductDetailRepository();
        private ProductRepository product_repo = new ProductRepository();
        private CustomerRepository customer_repo = new CustomerRepository();

        [HttpPost("{customer_id}/Post1")]
        public ActionResult AddProductToCart1(string customer_id, _ShoppingCart Cart)
        {
            int error = 0;

            Thread check_customer = new Thread(() =>
            {
                if (!customer_repo.isCustomerExist(customer_id))
                    error++;
            });

            Thread check_product = new Thread(() =>
            {
                if (!product_repo.isProductExist(Cart.id))
                    error++;
            });

            Thread check_color_size = new Thread(() =>
            {
                if (!productDetail_repo.isColorAndSizeExist(Cart.id, Cart.Product_detail_color, Cart.Product_detail_size))
                    error++;
            });

            check_customer.Start();
            check_product.Start();
            check_color_size.Start();
            check_customer.Join();
            check_product.Join();
            check_color_size.Join();

            if (error > 0)
                return BadRequest("Incorrect infomation.");
            int ProductDetail_Quantity = productDetail_repo.GetProductDetailQuantity(Cart.id, Cart.Product_detail_color, Cart.Product_detail_size);
            if (Cart.Quantity > ProductDetail_Quantity)
                return BadRequest($"The number of products in stock is not enough.");
            if (shoppingCart_repo.isProductExistInCart(customer_id, Cart.id, Cart.Product_detail_color, Cart.Product_detail_size))
                return BadRequest($"Product '{Cart.id}' with color '{Cart.Product_detail_color}' and size '{Cart.Product_detail_size}' had been added into cart before.");
            var add = shoppingCart_repo.Add_Product_to_Cart(customer_id, Cart.id, Cart.Product_detail_color, Cart.Product_detail_size, Cart.Quantity);
            if (add == false)
                return BadRequest("Add failed.");
            return Ok("Added successfully.");
        }

        [HttpPost("{customer_id}/Post2")]
        public ActionResult AddProductToCart2(string customer_id, _ShoppingCart Cart)
        {
            int error = 0;

            Thread check_customer = new Thread(() =>
            {
                if (!customer_repo.isCustomerExist(customer_id))
                    error++;
            });

            Thread check_product = new Thread(() =>
            {
                if (!product_repo.isProductExist(Cart.id))
                    error++;
            });

            Thread check_color_size = new Thread(() =>
            {
                if (!productDetail_repo.isColorAndSizeExist(Cart.id, Cart.Product_detail_color, Cart.Product_detail_size))
                    error++;
            });

            check_customer.Start();
            check_product.Start();
            check_color_size.Start();
            check_customer.Join();
            check_product.Join();
            check_color_size.Join();

            if (error > 0)
                return BadRequest("Incorrect infomation.");
            int ProductDetail_Quantity = productDetail_repo.GetProductDetailQuantity(Cart.id, Cart.Product_detail_color, Cart.Product_detail_size);
            if (Cart.Quantity > ProductDetail_Quantity)
                return BadRequest($"The number of products in stock is not enough.");
            if (shoppingCart_repo.isProductExistInCart(customer_id, Cart.id, Cart.Product_detail_color, Cart.Product_detail_size))
                return BadRequest($"Product '{Cart.id}' with color '{Cart.Product_detail_color}' and size '{Cart.Product_detail_size}' had been added into cart before.");
            var add = shoppingCart_repo.Add_Product_to_Cart(customer_id, Cart.id, Cart.Product_detail_color, Cart.Product_detail_size, Cart.Quantity);
            if (add == false)
                return BadRequest("Add failed.");
            return Ok("Added successfully.");
        }

        [HttpPut("{customer_id}/Update")]
        public ActionResult UpdateProcuctInCart(string customer_id, _ShoppingCart Cart)
        {
            int error = 0;

            Thread check_customer = new Thread(() =>
            {
                if (!customer_repo.isCustomerExist(customer_id))
                    error++;
            });

            Thread check_product = new Thread(() =>
            {
                if (!product_repo.isProductExist(Cart.id))
                    error++;
            });

            Thread check_color_size = new Thread(() =>
            {
                if (!productDetail_repo.isColorAndSizeExist(Cart.id, Cart.Product_detail_color, Cart.Product_detail_size))
                    error++;
            });

            check_customer.Start();
            check_product.Start();
            check_color_size.Start();
            check_customer.Join();
            check_product.Join();
            check_color_size.Join();

            if (error > 0)
                return BadRequest("Incorrect infomation.");
            int ProductDetail_Quantity = productDetail_repo.GetProductDetailQuantity(Cart.id, Cart.Product_detail_color, Cart.Product_detail_size);
            if (Cart.Quantity > ProductDetail_Quantity)
                return BadRequest($"The number of products in stock is not enough.");
            var update = shoppingCart_repo.Update_ProductDetail(customer_id, Cart.id, Cart.Product_detail_color, Cart.Product_detail_size, Cart.Quantity);
            if (update == false)
                return BadRequest("Update failed.");

            return Ok("Updated successfully.");
        }

        [HttpGet("{customer_id}/GetAll_ProductInCart")]
        public ActionResult<List<ShoppingCart>> GetAll_ProductInCart(string customer_id)
        {
            if (!customer_repo.isCustomerExist(customer_id))
                return NotFound($"Customer with ID '{customer_id}' was not found.");
            var allProductDetails = shoppingCart_repo.GetAll_IDProductAndDetailInCart(customer_id);
            if (allProductDetails == null)
            {
                return NotFound("No list found.");
            }
            return Ok(allProductDetails);
        }


        [HttpGet("{customer_id}/{cart_id}")]
        public ActionResult<ShoppingCart> Get_ProductWithDetailInCart(string customer_id, string cart_id)
        {
            if (customer_repo.Get_Customer(customer_id) == null)
                return NotFound($"Customer with ID '{customer_id}' was not found.");
            try
            {
                ShoppingCart info = shoppingCart_repo.Get_ProductAndDetailInCart(customer_id, cart_id);
                if (info != null)
                    return Ok(info);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("{customer_id}/Delete1")]
        public ActionResult DeleteProductInCart(string customer_id, __ShoppingCart Cart)
        {
            int error = 0;

            Thread check_customer = new Thread(() =>
            {
                if (!customer_repo.isCustomerExist(customer_id))
                    error++;
            });

            Thread check_product = new Thread(() =>
            {
                if (!product_repo.isProductExist(Cart.Product_id))
                    error++;
            });

            Thread check_color_size = new Thread(() =>
            {
                if (!productDetail_repo.isColorAndSizeExist(Cart.Product_id, Cart.Product_detail_color, Cart.Product_detail_size))
                    error++;
            });

            check_customer.Start();
            check_product.Start();
            check_color_size.Start();
            check_customer.Join();
            check_product.Join();
            check_color_size.Join();

            if (error > 0)
                return BadRequest("Incorrect infomation.");
            var delete = shoppingCart_repo.Delete_ProductDetail(customer_id, Cart.Product_id, Cart.Product_detail_color, Cart.Product_detail_size);
            if (delete == false)
                return BadRequest("Delete failed.");

            return Ok("Deleted successfully.");
        }

        [HttpDelete("{customer_id}/Delete2/{cart_id}")]
        public ActionResult DeleteProductInCart2(string customer_id, string cart_id)
        {
            if (cart_id == null)
            {
                return BadRequest("cart_id is null");
            }
            if(!shoppingCart_repo.isProductExistInCart(customer_id, cart_id))
            {
                return BadRequest("This product is not existed in cart.");
            }
            var delete = shoppingCart_repo.Delete_ProductDetail(customer_id, cart_id);
            if (delete == false)
                return BadRequest("Delete failed.");
            return Ok("Deleted successfully");
        }
    }
}
