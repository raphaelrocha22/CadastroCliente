using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Areas.AreaRestrita.Models.Users
{
    public class AlterarSenhaViewModel
    {
        public int idUsuario { get; set; }

        [RegularExpression("^[a-z0-9@&]{4,50}$", ErrorMessage = "Senha inválida")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Nova senha")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
        [Required(ErrorMessage = "Por favor, confirme sua senha")]
        [Display(Name = "Confirmar a nova senha")]
        public string SenhaConfirm { get; set; }
    }
}