using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook.Web;
using Tcc_Imoveis.Models;
using System.Data.EntityClient;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.EntityModel;
using System.Text.RegularExpressions;
using System.Data.Objects;
using Facebook;
using System.Web.Helpers;
using Facebook.Web.Mvc;
using System.Collections;
using System.Text;
namespace Tcc_Imoveis.Controllers
{
    public class MapController : Controller
    {
        //
        // GET: /Map/

        public ActionResult Index()
        {
            using (tcc_imoveisEntities tcc = new tcc_imoveisEntities())
            {
                ObjectResult<Condicoes_Result> condicoes = tcc.ListaCondicoes();
                ViewBag.condicoes = condicoes.ToList();

                ObjectResult<AtributosGerais_Result> atributos = tcc.ListaAtributosGerais();
                ViewBag.atributos = atributos.ToList();

                return View();
            }
            
        }


        
        public ActionResult MostraBusca(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public JsonResult Polygon(int id)
        {
            using (tcc_imoveisEntities tcc = new tcc_imoveisEntities())
            {

                ObjectResult<string> result = tcc.ExibePoligono(id);

                string polygon = result.ElementAt(0);

                
                List<string[]> points = new List<string[]>();

                //procura o padrão de coordenadas(latitude e longitude) encontradas na string.
                Regex er = new Regex(@"([0-9-\.]+) ([0-9-\.]+)");

                MatchCollection pointCollection = er.Matches(polygon);

                string[] point = new string[2];
                for (int i = 0; i < pointCollection.Count; i++)
                {
                    point = new string[] { 
                    pointCollection[i].Groups[1].Value.ToString(), //latitude
                    pointCollection[i].Groups[2].Value.ToString() //longitude
                
                };

                    points.Add(point);


                }

                return Json(points);
            }
            
        }

        public ActionResult PesquisaTempoReal()
        {

            tcc_imoveisEntities tcc = new tcc_imoveisEntities();

            ObjectResult<AtributosGerais_Result> atributos = tcc.ListaAtributosGerais();
            var listAtributos = atributos.ToList();



            string sessionId = System.Web.HttpContext.Current.Session.SessionID;

            
            ObjectResult<int?> insert = tcc.InserePesquisa(sessionId, "runtime_search_" + sessionId);
            List<int?> id = insert.ToList();
            if (id.ElementAt(0) != null)
            {
                int idPesquisa = Convert.ToInt32(id.ElementAt(0));

                foreach (var atributo in listAtributos)
                {
                    if (!string.IsNullOrEmpty(Request[atributo.IdImovelAtributoTipo.ToString()]))
                    {
                        string valor = Request[atributo.IdImovelAtributoTipo.ToString()];
                        string condicao = Request["condicao_" + atributo.IdImovelAtributoTipo];

                        
                        
                        tcc.InsereAtributoPesquisa(idPesquisa, 
                            atributo.IdImovelAtributoTipo,
                            HttpUtility.HtmlDecode(condicao),
                            valor);

                        

                        
                    }
                    
                }
                if(!string.IsNullOrEmpty(Request["polygon"])) {
                    string poligono = Util.ToPolygon(Request["polygon"]);
                    tcc.InsereAtributoPesquisa(idPesquisa, "PL", "=", "1");
                    tcc.InserePoligono(idPesquisa, Util.ToPolygon(poligono));
                }


                return RedirectToAction("ListaResult", new { id = idPesquisa });
                
               
            }
            return RedirectToAction("/Map/BuscaGeral");
            
        }


          
        public JsonResult SavePesquisa(string nomePesquisa, string polygon)
        {
            tcc_imoveisEntities tcc = new tcc_imoveisEntities();

 
            if (FacebookWebContext.Current.IsAuthenticated())
            {
                var client = new FacebookWebClient();
                dynamic me = client.Get("me");

                ObjectResult<int?> insert = tcc.InserePesquisa(me.id, nomePesquisa);
                List<int?> id = insert.ToList();
                if (id.ElementAt(0) != null)
                {
                    int idPesquisa = Convert.ToInt32(id.ElementAt(0));
                    tcc.InsereAtributoPesquisa(idPesquisa, "PL", "=", "1");
                    tcc.InserePoligono(idPesquisa, Util.ToPolygon(polygon));
                }
            }
           

                            
            return Json(true);
            
        }

        [FacebookAuthorize(LoginUrl = "/Account/Login?returnUrl=/Map/Listas")]       
        public ActionResult Listas()
        {
            var client = new FacebookWebClient();
            dynamic me = client.Get("me");
            
            tcc_imoveisEntities tcc = new tcc_imoveisEntities();
            

            IEnumerable<Pesquisa_Result> pesquisa_result = tcc.ListaPesquisaUsuario(long.Parse(me.id));

            ViewBag.pesquisas = pesquisa_result.ToList();


            return View();
            
        }


        public ActionResult BuscaGeral()
        {
            tcc_imoveisEntities tcc = new tcc_imoveisEntities();

            
            
            ObjectResult<TipoDado_Result> listaTipoDadoCondicoes = tcc.ListaTipoDadoCondicoes();
            
            Hashtable grupoTipoCondicao = new Hashtable();
            
            List<string> tipos;
            foreach (var tipoCondicao in listaTipoDadoCondicoes.ToList())
            {
                //verifica se existe o ID do tipo de dado na hashtable
                if (grupoTipoCondicao.ContainsKey(tipoCondicao.idtipo_dado))
                {
                    //adiciona um item na lista de condicoes existentes para o tipo de dado acima.
                    ((List<string>)grupoTipoCondicao[tipoCondicao.idtipo_dado]).Add(tipoCondicao.idpesquisa_condicoes);
                }
                else
                {
                    //cria uma lista temporaria
                    tipos = new List<string>();

                    //adiciona uma condicao na lista                    
                    tipos.Add(tipoCondicao.idpesquisa_condicoes);

                    //adiciona a lista na hastable
                    grupoTipoCondicao.Add(tipoCondicao.idtipo_dado,tipos); 
                       
                    
                }
                
            }
            

            List<string> arrayJavaScript = new List<string>();
            foreach (string tipoDado in grupoTipoCondicao.Keys)
            {
                arrayJavaScript.Add(String.Format("'{0}':['{1}']",
                    tipoDado,
                    //junta todas as condicoes do tipo de dado em uma unica string
                    //no formato 'condicao','condicao'
                    String.Join("','", ((List<string>)grupoTipoCondicao[tipoDado]))).ToString()
                );

            }


            ViewBag.saidaJs = String.Join(",\n", arrayJavaScript);

            ObjectResult<AtributosGerais_Result> atributos = tcc.ListaAtributosGerais();
            ViewBag.atributos = atributos.ToList();

            return View();
        }

        
        public JsonResult ListaResult(int id)
        {
            tcc_imoveisEntities tcc = new tcc_imoveisEntities();

            IEnumerable<Imovel_Result> imoveis_result = tcc.ListaImoveisValidos(id);

            return Json(imoveis_result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ImovelResult()
        {
            tcc_imoveisEntities tcc = new tcc_imoveisEntities();
            
            IEnumerable<Imovel_Result> imoveis_result = tcc.ListaImoveis_Todos();

            return Json(imoveis_result);
            
        }

        [HttpPost]
        public JsonResult PolygonResult(string polygon)
        {
            tcc_imoveisEntities tcc = new tcc_imoveisEntities();


            IEnumerable<Imovel_Result> imoveis = tcc.ListaImoveis_Geo(Util.ToPolygon(polygon));
            return Json(imoveis);
        }
        [HttpPost]
        public JsonResult DistanceResult(string distance, string p)
        {
            tcc_imoveisEntities tcc = new tcc_imoveisEntities();

            IEnumerable<Imovel_Result> imoveis = tcc.ListaImoveis_Distance("POINT"+p.Replace(",",""), distance);
            return Json(imoveis);

            
        }
              
    }
}
