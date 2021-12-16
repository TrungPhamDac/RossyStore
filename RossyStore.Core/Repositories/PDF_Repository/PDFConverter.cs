using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.PDF_Repository
{
    public class PDFConverter
    {
        private readonly IConverter _converter;
        public PDFConverter(IConverter converter)
        {
            _converter = converter;
        }
        public string CreatePDF(string order_id)
        {
            DateTime date = DateTime.Now;
            string report_fileName = "REP" 
                + date.ToString("dd")
                + date.ToString("MM")
                + date.ToString("yyyy")
                + date.ToString("HH")
                + date.ToString("mm")
                + date.ToString("ss")
                + ".pdf";
            //string path = Path.Combine(WebRootPath + "\\Report\\");
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Report",
                //Out = @"E:\C#\Đồ án\RossyStore.BE\RossyStore.API\wwwroot\report\" + report_fileName
                Out = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "report", report_fileName)
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = new TemplateGenerator().GetHTMLString(order_id)
                //WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "style.css") },
            };

            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return report_fileName;
        }
    }
}
