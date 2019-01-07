using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pessoa.api.Dto
{
    public class TelefoneDto
    {
        public long Id { get; private set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
    }
}
