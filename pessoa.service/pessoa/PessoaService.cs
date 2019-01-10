using System;
using System.Collections.Generic;
using System.Text;
using pessoa.api;
using pessoa.domain.pessoa;

namespace pessoa.service.pessoa
{
    public class PessoaService
    {
        private PessoaDbContext _dbContext;

        PessoaService()
        {
            _dbContext = new PessoaDbContext();
        }

        public Pessoa Save(Pessoa Pessoa)
        {            
            _dbContext.Add(Pessoa);
            _dbContext.SaveChanges();
            return Pessoa;
        }
    }
}
