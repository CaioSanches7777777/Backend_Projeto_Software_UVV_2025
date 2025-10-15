using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_login.Models
{
    [Table("paginas", Schema = "public")]
    public class Pagina
    {
        [Key, Column("id_pagina")]
        public int IdPagina { get; set; }

        [Column("id_wiki")]
        [Required]
        public int IdWiki { get; set; }

        [Column("nsfw_flag")]
        [Required]
        public bool NsfwFlag { get; set; }

        [Column("conteudo")]
        [Required]
        public string Conteudo { get; set; } = string.Empty;

        [Column("is_main")]
        [Required]
        public bool IsMain { get; set; }
    }
}