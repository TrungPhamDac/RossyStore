using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RossyStore.API.Controllers.PDFControllers;
using RossyStore.Core.Interfaces.Mail_Interface;
using RossyStore.Core.Models.Customer_Model;
using RossyStore.Core.Models.Mail_Model;
using RossyStore.Core.Models.Order_Model;
using RossyStore.Core.Models.ShoppingCart_Model;
using RossyStore.Core.Repositories.Customer_Repository;
using RossyStore.Core.Repositories.Customer_Repository.ShoppingCart_Repository;
using RossyStore.Core.Repositories.Order_Repository;
using RossyStore.Core.Repositories.PDF_Repository;
using RossyStore.Core.Repositories.Product_Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static RossyStore.Core.Repositories.Order_Repository.OrderRepository;

namespace RossyStore.API.Controllers.OrdersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private OrderRepository order_repo = new OrderRepository();
        private CustomerRepository customer_repo = new CustomerRepository();
        private ShoppingCartRepository shoppingCart_repo = new ShoppingCartRepository(); 
        private ProductDetailRepository productDetail_repo = new ProductDetailRepository();
        private readonly IMail _imail;
        private readonly IConverter _converter;

        public OrdersController(IMail imail, IConverter converter)
        {
            _imail = imail;
            _converter = converter;
        }

        /*public OrdersController(IConverter converter)
        {
            _converter = converter;
        }*/

        [HttpPost("CreateOrder1")]
        public ActionResult<Order> CreateOrder(_Order o)
        {
            if (!customer_repo.isCustomerExist(o.customer.id))
                return NotFound($"Customer with id '{o.customer.id}' was not found.");
            Order order;
            try
            {
                if (o.list_cart.Count == 0)
                {
                    return BadRequest("There are no products in the cart.");
                }
                else
                {
                    foreach (var item in o.list_cart)
                    {
                        int product_quantity = productDetail_repo.GetProductDetailQuantity(item.id, item.Product_detail_color, item.Product_detail_size);
                        if (item.Quantity > product_quantity)
                            return BadRequest($"Products in stock are not enough");
                    }
                }
                order = order_repo.Add_Order(o.customer, o.list_cart);
                if (order == null)
                    return BadRequest("Add failed.");
                new Thread(() =>
                {
                    MailRequest request = new MailRequest();
                    request.ToEmail = o.customer.customer_email;
                    request.Subject = "RossyStore - Hóa đơn";
                    request.Body = $"Cảm ơn bạn đã mua hàng tại RossyStore \n\n" +
                        $"Ngày hóa đơn: {order.order_date.ToString("dd-MM-yyy HH:mm")} \n\n" +
                        $"Tổng hóa đơn: {String.Format("{0:0,0}", order.order_total)} VNĐ";
                    _imail.SendEmailAsync(request);
                }).Start();
            }
            catch (Exception)
            {
                return BadRequest("Add failed.");
            }
            return StatusCode(201, order);
        }

        [HttpPost("CreateOrder2")]
        public ActionResult<Order> CreateOrder2(_Order o)
        {
            if (!customer_repo.isCustomerExist(o.customer.id))
                return NotFound($"Customer with id '{o.customer.id}' was not found.");
            Order order;
            try
            {
                if (o.list_cart.Count == 0)
                {
                    return BadRequest("There are no products in the cart.");
                }
                else
                {
                    foreach (var item in o.list_cart)
                    {
                        int product_quantity = productDetail_repo.GetProductDetailQuantity(item.id, item.Product_detail_color, item.Product_detail_size);
                        if (item.Quantity > product_quantity)
                            return BadRequest($"Products in stock are not enough");
                    }
                }
                order = order_repo.Add_Order2(o.customer, o.list_cart);
                if (order == null)
                    return BadRequest("Add failed.");
                new Thread(() =>
                {
                    _MailRequest request = new _MailRequest();
                    request.ToEmail = o.customer.customer_email;
                    request.Subject = "RossyStore - Hóa đơn";
                    request.Body = $"Cảm ơn bạn đã mua hàng tại RossyStore \n\n" +
                        $"Ngày hóa đơn: {order.order_date.ToString("dd-MM-yyy HH:mm")} \n\n" +
                        $"Tổng hóa đơn: {String.Format("{0:0,0}", order.order_total)} VNĐ";
                    var fileName = new PDFConverter(_converter).CreatePDF(order.id);
                    request.Attachments = new List<string>();
                    request.Attachments.Add(fileName);
                    _imail.SendEmailAsync(request);
                }).Start();

            }
            catch (Exception ex)
            {
                throw ex;
                return BadRequest("Add failed.");
            }
            return StatusCode(201, order);
        }

        [HttpGet("GetOrder/{order_id}")]
        public ActionResult<Order> GetOrder(string order_id)
        {
            Order order;
            try
            {
                order = order_repo.Get_Order(order_id);
                if (order == null) return NotFound($"Order with id '{order_id}' was not found.");
            }
            catch (Exception)
            {
                return BadRequest("Get failed.");
            }
            return Ok(order);
        }

        //[Authorize(Roles = "admin, employee")]
        [HttpGet("GetAllOrders")]
        public ActionResult<List<Order>> GetAllOrders()
        {
            try
            {
                var listorder = order_repo.Get_AllOrders();
                if (listorder == null)
                    return NotFound("No list found.");
                return Ok(listorder);
            }
            catch (Exception)
            {
                return BadRequest("Get failed.");
            }
        }

        //[Authorize(Roles = "admin, employee")]
        /*[HttpGet("GetAllInfoOrders")]
        public ActionResult<List<OrderDetail>> GetAllInfoOrders()
        {
            try
            {
                var listorder = order_repo.Get_AllOrderInfo();
                if (listorder == null)
                    return NotFound("No list found.");
                return Ok(listorder);
            }
            catch (Exception)
            {
                return BadRequest("Get failed.");
            }
        }*/

        //[Authorize(Roles = "admin, employee")]
        [HttpGet("GetOrder_ByName/{customer_name}")]
        public ActionResult<List<Order>> GetInfoOrderByName(string customer_name)
        {
            try
            {
                var listorder = order_repo.Get_InfoOrder_byName(customer_name);
                if (listorder == null)
                    return NotFound("No list found.");
                return Ok(listorder);
            }
            catch (Exception)
            {
                return BadRequest("Get failed.");
            }
        }

        //[Authorize(Roles = "admin, employee")]
        [HttpGet("GetOrder_ByPhone/{customer_phone}")]
        public ActionResult<List<Order>> GetInfoOrderByPhone(string customer_phone)
        {
            try
            {
                var listorder = order_repo.Get_InfoOrder_byphone(customer_phone);
                if (listorder == null)
                    return NotFound("No list found.");
                return Ok(listorder);
            }
            catch (Exception)
            {
                return BadRequest("Get failed.");
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("Delete/{order_id}")]
        public ActionResult Delete(string order_id)
        {
            try
            {
                var delete = order_repo.Delete_Order(order_id);
                if (delete)
                    return Ok("Deleted successfully.");
                return BadRequest("Deleted failed.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
