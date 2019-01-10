using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pessoa.api.Dto;
using pessoa.domain.endereco;
using pessoa.domain.pessoa;
using pessoa.domain.telefone;
using AutoMapper;

namespace pessoa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        readonly PessoaDbContext _dbContext;
        readonly IMapper _mapper;

        public PessoasController(IMapper mapper)
        {
            _dbContext = new PessoaDbContext();
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<PessoaDto> Post(PessoaDto pessoaDto)
        {           
            var pessoaEntity =_mapper.Map<Pessoa>(pessoaDto);
            _dbContext.Pessoas.Add(pessoaEntity);
            _dbContext.SaveChanges();
            string uri = Url.Action("Post", new { id = pessoaEntity.Id });
            return Created(uri, _mapper.Map<PessoaDto>(pessoaEntity));
        }

        [HttpGet]
        public ActionResult<IList<Pessoa>> GetAll()
        {
            return Ok(_dbContext.Pessoas.ToList());
        }

        [HttpGet]
        [Route("sem-endereco")]
        public ActionResult GetPessoasSemEndereco() {
            var pessoas = _dbContext.Pessoas.Where(p => p.Enderecos.Count() == 0);

            return Ok(pessoas);
        }

        [HttpGet]
        [Route("sem-telefone")]
        public ActionResult GetPessoasSemTelefone() {
            var pessoas = _dbContext.Pessoas.Where(p => p.Telefones.Count() == 0);
            return Ok(pessoas);
        }

        [HttpGet]
        [Route("completa")]
        public ActionResult GetPessoasComEnderecosETelefones() {
            var pessoas = from pessoa in _dbContext.Pessoas 
                join endereco in _dbContext.Enderecos on pessoa equals endereco.Pessoa
                join telefone in _dbContext.Telefones on pessoa equals telefone.Pessoa
                select new { pessoa.Nome, pessoa.Enderecos, pessoa.Telefones }; 
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public ActionResult<IList<Pessoa>> GetOne(int id)
        {
            return Ok(_dbContext.Pessoas.Where(pessoa => pessoa.Id == id));
        }

        [HttpPut("{id}")]
        public ActionResult<Pessoa> Put(int id, [FromBody] Pessoa people)
        {
            IQueryable<Pessoa> result = _dbContext.Pessoas.Where(p => p.Id == id);

            if (result.Count() > 0)
            {
                Pessoa pessoaFromDatabase = result.First();
                pessoaFromDatabase.Nome = people.Nome;
                _dbContext.Update(pessoaFromDatabase);
                _dbContext.SaveChanges();
                return Ok(pessoaFromDatabase);
            }

            return BadRequest("Pessoa de ID " + id + " não existe no banco de dados");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            IQueryable<Pessoa> result = _dbContext.Pessoas.Where(p => p.Id == id);

            if (result.Count() > 0)
            {
                _dbContext.Remove(result.First());
                _dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest("Pessoa de ID " + id + " não existe no banco de dados");
        }


        

    }
}