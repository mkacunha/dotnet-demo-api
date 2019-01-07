using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pessoa.api.Dto
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<EnderecoDto> Enderecos { get; set; }
        public ICollection<TelefoneDto> Telefones { get; set; }
    }
}
