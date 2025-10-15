using System.ComponentModel.DataAnnotations;

namespace Backend_login.Models
{
    public class Usuario
    {
        [Key] // <- Isso indica que é a chave primária
        public int Id_Usuario { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Endereco_Email { get; set; } = string.Empty;
        public string? Imagem { get; set; }
    }
}
