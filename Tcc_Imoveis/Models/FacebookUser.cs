using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tcc_Imoveis.Models
{
    public class FacebookUser
    {
        public long FacebookId { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }


        public string Name { get; set; }
    }
}