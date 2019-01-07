using System;
using System.Collections.Generic;
using System.Text;
using pessoa.domain.pessoa;

namespace pessoa.domain.endereco
{
    public class Endereco
    {
        public long Id { get; private set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }

        public Pessoa Pessoa { get; set; }

    }
}
