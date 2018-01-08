using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Cliente
    {
        public int id { get; set; }
        public int codCliente { get; set; }
        public int codun { get; set; }
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string cpfCnpj { get; set; }
        public string inscricaoEstadual { get; set; }
        public string inscricaoMunicipal { get; set; }
        public TipoCliente tipoCliente { get; set; }
        public ModalidadeCliente modalidade { get; set; }
        public Endereco enderecoNotaFiscal { get; set; }
        public Endereco enderecoCobranca { get; set; }
        public Endereco enderecoEntregaLentes { get; set; }
        public Endereco enderecoEntregaArmacao { get; set; }
        public Representante representante { get; set; }

    }
}
