using Backend_login.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_login.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Users { get; set; }
        public DbSet<Pagina> Pages { get; set; }
        public DbSet<Wiki> Wikis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Configuração da tabela de usuários
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario", "public");
                entity.HasKey(u => u.Id_Usuario);

                entity.Property(u => u.Id_Usuario).HasColumnName("id_usuario");
                entity.Property(u => u.Nome).HasColumnName("nome");
                entity.Property(u => u.Senha).HasColumnName("senha");
                entity.Property(u => u.Endereco_Email).HasColumnName("endereco_email");
                entity.Property(u => u.Imagem).HasColumnName("imagem");
            });

            // 🔹 Configuração da tabela de páginas
            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.ToTable("paginas", "public");
                entity.HasKey(p => new { p.IdPagina, p.IdWiki });

                entity.Property(p => p.IdPagina).HasColumnName("id_pagina");
                entity.Property(p => p.IdWiki).HasColumnName("id_wiki");
                entity.Property(p => p.NsfwFlag).HasColumnName("nsfw_flag").IsRequired();
                entity.Property(p => p.Conteudo).HasColumnName("conteudo").IsRequired();
                entity.Property(p => p.IsMain).HasColumnName("is_main").IsRequired();

                /*
                // 🔗 Relação: uma página pertence a uma wiki
                entity.HasOne<Wiki>()
                      .WithMany(w => w.Paginas)
                      .HasForeignKey(p => p.IdWiki)
                      .OnDelete(DeleteBehavior.Cascade);
                */
            });

            // 🔹 Configuração da tabela de wikis
            modelBuilder.Entity<Wiki>(entity =>
            {
                entity.ToTable("wiki", "public");
                entity.HasKey(w => w.IdWiki);

                entity.Property(w => w.IdWiki).HasColumnName("id_wiki");
                entity.Property(w => w.Nome).HasColumnName("nome").HasMaxLength(100).IsRequired();
                entity.Property(w => w.Descricao).HasColumnName("descricao").HasMaxLength(600);
                entity.Property(w => w.EnderecoWeb).HasColumnName("endereco_web").HasMaxLength(100).IsRequired();
                entity.Property(w => w.IdTag).HasColumnName("id_tag").IsRequired();
            });
        }
    }
}
