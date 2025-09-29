using System.ComponentModel.DataAnnotations;

namespace Backend_login.Models
{
    public class Pagina
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        // Relacionamento para hierarquia
        public int? ParentPageId { get; set; }
        public Pagina? ParentPage { get; set; }

        public ICollection<Pagina>? SubPages { get; set; }

        // Autor da página
        public int UserId { get; set; }
        public Usuario User { get; set; }
    }

}