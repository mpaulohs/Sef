using System.ComponentModel.DataAnnotations;

namespace Sfe.Application.ViewModels.ControllersGperfil
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required (ErrorMessage = "O campo senha é obrigatório.")]
        [StringLength(100, ErrorMessage = "A {0} deve tem pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }
    }
}
