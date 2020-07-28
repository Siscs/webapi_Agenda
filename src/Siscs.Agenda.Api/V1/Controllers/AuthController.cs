using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Siscs.Agenda.Api.Services;
using Siscs.Agenda.Api.ViewModels;
using Siscs.Agenda.Business.Interfaces;

namespace Siscs.Agenda.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    // [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : MainController
    {

        private readonly SignInManager<IdentityUser> _signInmanager;
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly TokenConfig _tokenConfig;

        private readonly ILogger _logger;

        public AuthController(INotificador notificador,
                              SignInManager<IdentityUser> signInmanager,
                              UserManager<IdentityUser> usermanager,
                              IOptions<TokenConfig> tokenConfig,
                              IUsuario usuario, 
                              ILogger<AuthController> logger) : base(notificador, usuario)
        {
            _signInmanager = signInmanager;
            _usermanager = usermanager;
            _tokenConfig = tokenConfig.Value;
            _logger = logger;
        }

        [HttpPost("usuario/novo")]
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuario(UsuarioRegistrarVM usuarioRegistrarVM)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var newUser = new IdentityUser
            {
                UserName = usuarioRegistrarVM.Email,
                Email = usuarioRegistrarVM.Email,
                EmailConfirmed = true
            };

            var result = await _usermanager.CreateAsync(newUser, usuarioRegistrarVM.Senha);

            if(!result.Succeeded) 
            {
                foreach (var error in result.Errors)
                {
                    NotificarErro(error.Description);
                }
                return CustomResponse();
            }

            await _signInmanager.SignInAsync(newUser, false);

            _logger.LogInformation($"Cadastrado novo usuário {usuarioRegistrarVM.Email}");

            return CustomResponse(usuarioRegistrarVM);

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UsuarioLoginVM usuarioLoginVM)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInmanager.PasswordSignInAsync(usuarioLoginVM.Email, usuarioLoginVM.Senha, false , true);

            if(result.Succeeded) 
            {
                var usuario = await _usermanager.FindByEmailAsync(usuarioLoginVM.Email);
                var roles = await _usermanager.GetRolesAsync(usuario);
                var identityClaims = new ClaimsIdentity();
                identityClaims.AddClaims(await _usermanager.GetClaimsAsync(usuario));

                var token = TokenService.GerarToken(usuario, identityClaims, roles, _tokenConfig);

                var response = new LoginResponseVM
                {
                     Token = token,
                     ExpiraEm = 0,
                     Usuario = new TokenUsuarioVM
                     {
                          Id = usuario.Id,
                          Email = usuario.Email,
                          Claims = identityClaims.Claims.Select(c => new ClaimVM { Tipo = c.Type, Valor = c.Value })
                     }
                };

                _logger.LogInformation($"Novo login usuário: {usuario.Email}");

                return CustomResponse(response);
            }

            if(result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado.");
                return CustomResponse();    
            }

            NotificarErro("Usuário inválido");
            return CustomResponse();
            
        }
        
    }
}