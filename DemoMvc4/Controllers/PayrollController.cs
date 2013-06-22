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
            return View();
        }

        public ActionResult Example(int id)
        {
            var example = new PayrollExample();
            return View(example);
        }
    }
}
