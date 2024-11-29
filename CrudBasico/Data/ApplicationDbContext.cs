using CrudBasico.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudBasico.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) { }

        public DbSet<Contacto> Contactos { get; set; }
    }
}
