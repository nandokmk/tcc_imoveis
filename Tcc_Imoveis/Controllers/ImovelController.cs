using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tcc_Imoveis.Models;

namespace Tcc_Imoveis.Controllers
{
    public class ImovelController : Controller
    {
        //
        // GET: /Imovel/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Imovel/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Imovel/Create

        public ActionResult Create()
        {
            
            using (var db = new tcc_imoveisEntities1())
            {
                List<bairro> bairros = db.bairro.ToList();
                ViewBag.bairros = bairros;
            }


            return View();
        } 

        //
        // POST: /Imovel/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Imovel/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Imovel/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Imovel/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Imovel/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
