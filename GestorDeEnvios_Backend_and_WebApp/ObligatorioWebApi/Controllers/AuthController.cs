using DTOs.DTOs.DTOsAuth;
using DTOs.DTOs.DTOsUsuario;
using LogicaAplicacion.ICasosUso.ICUUsuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ObligatorioWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ICULogin _CULogin;
        public AuthController(ICULogin CULogin)
        {
            _CULogin = CULogin;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DTOLogin dtoLogin)
        {
            try
            {
                DTOUsuario dtoUsuarioLogin = new DTOUsuario();
                dtoUsuarioLogin.Email = dtoLogin.Email;
                dtoUsuarioLogin.Password = dtoLogin.Password;
                DTOUsuario dtoUsuario = _CULogin.VerificarDatosParaLogin(dtoUsuarioLogin);
                if(dtoUsuario.Rol != "Cliente")
                {
                    return StatusCode(403,"Solo los clientes pueden iniciar sesión.");
                }
                var secretKey = "UTzl^7yPl$5xrT6&{7RZCSG&O42MEK89$CW1XXRrN(>XqIp{W4s2S5$>KT$6CG!2M]'ZlrqH-t%eI4.X9W~u#qO+oX£+[?7QDAa";
                var secretKeyCodificada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                List<Claim> claims = 
                [
                 new Claim(ClaimTypes.Email, dtoUsuario.Email),
                 new Claim(ClaimTypes.Role , dtoUsuario.Rol)
                ];

                var credenciales = new SigningCredentials(secretKeyCodificada, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credenciales);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { Token = jwt });
            }
            catch (Exception e)
            {
                return Unauthorized("Error al iniciar sesion");
            }
        }

    }
}
