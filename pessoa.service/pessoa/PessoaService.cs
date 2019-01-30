using System;
using System.Collections.Generic;
using System.Text;
using pessoa.api;
using pessoa.domain.pessoa;
using pessoa.domain.endereco;
using pessoa.domain.telefone;
using AutoMapper;
using pessoa.dto;
using System.Linq;

namespace pessoa.service.pessoa
{
    public class PessoaService
    {
        private PessoaDbContext _dbContext;
        public PessoaService()
        {
            _dbContext = new PessoaDbContext();
        }

        public PessoaDto Save(PessoaDto Dto)
        {
            var pessoa = new Pessoa();
            pessoa.Nome = Dto.Nome;
            pessoa.Telefones = TelefoneDtoToEntity(Dto.Telefones);
            _dbContext.Add(pessoa);
            _dbContext.SaveChanges();
            Dto.Id = pessoa.Id;
            return Dto;
        }

        public IList<Pessoa> findAll()
        {
            return _dbContext.Pessoas.ToList();
        }

        public List<Pessoa> findByPredicate(Func<Pessoa, bool> predicate)
        {
            return _dbContext.Pessoas.Where(predicate).ToList();
        }

        public List<Pessoa> findAllWithEnderecoAndTelefone()
        {
            var pessoas = from pessoa in _dbContext.Pessoas
                          join endereco in _dbContext.Enderecos on pessoa equals endereco.Pessoa
                          join telefone in _dbContext.Telefones on pessoa equals telefone.Pessoa
                          select new Pessoa(pessoa.Id, pessoa.Nome, pessoa.Enderecos, pessoa.Telefones);
            return pessoas.ToList();
        }

        public Pessoa Update(int Id, Pessoa Pessoa)
        {
            IQueryable<Pessoa> result = _dbContext.Pessoas.Where(p => p.Id == Id);

            if (result.Count() > 0)
            {
                Pessoa pessoaFromDatabase = result.First();
                pessoaFromDatabase.Nome = Pessoa.Nome;
                _dbContext.Update(pessoaFromDatabase);
                _dbContext.SaveChanges();
                return pessoaFromDatabase;
            }

            return null;
        }

        public bool Delete(int Id)
        {
            IQueryable<Pessoa> result = _dbContext.Pessoas.Where(p => p.Id == Id);

            if (result.Count() > 0)
            {
                _dbContext.Remove(result.First());
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }
        private ICollection<Telefone> TelefoneDtoToEntity(ICollection<TelefoneDto> Dtos)
        {
            ICollection<Telefone> Telefones = new List<Telefone>();

            foreach (TelefoneDto Dto in Dtos)
            {
                var telefone = new Telefone();
                telefone.Ddd = Dto.Ddd;
                telefone.Numero = Dto.Numero;
                Telefones.Add(telefone);
            }

            return Telefones;
        }

        private ICollection<Endereco> EnderecosDtoToEntity(ICollection<EnderecoDto> dtos)
        {
            ICollection<Endereco> enderecos = new List<Endereco>();

            foreach (EnderecoDto dto in dtos)
            {
                var endereco = new Endereco();
                endereco.Logradouro = dto.Logradouro;
                enderecos.Add(endereco);
            }

            return enderecos;
        }
    }
}
