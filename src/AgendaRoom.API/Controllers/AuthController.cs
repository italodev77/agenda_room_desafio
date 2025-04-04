using AgendaRoom.DTOs;
using AgendaRoom.Entities;
using AgendaRoom.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AgendaRoom.Config;
using AgendaRoom.Domain.Entities;

namespace AgendaRoom.Controllers
{
    [Route("api/Autenticacao")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(ApiDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (await _context.User.AnyAsync(u => u.email == model.Email))
                return BadRequest("E-mail já está em uso.");

            var user = new User
            {
                name = model.Nome,
                email = model.Email
            };

            
            user.hashPassword = _passwordHasher.HashPassword(user, model.Senha);

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            
            var usuarioSalvo = await _context.Usuarios.FirstOrDefaultAsync(u => u.email == model.Email);
            

            return CreatedAtAction(nameof(Register), new { email = usuario.email }, "Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.email == model.Email);
            
            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.senhaHash, model.Senha);
            
            var token = TokenService.GenerateToken(usuario);
            return Ok(new { token });
        }
    }
}
