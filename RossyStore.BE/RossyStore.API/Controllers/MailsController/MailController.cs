using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RossyStore.Core.Interfaces.Mail_Interface;
using RossyStore.Core.Models.Mail_Model;
using RossyStore.Core.Repositories.Mail_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RossyStore.API.Controllers.MailsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMail _imail;
        
        public MailController(IMail imail)
        {
            _imail = imail;
        }

        [HttpPost("Send")]
        public async Task<ActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await _imail.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
