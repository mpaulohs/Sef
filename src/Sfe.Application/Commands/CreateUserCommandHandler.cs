using Sfe.Application.Behaviors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Sfe.Domain.AggregatesModel.UserAggregate;

namespace Sfe.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository autorRepository)
        {
            _userRepository = autorRepository;
        }

        public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {   
            var result =  await _userRepository.CreateAsync(request.Name, request.Email);            
            return new Response(result.user.Id);
        }
    }   
}
