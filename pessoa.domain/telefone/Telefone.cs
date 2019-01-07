using System;
using System.Collections.Generic;
using System.Text;
using pessoa.domain.pessoa;

namespace pessoa.domain.telefone
{
    public class Telefone
    {
        public long Id { get; private set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
        
        public Pessoa Pessoa { get; set; }
    }
}
