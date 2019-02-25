using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfe.Application.Commands;
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
            var result = await _mediator.Send(user);                              
            return CreatedAtAction("GetUser", new { id = result.Result }, user);
        }

    }
}
