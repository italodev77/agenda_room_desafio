using AgendaRoom.DTOs;
using AgendaRoom.Entities;
using AgendaRoom.DALs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AgendaRoom.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsuarioDAL _usuarioDal;
        private readonly PasswordHasher<Usuarios> _passwordHasher;

        public UserController(UsuarioDAL usuarioDal)
        {
            _usuarioDal = usuarioDal;
            _passwordHasher = new PasswordHasher<Usuarios>();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usuarioDal.GetAllUsers();
            return Ok(users);
        }
        
        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _usuarioDal.GetUserById(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }
        
        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO model)
        {
            var user = await _usuarioDal.GetUserById(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            user.nome = model.Nome ?? user.nome;
            user.email = model.Email ?? user.email;

            if (!string.IsNullOrEmpty(model.Senha))
                user.senhaHash = _passwordHasher.HashPassword(user, model.Senha);

            await _usuarioDal.UpdateUser(user);
            return Ok("Usuário atualizado com sucesso.");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _usuarioDal.GetUserById(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            await _usuarioDal.DeleteUser(id);
            return Ok("Usuário excluído com sucesso.");
        }
    }
}
