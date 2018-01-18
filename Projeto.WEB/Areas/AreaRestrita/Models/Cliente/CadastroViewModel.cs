using Projeto.Entidades.Enum;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class CadastroViewModel:GenericClass
    {
        /// <summary>
        /// Status da requisicao do CNPJ (OK ou ERRO)
        /// </summary>
        public string Status { get; set; }
        public ClasseCliente Classe { get; set; }
    }
}