using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AgendaRoom.API.DTOs;
using AgendaRoom.Config;
using AgendaRoom.Domain.Entities;
using AgendaRoom.Infrastructure.Service;

namespace AgendaRoom.Controllers
{
    [Route("api/Autenticacao")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly TokenService _tokenService;
        public AuthController(ApiDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (await _context.User.AnyAsync(u => u.email == model.Email))
                return BadRequest("E-mail já está em uso.");

            var user = new User
            {
                name = model.Name,
                email = model.Email
            };

            user.hashPassword = _passwordHasher.HashPassword(user, model.Password);
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var newUser = await _context.User.FirstOrDefaultAsync(u => u.email == model.Email);
            return CreatedAtAction(nameof(Register), new { email = newUser.email }, "Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.email == model.Email);
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.hashPassword, model.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Senha incorreta.");
            
            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
