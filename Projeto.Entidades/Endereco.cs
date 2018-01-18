using Projeto.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Endereco
    {
        public int IdEndereco { get; set; }
        public TipoEndereco Tipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string Cep { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }

        public Endereco()
        {

        }

        public Endereco(TipoEndereco tipo, string logradouro, string numero,
            string complemento, string bairro, string municipio, string uf, string cep,
            string telefone1, string telefone2, string email)
        {
            Tipo = tipo;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Municipio = municipio;
            UF = uf;
            Cep = cep;
            Telefone1 = telefone1;
            Telefone2 = telefone2;
            Email = email;
        }

        public Endereco(int idEndereco, TipoEndereco tipo, string logradouro, string numero,
            string complemento, string bairro, string municipio, string uf, string cep,
            string telefone1, string telefone2, string email)
            : this(tipo, logradouro, numero, complemento, bairro, municipio, uf, cep, telefone1, telefone2, email)
        {
            IdEndereco = idEndereco;
        }
    }
}
