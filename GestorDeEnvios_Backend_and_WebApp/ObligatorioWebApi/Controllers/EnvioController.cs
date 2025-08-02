using DTOs.DTOs.DTOsEnvio;
using DTOs.DTOs.DTOsUsuario;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using LogicaNegocio.CustomExceptions.EnvioExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ObligatorioWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        private ICUObtenerEnvioByNumTracking _CUObtenerEnvioByNumTracking;
        private ICUListarEnviosDelUsuario _CUListarEnviosDelUsuario;
        private ICUListarEnviosUsuarioPorFecha _CUListarEnviosUsuarioPorFecha;
        private ICUListarEnviosDelUsuarioPorComentario _CUListarEnviosDelUsuarioPorComentario;
        public EnvioController(ICUObtenerEnvioByNumTracking CUObtenerEnvioByNumTracking, ICUListarEnviosDelUsuario CUListarEnviosDelUsuario, ICUListarEnviosUsuarioPorFecha CUListarEnviosUsuarioPorFecha, ICUListarEnviosDelUsuarioPorComentario CUListarEnviosDelUsuarioPorComentario)
        {
            _CUObtenerEnvioByNumTracking = CUObtenerEnvioByNumTracking;
            _CUListarEnviosDelUsuario = CUListarEnviosDelUsuario;
            _CUListarEnviosUsuarioPorFecha = CUListarEnviosUsuarioPorFecha;
            _CUListarEnviosDelUsuarioPorComentario = CUListarEnviosDelUsuarioPorComentario;
        }



        [HttpGet("{numTracking}")]
        public IActionResult GetEnvio(int numTracking)
        {
            try
            {
                DTOEnvioCliente envio = _CUObtenerEnvioByNumTracking.ObtenerEnvio(numTracking);
                return Ok(envio);
            }
            catch (EnvioNoExisteException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [Authorize(Roles = "Cliente")]
        [HttpGet]
        public IActionResult ListarEnviosUsuario()
        {
            try
            {
                string email = EmailUser();
                List<DTOEnvioCliente> envios = _CUListarEnviosDelUsuario.Ejecutar(email);
                return Ok(envios);
            }
            catch (ErrorAlListarEnviosException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("EnviosPorFecha")]
        public IActionResult ListarEnviosUsuarioPorFecha([FromQuery] string fecha1, [FromQuery] string fecha2, [FromQuery] string? estado)
        {
            try
            {
                DateTime fecha1ToDate = DateTime.Parse(fecha1);
                DateTime fecha2ToDate = DateTime.Parse(fecha2);
                string email = EmailUser();
                List<DTOEnvioCliente> envios = _CUListarEnviosUsuarioPorFecha.Ejecutar(email, fecha1ToDate, fecha2ToDate, estado);
                return Ok(envios);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (EstadoInvalidoException e)
            {
                return BadRequest(e.Message);
            }
            catch (ErrorAlListarEnviosException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("EnviosPorComentario")]
        public IActionResult ListarEnviosUsuarioPorComentario([FromQuery] string comentario)
        {
            try
            {
                string email = EmailUser();
                List<DTOEnvioCliente> envios = _CUListarEnviosDelUsuarioPorComentario.Ejecutar(email, comentario);
                return Ok(envios);
            }
            catch (ErrorAlListarEnviosException e)
            {
                return StatusCode(500, e.Message);
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
