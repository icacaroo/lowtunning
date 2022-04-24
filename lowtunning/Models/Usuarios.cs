using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lowtunning.Models
{
    public class Usuarios
    {
        public string cod_login { get; set; }

        public string usuario { get; set; }

        public string Senha { get; set; }

        public string email { get; set; }

        public string telefone { get; set; }

        public string cpf { get; set; }

        public string endereço { get; set; }

        public int status { get; set; }
    }
}