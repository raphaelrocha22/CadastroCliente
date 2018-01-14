using Newtonsoft.Json;
using Projeto.DAL.Persistencia;
using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class CadastroViewModel:GenericClass
    {
        public string Status { get; set; }
        public ClasseCliente Classe { get; set; }
    }
}