using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfe.Application.Commands;
using Sfe.Application.Extensions;
using Sfe.Domain.AggregatesModel.UserAggregate;

namespace Sfe.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        public UsersController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var usuario = await _userRepository.ListAllAsync();            
            return await _userRepository.ListAllAsync();          
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserCommand user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(new CreateUserCommand(user.Name, user.Email));
            
            return CreatedAtAction("GetUser", new { id = result.Result }, user);
        }

    }
}
