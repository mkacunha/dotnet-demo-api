using System;
using System.Collections.Generic;
using System.Text;
using pessoa.domain.endereco;
using pessoa.domain.telefone;

namespace pessoa.domain.pessoa
{
    public class Pessoa
    {
        public int Id { get; private set; }
        public string Nome { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Telefone> Telefones { get; set; }

        public Pessoa()
        {
        }

        public Pessoa(int Id, string Nome, ICollection<Endereco> Enderecos, ICollection<Telefone> Telefones)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Enderecos = Enderecos;
            this.Telefones = Telefones;
        }
    }
}
