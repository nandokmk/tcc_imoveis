using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tcc_Imoveis.Models;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;

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

            tcc_imoveisEntities tcc = new tcc_imoveisEntities();

            ObjectResult<GetImovel_Result> imovel = tcc.get_imovel(id);

            ViewBag.imovel = imovel.ElementAt(0);
            imovel.Dispose();
                
            ObjectResult<ImovelAtributo_Result> atributos = tcc.ListaAtributosImovel(id);

            ViewBag.imovelAtributos = atributos.ToList();
            atributos.Dispose();

            ObjectResult<ImovelImagens_Result> imagens = tcc.ListaImagensImovel(id);

            List<ImovelImagens_Result> imagensImovel = imagens.ToList();
            if (imagensImovel.Count > 0)
            {
                ViewBag.ImagemPrincipal = imagensImovel.ElementAt(0).imagem;
            }
            
            imagens.Dispose();


       
            

            return View();

            

               
                
            
           
        }

        //
        // GET: /Imovel/Create

        public ActionResult Create()
        {
            
           

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
