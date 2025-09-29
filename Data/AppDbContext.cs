using Backend_login.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Backend_login.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Pagina> Pages { get; set; }      //para entidades de pagina

        public DbSet<Usuario> Users { get; set; }   //para entidades de usuario
    }
}
