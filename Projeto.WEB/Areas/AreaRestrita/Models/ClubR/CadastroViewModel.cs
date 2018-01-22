using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Projeto.WEB.Areas.AreaRestrita.Models.ClubR
{
    public class CadastroViewModel:GenericClass
    {
        public HttpPostedFileBase Contrato { get; set; }
        public string Obervacao { get; set; }
    }
}