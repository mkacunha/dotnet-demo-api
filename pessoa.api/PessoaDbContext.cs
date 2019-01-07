using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using pessoa.domain.endereco;
using pessoa.domain.pessoa;
using pessoa.domain.telefone;

namespace pessoa.api
{
    public partial class PessoaDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        public PessoaDbContext()
        {
        }

        public PessoaDbContext(DbContextOptions<PessoaDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=192.168.99.100;User Id=pessoa;Password=1234;Database=pessoa-db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}
