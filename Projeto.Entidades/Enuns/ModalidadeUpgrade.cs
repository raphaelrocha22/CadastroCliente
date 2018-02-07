using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades.Enuns
{
    public enum ModalidadeUpgrade
    {
        [Display(Name = "Modalidade 1")]
        Modalidade1 = 1,

        [Display(Name = "Modalidade 2")]
        Modalidade2 = 2,

        [Display(Name = "Modalidade 3")]
        Modalidade3 = 3
    }
}
