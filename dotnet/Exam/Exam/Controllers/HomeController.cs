using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Exam.Models;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ScanFormModel model)
        {
            var urlAndData = Scaner.ScanPage(model.Url, model.Url, model.Depth, 0);
            var builder = new StringBuilder();
            foreach(var item in urlAndData)
            {
                builder.Append("======================================================================\n");
                builder.Append(item.Item1);
                builder.Append("\n\n");
                builder.Append(item.Item2);
                builder.Append("\n\n\n");
                builder.Append("======================================================================\n");
            }
            return Content(builder.ToString());
        }
        
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult About(ViewResultsModel model)
        {
            var data = Scaner.GetDataByUrl(model.Url);
            var builder = new StringBuilder();
            foreach (var item in data)
            {
                builder.Append("======================================================================\n");
                builder.Append(item.Item1);
                builder.Append("\n\n");
                builder.Append(item.Item2);
                builder.Append("\n\n\n");
                builder.Append("======================================================================\n");
            }
            return Content(builder.ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
