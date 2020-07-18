using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Siscs.Agenda.Api.Services;
using Siscs.Agenda.Api.ViewModels;

namespace Siscs.Agenda.Api.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {

        private readonly SignInManager<IdentityUser> _signInmanager;
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly TokenConfig _tokenConfig;

        public AuthController(SignInManager<IdentityUser> signInmanager, 
                              UserManager<IdentityUser> usermanager,
                              IOptions<TokenConfig> tokenConfig)
        {
            _signInmanager = signInmanager;
            _usermanager = usermanager;
            _tokenConfig = tokenConfig.Value;
        }


        [HttpPost("novousuario")]
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuario(UsuarioRegistrarVM usuarioRegistrarVM)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(p => p.Errors));

            var newUser = new IdentityUser
            {
                UserName = usuarioRegistrarVM.Email,
                Email = usuarioRegistrarVM.Email,
                EmailConfirmed = true
            };

            var result = await _usermanager.CreateAsync(newUser, usuarioRegistrarVM.Senha);

            if(!result.Succeeded) return BadRequest(result.Errors);

            await _signInmanager.SignInAsync(newUser, false);

            return Ok();

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UsuarioLoginVM usuarioLoginVM)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(p => p.Errors));

            var result = await _signInmanager.PasswordSignInAsync(usuarioLoginVM.Email, usuarioLoginVM.Senha, false , true);

            if(result.Succeeded) 
            {
                var usuario = await _usermanager.FindByEmailAsync(usuarioLoginVM.Email);
                var roles = await _usermanager.GetRolesAsync(usuario);
                var identityClaims = new ClaimsIdentity();
                identityClaims.AddClaims(await _usermanager.GetClaimsAsync(usuario));

                return Ok(TokenService.GerarToken(usuario, identityClaims, roles, _tokenConfig));
            }


            return BadRequest(new { Message = "usuario inválido"} );
        }
        
    }
}