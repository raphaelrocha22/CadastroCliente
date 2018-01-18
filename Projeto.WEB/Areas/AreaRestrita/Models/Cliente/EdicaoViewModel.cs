using Projeto.Entidades.Enum;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Cliente
{
    public class EdicaoViewModel:GenericClass
    {
        public int IdCliente { get; set; }
        public int CodCliente { get; set; }
        public ClasseCliente Classe { get; set; }
    }
}