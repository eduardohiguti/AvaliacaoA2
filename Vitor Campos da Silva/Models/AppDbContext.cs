namespace Vitor_Campos_da_Silva.Models;

using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Folha> Folhas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=EduardoHiguti_VitorCampos.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Folha>()
            .HasOne<Funcionario>()
            .WithMany(f => f.Folhas)
            .HasForeignKey(f => f.FuncionarioId);
    }

}