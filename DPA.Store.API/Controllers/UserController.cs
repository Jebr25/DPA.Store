using DPA.Store.DOMAIN.Core.Entities;
using DPA.Store.DOMAIN.Core.Interfaces;
using DPA.Store.DOMAIN.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Sign In")]
        public async Task<IActionResult> SignIn(User user)
        {
            var isValid = await _userRepository.ValidateUser(user.Email, user.Password);

            if (isValid)
            {
                return Ok(new { message = "Inicio de sesión exitoso" });
            }

            return BadRequest(new { message = "Credenciales incorrectas" });
        }

        [HttpPost("Sign Up")]
        public async Task<IActionResult> SignUp(User user)
        {
            var existingUser = await _userRepository.FindByEmail(user.Email);

            if (existingUser != null)
            {
                return BadRequest(new { message = "El usuario ya existe" });
            }

            var result = await _userRepository.Insert(user);

            if (result)
            {
                return Ok(new { message = "Usuario registrado con éxito" });
            }

            return BadRequest(new { message = "Error al registrar el usuario" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _userRepository.DeleteById(id);
            if (!result)
                return BadRequest(new { message = "Usuario no encontrado" });
            else
                return Ok(new { message = "Usuario eliminado con éxito" });
        }

    }
}
