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
    
    public partial class usuario_lista_imovel
    {
        public int idusuario_lista { get; set; }
        public int idImovel { get; set; }
        public Nullable<int> visualizar { get; set; }
    
        public virtual imovel imovel { get; set; }
        public virtual usuario_lista usuario_lista { get; set; }
    }
}
