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
    [Route("api/v{version:apiVersion}/instituicao")]
    public class InstituicaoController : MainController
    {
        private readonly IInstituicaoRepository _repository;
        private readonly IMapper _mapper;
        public InstituicaoController(IInstituicaoRepository instituicaoRepository, 
                                     INotificador notificador,
                                     IMapper mapper,
                                     IUsuario usuario) : base(notificador, usuario)
        {
            _repository = instituicaoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<InstituicaoVM>>> Get()
        {
            var instituicao = _mapper.Map<List<InstituicaoVM>>(await _repository.Obter());
            return instituicao;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<InstituicaoVM>> GetById(long id)
        {
            if (id > 0)
            {
                var res = await _repository.ObterPorId(id);
                if (res == null) return NotFound();
                return _mapper.Map<InstituicaoVM>(res);
            }
            else
            {
                return BadRequest(new { message = "Id Inválido" });
            }
        }

        [HttpPost]
        // [ClaimsAuthorize("Instituicao", "Incluir")]
        public async Task<ActionResult<InstituicaoVM>> Post(InstituicaoVM instituicaoVM)
        {

            if(ModelState.IsValid)
            {
                await _repository.Adicionar(_mapper.Map<Instituicao>(instituicaoVM));
                return Ok(instituicaoVM);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        // [ClaimsAuthorize("Instituicao", "Alterar")]
        public async Task<ActionResult<InstituicaoVM>> Put(InstituicaoVM instituicaoVM)
        {
            if(ModelState.IsValid)
            {
                await _repository.Alterar(_mapper.Map<Instituicao>(instituicaoVM));
                return Ok(instituicaoVM);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        //[ClaimsAuthorize("Instituicao", "Excluir")]
        public async Task<ActionResult<int>> Delete(InstituicaoVM instituicaoVM)
        {
            if (instituicaoVM.Id > 0)
            {
                await _repository.Excluir(instituicaoVM.Id);
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Instituição Inválida" });
            }
        }
    }
}