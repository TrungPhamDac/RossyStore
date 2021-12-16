using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RossyStore.Core.Models.Customer_Model;
using RossyStore.Core.Repositories.Customer_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RossyStore.API.Controllers.CustomerControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private CustomerRepository customer_repo = new CustomerRepository();
        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            if (customer_repo.isPhoneNumberExist(customer.customer_phoneNumber))
            {
                ModelState.AddModelError("customer_phoneNumber", "Phone number is existed.");
            }
            if (customer_repo.isEmailExist(customer.customer_email))
            {
                ModelState.AddModelError("customer_email", "Email is existed.");
            }
            if (!ModelState.IsValid)
            {
                var errorlist = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();
                string errors = "";
                foreach (var item in errorlist)
                {
                    errors += item;
                    if (errorlist.IndexOf(item) < (errorlist.Count-1))
                        errors += "\n";
                }
                return BadRequest(errors);
            }
            customer_repo.Add_Customer(customer);
            return StatusCode(201, customer);
        }

        //[Authorize]
        [HttpGet("GetAll")]
        public ActionResult<List<Customer>> GetAllCustomers()
        {
            var allCustomers = customer_repo.Get_AllCustomers();
            if (allCustomers == null)
            {
                return NotFound("No list found.");
            }
            return Ok(allCustomers);
        }

        [HttpGet("GetByID/{customer_id}")]
        public ActionResult<Customer> GetCustomer(string customer_id)
        {
            var customer = customer_repo.Get_Customer(customer_id);
            if (customer == null)
            {
                return NotFound($"Customer with ID: {customer_id} was not found.");
            }
            return Ok(customer);
        }

        [HttpGet("GetByName/{customer_name}")]
        public ActionResult<List<Customer>> GetCustomer_byName(string customer_name)
        {
            var customer = customer_repo.Get_Customer_byName(customer_name);
            if (customer == null)
            {
                return NotFound($"Customer with name '{customer_name}' was not found.");
            }
            return Ok(customer);
        }

        [HttpGet("GetByPhone/{customer_phone}")]
        public ActionResult<Customer> GetCustomer_byPhone(string customer_phone)
        {
            var customer = customer_repo.Get_Customer_byPhone(customer_phone);
            if (customer == null)
            {
                return NotFound($"Customer with phone number '{customer_phone}' was not found.");
            }
            return Ok(customer);
        }

        [HttpPut("Update")]
        public ActionResult<Customer> UpdateCustomer(Customer customer)
        {
            if (customer_repo.Get_Customer(customer.id) == null)
            {
                return NotFound($"Customer with ID: {customer.id} was not found.");
            }
            customer_repo.Update_Customer(customer);
            return Ok(customer);
        }

        [HttpDelete("Delete/{customer_id}")]
        public ActionResult DeleteCustomer(string customer_id)
        {
            if(customer_id == null)
            {
                return BadRequest("ID not entered.");
            }
            if (customer_repo.Get_Customer(customer_id) == null)
            {
                return NotFound($"Customer with ID: {customer_id} was not found.");
            }
            customer_repo.Delete_Customer(customer_id);
            return Ok("Deleted successfully");
        }
    }
}
