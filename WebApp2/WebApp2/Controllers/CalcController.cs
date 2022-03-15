using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp2.Models;

namespace WebApp2.Controllers
{
    public class CalcController : Controller
    {
        private readonly ILogger<CalcController> _logger;

        public CalcController(ILogger<CalcController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Manual()
        {
            if (Request.Method == "POST")
            {
                try
                {
                    var calcModel = new CalcModel
                    {
                        x = Int32.Parse(HttpContext.Request.Form["x"]),
                        Operation = HttpContext.Request.Form["operation"],
                        y = Int32.Parse(HttpContext.Request.Form["y"])
                    };

                    ViewBag.Result = calcModel.Calc();
                }
                catch
                {
                    ViewBag.Result = "Invalid input";
                }

                return View("Result");
            }
            return View("ViewWithHtmlHelpers");
        }

        [HttpGet]
        [ActionName("ManualWithSeparateHandlers")]
        public IActionResult ManualWithSeparateHandlersGet()
        {
            return View("ViewWithHtmlHelpers");
        }

        [HttpPost]
        [ActionName("ManualWithSeparateHandlers")]
        public IActionResult ManualWithSeparateHandlersPost()
        {
            try
            {
                var calcModel = new CalcModel
                {
                    x = Int32.Parse(HttpContext.Request.Form["x"]),
                    Operation = HttpContext.Request.Form["operation"],
                    y = Int32.Parse(HttpContext.Request.Form["y"])
                };

                ViewBag.Result = calcModel.Calc();
            }
            catch
            {
                ViewBag.Result = "Invalid input";
            }

            return View("Result");
        }

        [HttpGet]
        public IActionResult ModelBindingInParameters()
        {
            return View("ViewWithHtmlHelpers");
        }

        [HttpPost]
        public IActionResult ModelBindingInParameters(int x, string operation, int y)
        {
            if (ModelState.IsValid)
            {
                var calcModel = new CalcModel
                {
                    x = x,
                    Operation = operation,
                    y = y
                };
                ViewBag.Result = calcModel.Calc();
            }
            else
            {
                ViewBag.Result = "Invalid input";
            }

            return View("Result");
        }

        [HttpGet]
        public IActionResult ModelBindingInSeparateModel()
        {
            return View("ViewWithTagHelpers");
        }

        [HttpPost]
        public IActionResult ModelBindingInSeparateModel(CalcModel model)
        {
            ViewBag.Result = ModelState.IsValid
                ? model.Calc()
                : "Invalid input";

            return View("Result");
        }
    }
}

