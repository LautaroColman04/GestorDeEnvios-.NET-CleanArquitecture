using ClienteHTTP.Filtros;
using ClienteHTTP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ClienteHTTP.Controllers
{
    public class UsuarioController : Controller
    {
        private HttpClient _httpClient;
        public UsuarioController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://obligatoriowebapi20250625203030-cygbfedwhuagcdeh.brazilsouth-01.azurewebsites.net/");
        }

        [LogueadoAuthorize]
        public IActionResult CambiarPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CambiarPasswordAsync(DTOCambiarPassword dto)
        {
            string token = HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync("/api/Usuario/cambiar-password", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Contraseña cambiada correctamente";
                return View();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorContent);
                return View();
            }
        }
    }
}
