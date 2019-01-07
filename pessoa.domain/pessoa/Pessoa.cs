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
    }
}
