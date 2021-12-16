using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RossyStore.Core.Models.Customer_Model;
using RossyStore.Core.Models.Employee_Model;
using RossyStore.Core.Models.SignIn_Model;
using RossyStore.Core.Repositories.User_Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.API.Controllers.SignIn_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private IConfiguration _config;
        private UserRepository user_repo = new UserRepository();
        public SignInController(IConfiguration config)
        {
            _config = config;
        }

        //[AllowAnonymous]
        [HttpPost("CustomerLogin")]
        public ActionResult CustomerLogin(SignIn userLogin)
        {
            var user = Authenticate_customer(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        //[AllowAnonymous]
        [HttpPost("EmployeeLogin")]
        public ActionResult EmployeeLogin(SignIn userLogin)
        {
            var user = Authenticate_employee(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        } 

        private string Generate(Customer customerLogin)
        {
            var secutiryKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secutiryKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, customerLogin.customer_email),
                //new Claim(ClaimTypes.GivenName, customerLogin.customer_name),
                new Claim(ClaimTypes.Role, "customer")
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string Generate(Employee employeeLogin)
        {
            var secutiryKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secutiryKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, employeeLogin.employee_name),
                //new Claim(ClaimTypes.GivenName, employeeLogin.employee_name),
                new Claim(ClaimTypes.Role, employeeLogin.Role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Customer Authenticate_customer(SignIn userLogin)
        {
            var currentUser = user_repo.GetCustomer(userLogin.Email, userLogin.Password);

            if (currentUser != null)
                return currentUser;
            return null;
        }

        private Employee Authenticate_employee(SignIn userLogin)
        {
            var currentUser = user_repo.GetEmployee(userLogin.Email, userLogin.Password);

            if (currentUser != null)
                return currentUser;
            return null;
        }
    }
}
