using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pessoa.domain.endereco;
using pessoa.domain.pessoa;
using pessoa.domain.telefone;
using AutoMapper;
using pessoa.service.pessoa;
using pessoa.dto;

namespace pessoa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        PessoaService _pessoaService;
        public PessoasController()
        {
            _pessoaService = new PessoaService();
        }

        [HttpPost]
        public ActionResult<PessoaDto> Post(PessoaDto pessoaDto)
        {
            var pessoaSaved = _pessoaService.Save(pessoaDto);
            string uri = Url.Action("Post", new { id = pessoaSaved.Id });
            return Created(uri, pessoaSaved);
        }

        [HttpGet]
        public ActionResult<IList<PessoaDto>> GetAll()
        {
            return Ok(_pessoaService.findAll());
        }

        [HttpGet]
        [Route("sem-endereco")]
        public ActionResult GetPessoasSemEndereco()
        {
            return Ok(_pessoaService.findAll());
        }

        [HttpGet]
        [Route("sem-telefone")]
        public ActionResult GetPessoasSemTelefone()
        {
            return Ok(_pessoaService.findByPredicate(p => p.Telefones.Count() == 0));
        }

        [HttpGet]
        [Route("completa")]
        public ActionResult GetPessoasComEnderecosETelefones()
        {
            return Ok(_pessoaService.findAllWithEnderecoAndTelefone());
        }

        [HttpGet("{id}")]
        public ActionResult<IList<Pessoa>> GetOne(int Id)
        {
            return Ok(_pessoaService.findByPredicate(pessoa => pessoa.Id == Id));
        }

        [HttpPut("{Id}")]
        public ActionResult<Pessoa> Put(int Id, [FromBody] Pessoa Pessoa)
        {
            var pessoaUpdated = _pessoaService.Update(Id, Pessoa);

            if (pessoaUpdated != null)
            {
                return Ok(pessoaUpdated);
            }

            return BadRequest("Pessoa de ID " + Id + " não existe no banco de dados");
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            if (_pessoaService.Delete(Id))
            {
                return Ok();
            }
            return BadRequest("Pessoa de ID " + Id + " não existe no banco de dados");
        }




    }
}