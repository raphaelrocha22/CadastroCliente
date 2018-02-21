using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Users
{
    public class ConsultaViewModel
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public StatusUsuario Status { get; set; }
    }
}