using ClienteHTTP.Filtros;
using ClienteHTTP.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ClienteHTTP.Controllers
{
    public class EnvioController : Controller
    {
        private HttpClient _httpClient;
        public EnvioController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://obligatoriowebapi20250625203030-cygbfedwhuagcdeh.brazilsouth-01.azurewebsites.net/");
        }

        [HttpGet]
        public IActionResult GetEnvio()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetEnvioAsync(int numTracking)
        {
            var response = await _httpClient.GetAsync($"/api/Envio/{numTracking}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var envio = JsonSerializer.Deserialize<DTOEnvio>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(envio);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = errorContent;
                Console.WriteLine($"Error recibido: {errorContent}");

                return RedirectToAction("GetEnvio");
            }
        }

        [LogueadoAuthorize]
        [HttpGet]
        public async Task<IActionResult> MisEnviosAsync()
        {
            string token = HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync("/api/Envio");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var listaEnvios = JsonSerializer.Deserialize<List<DTOEnvio>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(listaEnvios);
            }
            else
            {
                Console.WriteLine($" Error: {response.StatusCode} - {response.ReasonPhrase}");
                return View(null);
            }
        }

        [LogueadoAuthorize]
        [HttpGet]
        public IActionResult MisEnviosPorFecha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MisEnviosPorFechaAsync(DateTime fecha1, DateTime fecha2, string estado)
        {
            string token = HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string fecha1String = fecha1.ToString("yyyy-MM-dd");
            string fecha2String = fecha2.ToString("yyyy-MM-dd");
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Envio/EnviosPorFecha?fecha1={fecha1String}&fecha2={fecha2String}&estado={estado}");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var listaEnvios = JsonSerializer.Deserialize<List<DTOEnvio>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(listaEnvios);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = errorContent;

                return RedirectToAction("MisEnviosPorFecha");
            }
        }

        [LogueadoAuthorize]
        [HttpGet]
        public IActionResult MisEnviosPorComentario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MisEnviosPorComentarioAsync(string comentario)
        {
            ViewBag.textoBuscado = comentario;
            string token = HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Envio/EnviosPorComentario?comentario={comentario}");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var listaEnvios = JsonSerializer.Deserialize<List<DTOEnvio>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(listaEnvios);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = errorContent;
                return RedirectToAction("MisEnviosPorComentario");
            }
        }
    }
}
