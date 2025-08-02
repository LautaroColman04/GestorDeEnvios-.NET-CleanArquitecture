using ClienteHTTP.Filtros;
using ClienteHTTP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ClienteHTTP.Controllers
{
    public class AuthController : Controller
    {
        private HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://obligatoriowebapi20250625203030-cygbfedwhuagcdeh.brazilsouth-01.azurewebsites.net/");
        }
        [NoLogueadoAuthorize]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(DTOLogin dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var tk = JsonSerializer.Deserialize<TokenResponse>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                HttpContext.Session.SetString("Token", tk.Token);

                return RedirectToAction("MisEnvios", "Envio");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = errorContent;

                return RedirectToAction("Login");

            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
