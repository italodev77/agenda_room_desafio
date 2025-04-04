using AgendaRoom.DTOs;
using AgendaRoom.Entities;
using AgendaRoom.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AgendaRoom.Config;

namespace AgendaRoom.Controllers
{
    [Route("api/Autenticacao")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly PasswordHasher<Usuarios> _passwordHasher;

        public AuthController(ApiDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuarios>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (await _context.Usuarios.AnyAsync(u => u.email == model.Email))
                return BadRequest("E-mail já está em uso.");

            var usuario = new Usuarios
            {
                nome = model.Nome,
                email = model.Email
            };

            
            usuario.senhaHash = _passwordHasher.HashPassword(usuario, model.Senha);

            _context.Usuarios.Add(usuario);
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
