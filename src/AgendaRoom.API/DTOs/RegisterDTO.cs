using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace AgendaRoom.DTOs;

public class RegisterDTO
{
    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;

    
    public string SenhaHash => ComputeHash(Senha);

    private string ComputeHash(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
