using LabMedico.Models.CustomUser;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LabMedico.Models
{
    public class LaboratorioDbContext : IdentityDbContext<LaboratorioUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public LaboratorioDbContext()
            : base("MedicoDataBase")
        {
        }

        public static LaboratorioDbContext Create()
        {
            return new LaboratorioDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cita>().ToTable("Citas");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Estudio>().ToTable("Estudios");
            modelBuilder.Entity<Historico>().ToTable("Historicos");
            modelBuilder.Entity<Sucursal>().ToTable("Sucursales");
            modelBuilder.Entity<Tecnico>().ToTable("Tecnicos");
            modelBuilder.Entity<Zona>().ToTable("Zonas");
        }

        public DbSet<Analisis> Analisis { get; set; }
        public DbSet<AnalisisSucursal> AnalisisSucursals { get; set; }
        public DbSet<Sucursal> Sucursals { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicoes { get; set; }
        public DbSet<Estudio> Estudios { get; set; }
        public DbSet<Historico> Historicoes { get; set; }
        public DbSet<Zona> Zonas { get; set; }
    }
}