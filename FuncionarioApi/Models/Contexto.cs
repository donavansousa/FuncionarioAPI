namespace FuncionarioApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
        }

        public virtual DbSet<Funcionario> Funcionario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Departamento)
                .IsUnicode(false);
        }
    }
}
