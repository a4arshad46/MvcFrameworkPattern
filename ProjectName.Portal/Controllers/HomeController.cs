using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectName.Data.Services;

namespace ProjectName.Portal.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeesService oIEmployeesService;
        public HomeController(IEmployeesService IEmployeesServiceIntializer)
        {
            oIEmployeesService = IEmployeesServiceIntializer;
        }
        public ActionResult Index()
        {
            var employee = oIEmployeesService.IlGetAllEmployees(1, 10, "", "");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}