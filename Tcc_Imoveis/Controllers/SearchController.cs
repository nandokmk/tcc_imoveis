using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tcc_Imoveis.Models;

namespace Tcc_Imoveis.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Fernando()
        {
            

            ViewBag.Fernando = "Programador Analista";
            return View();
        }


    }
}
