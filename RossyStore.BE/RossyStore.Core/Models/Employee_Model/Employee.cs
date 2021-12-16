
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Models.Employee_Model
{
    public class Employee
    {
        //ID will be automatically generated.
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string employee_name { get; set; }

        [Required]
        public string employee_sex { get; set; }

        //public DateTime employee_birthday { get; set; }

        [Required]
        public string employee_address { get; set; }

        [Required]
        [Phone]
        [StringLength(maximumLength: 11, MinimumLength = 10)]
        public string employee_phoneNumber { get; set; }

        //[Required]
        //public DateTime DayStartWorking { get; set; }

        [Required]
        [EmailAddress]
        public string employee_email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string employee_password { get; set; }

        public string Role { get; set; }
    }
}
