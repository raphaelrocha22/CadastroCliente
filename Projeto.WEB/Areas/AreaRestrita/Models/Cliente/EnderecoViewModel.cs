using Projeto.Entidades.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class EnderecoViewModel
    {
        public int IdEndereco { get; set; }

        public TipoEndereco Tipo { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Logradouro { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Numero { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo {1} caracteres")]
        public string Complemento { get; set; }

        [MaxLength(25, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Bairro { get; set; }

        [MaxLength(30, ErrorMessage = "Máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Municipio { get; set; }

        [RegularExpression("^[a-zA-Z]{2}$", ErrorMessage = "Formato inválido, informe a sigla com 2 caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string UF { get; set; }

        [RegularExpression("^[0-9.-]{8,10}$", ErrorMessage = "Formato inválido, informe apenas números com 8 caracteres")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cep { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [RegularExpression("^[0-9\\s()-]{2,20}$", ErrorMessage = "Formato inválido, informe apenas números com até 11 caracteres")]
        public string Telefone1 { get; set; }

        [RegularExpression("^[0-9\\s()-]{2,20}$", ErrorMessage = "Formato inválido, informe apenas números com até 11 caracteres")]
        public string Telefone2 { get; set; }       

        //string por causa da exibição na tela via Jquery
        public string DataCadastro { get; set; }
        

        public EnderecoViewModel()
        {

        }

        public EnderecoViewModel(EnderecoViewModel endereco)
        {
            Tipo = endereco.Tipo;
            Logradouro = endereco.Logradouro;
            Numero = endereco.Numero;
            Complemento = endereco.Complemento;
            Bairro = endereco.Bairro;
            Municipio = endereco.Municipio;
            UF = endereco.UF;
            Cep = endereco.Cep;
            Email = endereco.Email;
            Telefone1 = endereco.Telefone1;
            Telefone2 = endereco.Telefone2;
            DataCadastro = endereco.DataCadastro;
        }
        public EnderecoViewModel(EnderecoViewModel endereco, int idEndereco)
            :this(endereco)
        {
            IdEndereco = idEndereco;
        }
    }
}