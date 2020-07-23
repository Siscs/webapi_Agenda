using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Siscs.Agenda.Api.Services;
using Siscs.Agenda.Api.ViewModels;
using Siscs.Agenda.Business.Entities;
using Siscs.Agenda.Business.Interfaces;

namespace Siscs.Agenda.Api.Controllers
{
    [Authorize]
    [Route("api/v1/categoria")]
    public class CategoriaController : MainController
    {
        private readonly ICategoriaRepository _repository;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository CategoriaRepository,
                                   INotificador notificador,
                                   ICategoriaService categoriaService,
                                   IMapper mapper) : base(notificador)
                                   
        {
            _repository = CategoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        [ClaimsAuthorize("Categoria", "Consultar")]
        public async Task<ActionResult<List<CategoriaVM>>> Get()
        {
            var categorias = _mapper.Map<List<CategoriaVM>>(await _repository.Obter());
            return categorias;
        }

        [HttpGet("{id:int}")]
        [ClaimsAuthorize("Categoria", "Consultar")]
        public async Task<ActionResult<CategoriaVM>> GetById(long id)
        {
            
            if(id <= 0)
            {
                NotificarErro("Id inválido");
                return CustomResponse();
            }

            var res = await _repository.ObterPorId(id);
            if(res == null) return NotFound();
            return _mapper.Map<CategoriaVM>(res);

        }

        [HttpPost]
        [ClaimsAuthorize("Categoria", "Incluir")]
        public async Task<ActionResult<CategoriaVM>> Post(CategoriaVM categoriaVM)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoriaService.Adicionar(_mapper.Map<Categoria>(categoriaVM));

            return CustomResponse(categoriaVM);
            
        }

        [HttpPut]
        [ClaimsAuthorize("Categoria", "Alterar")]
        public async Task<ActionResult<CategoriaVM>> Put(CategoriaVM categoriaVM)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoriaService.Alterar(_mapper.Map<Categoria>(categoriaVM));

            return CustomResponse(categoriaVM);
            
        }

        [HttpDelete]
        [ClaimsAuthorize("Categoria", "Excluir")]
        public async Task<ActionResult> Delete(CategoriaVM categoria)
        {
            if(categoria.Id <= 0)
            {
                NotificarErro("Id da categoria inválida.");
                return CustomResponse();
            }

            await _categoriaService.Excluir(categoria.Id);
            return CustomResponse();

        }
    }
}