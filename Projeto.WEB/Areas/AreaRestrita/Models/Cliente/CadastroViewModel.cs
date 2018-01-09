using Newtonsoft.Json;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class CadastroViewModel
    {
        public string classe { get; set; }

        public int representante { get; set; }

        public string cnpj { get; set; }

        public int codun { get; set; }

        public string nome { get; set; }

        public string fantasia { get; set; }

        public string inscricaoEstadual { get; set; }

        public string inscricaoMunicipal { get; set; }
        
        public EnderecoViewModel enderecoCadastro { get; set; }

        public EnderecoViewModel enderecoCobranca { get; set; }

        public EnderecoViewModel enderecoEntrega { get; set; }

    }
}