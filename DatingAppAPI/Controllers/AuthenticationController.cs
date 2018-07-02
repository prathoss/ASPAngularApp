using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingAppAPI.DTOs;
using DatingAppAPI.Models;
using DatingAppAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _repository;

        public AuthenticationController(IAuthenticationRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDTO userForRegisterDTO)
        {
            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();
            if (await _repository.UserExists(userForRegisterDTO.Username)) ModelState.AddModelError("Username", "User already exists.");

            //validate request
            if (!ModelState.IsValid) return BadRequest(ModelState);

            User userToCreate = new User
            {
                Name = userForRegisterDTO.Username
            };

            var createdUser = await _repository.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDTO userForLoginDTO)
        {

        }
    }
}