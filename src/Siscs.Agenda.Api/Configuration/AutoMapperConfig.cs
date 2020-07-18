using AutoMapper;
using Siscs.Agenda.Api.ViewModels;
using Siscs.Agenda.Business.Entities;

namespace Siscs.Agenda.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<Categoria, CategoriaVM>().ReverseMap();
            CreateMap<Instituicao, InstituicaoVM>().ReverseMap();
            CreateMap<Curso, CursoVM>().ReverseMap();
        }
        
    }
}