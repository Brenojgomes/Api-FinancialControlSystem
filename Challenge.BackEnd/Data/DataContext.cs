using Challenge.BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Challenge.BackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receita>()
                .Property(a => a.Descricao)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<Receita>()
                .Property(a => a.valor)
                .IsRequired();

            modelBuilder.Entity<Receita>()
                .Property(a => a.Data)
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<Despesa>()
                .Property(a => a.Descricao)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<Despesa>()
                .Property(a => a.valor)
                .IsRequired();

            modelBuilder.Entity<Despesa>()
                .Property(a => a.Data)
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<Despesa>()
                .Property(a => a.Categoria)
                .HasConversion(
                p => p.ToString(),
                p => (Categoria)Enum.Parse(typeof(Categoria), p));
        }
       
    }
}
