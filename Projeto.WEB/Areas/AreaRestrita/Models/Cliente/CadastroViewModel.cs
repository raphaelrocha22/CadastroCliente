using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class CadastroViewModel
    {
        public string classe { get; set; }
        public string representante { get; set; }
        public string cnpj { get; set; }
        public string codun { get; set; }
        public string razaoSocial { get; set; }
    }
}