using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Siscs.Agenda.Api.Controllers;
using Siscs.Agenda.Api.ViewModels;
using Siscs.Agenda.Business.Entities;
using Siscs.Agenda.Business.Interfaces;

namespace Siscs.Agenda.Api.V1.Controllers
{
    // [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/curso")]
    public class CursoController : MainController
    {
        private readonly ICursoRepository _repository;
        private readonly IMapper _mapper;
        public CursoController(ICursoRepository CursoRepository, 
                               INotificador notificador,
                               IMapper mapper,
                               IUsuario usuario) : base(notificador, usuario)
        {
            _repository = CursoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CursoVM>>> Get()
        {
            return _mapper.Map<List<CursoVM>>(await _repository.Obter());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CursoVM>> GetById(long id)
        {
            if(id > 0)
            {
                var res = await _repository.ObterPorId(id);
                if(res == null) return NotFound();
                return _mapper.Map<CursoVM>(res);
            } else {
                return BadRequest(new { message = "Id Inválido"});
            }
        }

        [HttpPost]
        // [ClaimsAuthorize("Curso", "Incluir")]
        public async Task<ActionResult<CursoVM>> Post(CursoVM cursoVM)
        {
            if(ModelState.IsValid)
            {
                await _repository.Adicionar(_mapper.Map<Curso>(cursoVM));
                return Ok(cursoVM);
            } else {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        // [ClaimsAuthorize("Curso", "Alterar")]
        public async Task<ActionResult<CursoVM>> Put(CursoVM cursoVM)
        {
            if(ModelState.IsValid)
            {
                await _repository.Alterar(_mapper.Map<Curso>(cursoVM));
                return Ok(cursoVM);
            } else {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        // [ClaimsAuthorize("Curso", "Excluir")]
        public async Task<ActionResult> Delete(CursoVM curso)
        {
            if(curso.Id > 0)
            {
                await _repository.Excluir(curso.Id);
                return Ok();

            } else {
                return BadRequest(new { message = "Curso Inválido"});
            }
        }
    }
}