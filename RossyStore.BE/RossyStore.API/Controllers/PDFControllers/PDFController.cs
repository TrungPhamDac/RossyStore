using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RossyStore.Core.Repositories.PDF_Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RossyStore.API.Controllers.PDFControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly IConverter _converter;
        public PDFController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        public ActionResult CreatePDF(string order_id)
        {
            new PDFConverter(_converter).CreatePDF(order_id);
            return Ok("Successfully.");
        }
    }
}
