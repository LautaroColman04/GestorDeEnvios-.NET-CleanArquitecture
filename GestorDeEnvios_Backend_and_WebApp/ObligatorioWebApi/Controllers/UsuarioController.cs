using DTOs.DTOs.DTOsUsuario;
using LogicaAplicacion.ICasosUso.ICUUsuario;
using LogicaNegocio.CustomExceptions.UsuarioExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ObligatorioWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ICUCambiarPassword _CUCambiarPassword;
        public UsuarioController(ICUCambiarPassword CUCambiarPassword)
        {
            _CUCambiarPassword = CUCambiarPassword;
        }

        [Authorize(Roles = "Cliente")]
        [HttpPatch("cambiar-password")]
        public IActionResult CambiarPassword([FromBody] DTOCambiarPassword dto)
        {
            try
            {
                string email = EmailUser();
                _CUCambiarPassword.Ejecutar(dto, email);
                return Ok("Contraseña cambiada correctamente");
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (PasswordNoValidaException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string EmailUser()
        {
            string email = null;
            // como obtener el email del token
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var emailClaim = claimsIdentity.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
                email = emailClaim.Value;
            }
            return email;
        }
    }
}
