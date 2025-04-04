using AgendaRoom.DALs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AgendaRoom.API.DTOs;
using AgendaRoom.Domain.Entities;
using AgendaRoom.Infrastructure.DALs;

namespace AgendaRoom.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDAL _userDal;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserController(UserDAL userDal)
        {
            _userDal = userDal;
            _passwordHasher = new PasswordHasher<User>();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userDal.GetAllUsers();
            return Ok(users);
        }
        
        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userDal.GetUserById(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }
        
        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO model)
        {
            var user = await _userDal.GetUserById(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            user.name = model.Name ?? user.name;
            user.email = model.Email ?? user.email;

            if (!string.IsNullOrEmpty(model.Password))
                user.hashPassword = _passwordHasher.HashPassword(user, model.Password);

            await _userDal.UpdateUser(user);
            return Ok("Usuário atualizado com sucesso.");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userDal.GetUserById(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            await _userDal.DeleteUser(id);
            return Ok("Usuário excluído com sucesso.");
        }
    }
}
