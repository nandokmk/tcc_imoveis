using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using Tcc_Imoveis.Models;
using System.Web.Security;
using Facebook.Web;

namespace Tcc_Imoveis.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            if (FacebookWebContext.Current.IsAuthenticated())
            {
                return Redirect(returnUrl);
            }
            return View();
        }

    }
}