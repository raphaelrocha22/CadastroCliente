using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades.Enuns
{
    public enum Guelta
    {
        [Display(Name = "Normal")]
        Normal = 1,

        [Display(Name = "Turbo")]
        Turbo = 2,

        [Display(Name = "Sem Guelta")]
        Sem_Guelta = 3
    }
}
