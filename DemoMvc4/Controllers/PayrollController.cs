using DemoMvc4.Models;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DemoMvc4.Controllers
{
    public class PayrollController : Controller
    {
        //
        // GET: /Payroll/

        public ActionResult Index()
        {
            var examples = ExampleSpec.ExamplesStatic();
            return View(examples);
        }

        public ActionResult Example(uint id)
        {
            var examples = ExampleSpec.ExamplesStatic();
            ExampleSpec exampleSpec = examples.Single((x) => (x.Id == id));

            var example = new PayrollExample(exampleSpec);

            return View(example);
        }

        public FileStreamResult DownloadPDF(uint id)
        {
            var examples = ExampleSpec.ExamplesStatic();
            ExampleSpec exampleSpec = examples.Single((x) => (x.Id == id));

            var example = new PayrollExample(exampleSpec);

            var stream = new MemoryStream();

            example.CreatePDF(stream, false);

            return File(stream, "text/pdf", "Paycheck.pdf");
        }

        public FileStreamResult DownloadXML(uint id)
        {
            var examples = ExampleSpec.ExamplesStatic();
            ExampleSpec exampleSpec = examples.Single((x) => (x.Id == id));

            var example = new PayrollExample(exampleSpec);

            string stringXML = example.CreateXML();

            var byteArray = Encoding.ASCII.GetBytes(stringXML);

            var stream = new MemoryStream(byteArray);

            return File(stream, "text/xml", "Paycheck.xml");
        }
    }
}
