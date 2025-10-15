namespace Backend_login.Models
{
    public class Wiki
    {
        public int IdWiki { get; set; }           // id_wiki
        public string Nome { get; set; } = null!; // nome
        public string? Descricao { get; set; }    // descricao (pode ser nulo)
        public string EnderecoWeb { get; set; } = null!; // endereco_web
        public int IdTag { get; set; }            // id_tag

        // 🔗 Relação 1:N -> Uma Wiki tem várias páginas
        //public ICollection<Pagina>? Paginas { get; set; }
    }
}
