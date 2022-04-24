using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations;

using lowtunning.Models;
 
using System.Drawing;
 
using System.Net;
using System.Web.Mvc;

namespace lowtunning.Models
{
    public class Produtosm
    {
        public string cod_prod { get; set; }

        public string nome_prod { get; set; }

        public string marca_prod { get; set; }

        public string valor_prod { get; set; }

        public string descricaotipo { get; set; }

        public string idtipo { get; set; }
       
        public string qtd { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase caminhoimagem { get; set; }

        public string imagem { get; set; }

        public string cd_venda { get; set; }

        public string prodvenda { get; set; }

        public string data { get; set; }

        public string usuario { get; set; }






    }
}