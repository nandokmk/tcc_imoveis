using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tcc_Imoveis.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Pagina principal!";

            return View();
        }

        public ActionResult SobreNos()
        {
            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }
    }
}
