using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Tcc_Imoveis.Models
{
    public partial class tcc_imoveisEntities : ObjectContext
    {
        public ObjectResult<Imovel_Result> ListaImoveis_Geo(string polygon)
        {
            ObjectParameter polygonParameter = new ObjectParameter("ipolygon", polygon);
            
            return base.ExecuteFunction<Imovel_Result>("ListaImoveis_Geo", polygonParameter);
        }

        public ObjectResult<global::System.String> ExibePoligono(int id)
        {
            ObjectParameter idParameter = new ObjectParameter("idp", id);
            return base.ExecuteFunction<global::System.String>("ExibePoligono", idParameter);
        }

       
       
        public int InserePoligono(int idPesquisa, string polygon)
        {
            ObjectParameter pIdPesquisa = new ObjectParameter("pIdPesquisa", idPesquisa);
            ObjectParameter polygonParameter = new ObjectParameter("polygon", polygon);
            return base.ExecuteFunction("InserePoligono", pIdPesquisa, polygonParameter);
        }

        public ObjectResult<GetImovel_Result> get_imovel(int idImovel)
        {
            ObjectParameter idimovel = new ObjectParameter("in_idImovel", idImovel);
            return base.ExecuteFunction<GetImovel_Result>("get_imovel", idimovel);
        }

        public ObjectResult<Imovel_Result> ListaImoveis_Distance(string point, string distance )
        {
            ObjectParameter pointParameter = new ObjectParameter("ponto", point);
            ObjectParameter distanceParameter = new ObjectParameter("distancia", distance);
            return base.ExecuteFunction<Imovel_Result>("ListaImoveis_Distance", pointParameter, distanceParameter);


        }

        public ObjectResult<ImovelAtributo_Result> ListaAtributosImovel(int idImovel)
        {
            ObjectParameter idParameter = new ObjectParameter("pidImovel", idImovel);
            return base.ExecuteFunction<ImovelAtributo_Result>("ListaAtributosImovel", idParameter);
        }

        public ObjectResult<ImovelImagens_Result> ListaImagensImovel(int idImovel)
        {
            ObjectParameter idParameter = new ObjectParameter("pidImovel", idImovel);
            return base.ExecuteFunction<ImovelImagens_Result>("ListaImagensImovel", idParameter);
            
        }

        public ObjectResult<Imovel_Result> ListaImoveisValidos(int idLista)
        {
            ObjectParameter idParameter = new ObjectParameter("pidUsuario_Pesquisa", idLista);
            
            return base.ExecuteFunction<Imovel_Result>("ListaImoveisValidos", idParameter);
        }

        public ObjectResult<Pesquisa_Result> ListaPesquisaUsuario(long idUsuario)
        {
            ObjectParameter idParameter = new ObjectParameter("inIdUsuario", idUsuario);
            return base.ExecuteFunction<Pesquisa_Result>("ListaPesquisaUsuario", idParameter);
        }

        public ObjectResult<Nullable<global::System.Int32>> InserePesquisa(string idUsuario, string nomePesquisa)
        {
            ObjectParameter pUsuario = new ObjectParameter("pUsuario", idUsuario);
            ObjectParameter pNomePesquisa = new ObjectParameter("pNomePesquisa", nomePesquisa);
            return base.ExecuteFunction<Nullable<global::System.Int32>>("InserePesquisa", pUsuario, pNomePesquisa);
        }

        public int InsereAtributoPesquisa(int idPesquisa, string atributoTipo, string condicao, string valor)
        {
            //in pIdPesquisa int, in pidAtributoTipo varchar(2), in pidCondicao varchar(2), in pValor varchar(200))

            ObjectParameter pIdPesquisa = new ObjectParameter("pIdPesquisa", idPesquisa);
            ObjectParameter pidAtributoTipo = new ObjectParameter("pidAtributoTipo", atributoTipo);
            ObjectParameter pidCondicao = new ObjectParameter("pidCondicao", condicao);
            ObjectParameter pValor = new ObjectParameter("pValor", valor);
            return base.ExecuteFunction("InsereAtributoPesquisa",
                pIdPesquisa,
                pidAtributoTipo,
                pidCondicao,
                pValor


                );
        }


        public int InsereImovelNegado(int idPesquisa, int idImovel)
        {
            ObjectParameter pIdPesquisa = new ObjectParameter("pIdPesquisa", idPesquisa);
            ObjectParameter pidImovel = new ObjectParameter("pidImovel", idImovel);
            return base.ExecuteFunction("InsereImovelNegado", pIdPesquisa, pidImovel);
        }


        public int SalvaPesquisa(int idPesquisa, string usuarioId, string nomePesquisa)
        {
            ObjectParameter pIdPesquisa = new ObjectParameter("pIdPesquisa", idPesquisa);
            ObjectParameter pIdUsuario = new ObjectParameter("pIdUsuario", usuarioId);
            ObjectParameter pNomePesquisa = new ObjectParameter("pNomePesquisa", nomePesquisa);

            

            return base.ExecuteFunction("SalvaPesquisa", pIdUsuario, pNomePesquisa, pIdPesquisa);
        }

    }
}