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

namespace pessoa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        readonly PessoaDbContext _dbContext;

        public PessoasController()
        {
            _dbContext = new PessoaDbContext();
        }

        [HttpPost]
        public ActionResult<Pessoa> Post(Pessoa pessoa)
        {           
            _dbContext.Pessoas.Add(pessoa);
            _dbContext.SaveChanges();

            string uri = Url.Action("Post", new { id = pessoa.Id });
            return Created(uri, pessoa);
        }

        [HttpGet]
        public ActionResult<IList<Pessoa>> GetAll()
        {
            return Ok(_dbContext.Pessoas.ToList());
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