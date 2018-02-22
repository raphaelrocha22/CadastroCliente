using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades.Enuns
{
    public enum Campanha
    {
        ClubR = 1,
        Upgrade = 2,
        MarkUP = 3,

        [Display(Name = "Sem Campanha")]
        SemCampanha = 4
    }
}
