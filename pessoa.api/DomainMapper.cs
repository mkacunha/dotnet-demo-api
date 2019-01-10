using AutoMapper;
using pessoa.domain.pessoa;
using pessoa.domain.endereco;
using pessoa.domain.telefone;
using pessoa.api.Dto;

namespace pessoa.api
{
    public class DomainMapper : Profile
    {
        public DomainMapper()
        {
            CreateMap<Pessoa, PessoaDto>();
            CreateMap<PessoaDto, Pessoa>();
            CreateMap<Endereco, EnderecoDto>();
            CreateMap<EnderecoDto, Endereco>();
            CreateMap<Telefone, TelefoneDto>();
            CreateMap<TelefoneDto, Telefone>();
        }
    }
}