using DemoMvc4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Example(int id)
        {
            var examples = ExampleSpec.ExamplesStatic();
            ExampleSpec exampleSpec = examples.Single((x) => (x.Id == id));

            var example = new PayrollExample(exampleSpec);

            return View(example);
        }
    }
}
