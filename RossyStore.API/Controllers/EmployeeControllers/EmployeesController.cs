using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RossyStore.Core.Models.Employee_Model;
using RossyStore.Core.Repositories.Employee_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RossyStore.API.Controllers.EmployeeControllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private EmployeeRepository employee_repo = new EmployeeRepository();
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            if (employee_repo.isPhoneNumberExist(employee.employee_phoneNumber))
            {
                ModelState.AddModelError("employee_phoneNumber", "Phone number is existed.");
            }
            if (employee_repo.isEmailExist(employee.employee_email))
            {
                ModelState.AddModelError("emloyee_email", "Email is existed.");
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
                    if (errorlist.IndexOf(item) < (errorlist.Count - 1))
                        errors += "\n";
                }
                return BadRequest(errors);
            }
            employee_repo.Add_Employee(employee);
            return StatusCode(201, employee);
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Employee>> GetAllEmployees()
        {
            var allEmployees = employee_repo.Get_AllEmployees();
            if (allEmployees == null)
            {
                return NotFound("No list found.");
            }
            return Ok(allEmployees);
        }

        [HttpGet("{employee_id}")]
        public ActionResult<Employee> GetEmployee(string employee_id)
        {
            var employee = employee_repo.Get_Employee(employee_id);
            if (employee == null)
            {
                return NotFound($"Employee with ID: {employee_id} was not found.");
            }
            return Ok(employee);
        }

        [HttpGet("GetByName/{name}")]
        public ActionResult<List<Employee>> GetEmployeeByName(string name)
        {
            var employee = employee_repo.Get_Employee_ByName(name);
            if (employee == null)
            {
                return NotFound($"Employee with name '{name}' was not found.");
            }
            return Ok(employee);
        }

        [HttpGet("GetByPhone/{phone}")]
        public ActionResult<Employee> GetEmployeeByPhone(string phone)
        {
            var employee = employee_repo.Get_Employee_ByPhone(phone);
            if (employee == null)
            {
                return NotFound($"Employee with phone number '{phone}' was not found.");
            }
            return Ok(employee);
        }

        [HttpPut("Update")]
        public ActionResult<Employee> UpdateEmployee(Employee employee)
        {
            if (employee_repo.Get_Employee(employee.id) == null)
            {
                return NotFound($"Employee with ID: {employee.id} was not found.");
            }
            employee_repo.Update_Employee(employee);
            return Ok(employee);
        }

        [HttpDelete("Delete/{employee_id}")]
        public ActionResult DeleteEmployee(string employee_id)
        {
            if (employee_id == null)
                return BadRequest("ID not entered.");
            if (employee_repo.Get_Employee(employee_id) == null)
            {
                return NotFound($"Employee with ID: {employee_id} was not found.");
            }
            employee_repo.Delete_Employee(employee_id);
            return Ok("Deleted successfully");
        }
    }
}
