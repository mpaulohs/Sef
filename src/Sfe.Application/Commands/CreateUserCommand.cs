using Sfe.Application.Behaviors;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Sfe.Application.Commands
{
    public class CreateUserCommand : IRequest<Response>
    {
        [Required(ErrorMessage ="Campo Obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Email { get; set; }

        public CreateUserCommand() { }
        public CreateUserCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
