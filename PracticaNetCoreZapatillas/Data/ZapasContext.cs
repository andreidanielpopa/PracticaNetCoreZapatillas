using Microsoft.EntityFrameworkCore;
using PracticaNetCoreZapatillas.Models;

namespace PracticaNetCoreZapatillas.Data
{
    public class ZapasContext : DbContext
    {
        public ZapasContext(DbContextOptions<ZapasContext> options) : base(options) { }

        public DbSet<Zapatilla> Zapatillas { get; set; }

        public DbSet<ImagenesZapatilla> ImagenesZapatillas { get; set; }
    }
}
