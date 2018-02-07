using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades.Enuns
{
    public enum ModalidadeMarkup
    {
        [Display(Name = "Markup 4.3")]
        MarkUp4_3 = 1,

        [Display(Name = "Markup 4.8")]
        MarkUp4_8 = 2,

        [Display(Name = "Markup 5")]
        MarkUp5 = 3,
    }
}
