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
    // [Authorize]
    [Route("v1/categoria")]
    public class CategoriaController : MainController
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository CategoriaRepository, IMapper mapper)
        {
            _repository = CategoriaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaVM>>> Get()
        {
            var categorias = _mapper.Map<List<CategoriaVM>>(await _repository.Obter());
            return categorias;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaVM>> GetById(long id)
        {
            if(id > 0)
            {
                var res = await _repository.ObterPorId(id);
                if(res == null) return NotFound();
                return _mapper.Map<CategoriaVM>(res);
            } 
            else
            {
                return BadRequest(new { message = "Id Inválido"});
            }
        }

        [HttpPost]
        //[ClaimsAuthorize("Categoria", "Incluir")]
        public async Task<ActionResult<CategoriaVM>> Post(CategoriaVM categoriaVM)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _repository.Adicionar(_mapper.Map<Categoria>(categoriaVM));

            return Ok(categoriaVM);
            
        }

        [HttpPut]
        // [ClaimsAuthorize("Categoria", "Alterar")]
        public async Task<ActionResult<CategoriaVM>> Put(CategoriaVM categoriaVM)
        {
            if(ModelState.IsValid)
            {
                await _repository.Alterar(_mapper.Map<Categoria>(categoriaVM));
                return Ok(categoriaVM);
            } else {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        // [ClaimsAuthorize("Categoria", "Excluir")]
        public async Task<ActionResult> Delete(CategoriaVM categoria)
        {
            if(categoria.Id > 0)
            {
                await _repository.Excluir(categoria.Id);
                return Ok();
            } 
            else
            {
                return BadRequest(new { message = "Categoria Inválida"});
            }
        }
    }
}