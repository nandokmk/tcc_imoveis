//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tcc_Imoveis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class imovel_anuncio
    {
        public int idImovel_Anuncio { get; set; }
        public Nullable<int> idImovel { get; set; }
        public Nullable<System.DateTime> data_inicial { get; set; }
        public Nullable<System.DateTime> data_final { get; set; }
        public byte[] imagem { get; set; }
        public Nullable<int> prioridade { get; set; }
        public string texto { get; set; }
    
        public virtual imovel imovel { get; set; }
    }
}
