using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.WEB.Models.Home
{
    public class UpdateViewModel
    {
        [RegularExpression("^[a-z0-9]{4,50}$", ErrorMessage = "Login inválido")]
        [Required(ErrorMessage = "Por favor, informe o Login do usuário.")]
        [Display(Name = "Login")]
        public string login { get; set; }

        [RegularExpression("^[a-z0-9@&]{4,50}$", ErrorMessage = "Senha inválida")]
        [Required(ErrorMessage = "Por favor, informe a Senha Antiga.")]
        [Display(Name = "Senha Antiga")]
        public string senhaAntiga { get; set; }

        [RegularExpression("^[a-z0-9@&]{4,50}$", ErrorMessage = "Senha inválida")]
        [Required(ErrorMessage = "Por favor, informe a Senha do usuário.")]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Compare("senha", ErrorMessage = "Senhas não conferem")]
        [Required(ErrorMessage = "Por favor, confirme sua senha")]
        [Display(Name = "Confirmar senha")]
        public string senhaConfirm { get; set; }

        
    }
}