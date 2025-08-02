using DTOs.DTOs.DTOsUsuario;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaAplicacion.ICasosUso.ICUUsuario;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.Filtros;

namespace ObligatorioP3.Controllers
{
    public class UsuarioController : Controller
    {
        private ICUAltaUsuario _CUAltaUsuario;
        private ICULogin _CULogin;
        private ICUListarUsuarios _CUListarUsuarios;
        private ICUObtenerUsuario _CUObtenerUsuario;
        private ICUActualizarUsuario _CUActualizarUsuario;
        private ICUEliminarUsuario _CUEliminarUsuario;


        public UsuarioController(ICUAltaUsuario CUAltaUsuario, ICULogin CULogin, ICUListarUsuarios cUListarUsuarios, ICUObtenerUsuario cUObtenerUsuario, ICUActualizarUsuario cUActualizarUsuario, ICUEliminarUsuario cUEliminarUsuario)
        {
            _CUAltaUsuario = CUAltaUsuario;
            _CULogin = CULogin;
            _CUListarUsuarios = cUListarUsuarios;
            _CUObtenerUsuario = cUObtenerUsuario;
            _CUActualizarUsuario = cUActualizarUsuario;
            _CUEliminarUsuario = cUEliminarUsuario;
        }

        [LogueadoAuthorize]
        public IActionResult Index()
        {
            return View();
        }

        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DTOAltaUsuario dto)
        {
            try
            {
                int? lid = HttpContext.Session.GetInt32("LogueadoId");
                dto.LogueadoId = lid;

                _CUAltaUsuario.AltaUsuario(dto);
                ViewBag.msg = "Alta correcta";
            }
            catch (Exception e)
            {

                ViewBag.msg = e.Message;
            }
            return View();
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }


        [NoLogueadoAuthorize]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(DTOUsuario dto)
        {
            try
            {
                DTOUsuario b = _CULogin.VerificarDatosParaLogin(dto);
                if (b.Rol.Equals("Cliente"))
                {
                    throw new Exception("Los clientes no tienen permitido loguearse aca");
                }
                HttpContext.Session.SetInt32("LogueadoId", (int)b.Id);
                HttpContext.Session.SetString("LogueadoRol", b.Rol);
                HttpContext.Session.SetString("LogueadoNombreCompleto", b.Nombre + " " + b.Apellido);
                return RedirectToAction("Index", "Usuario");
            }
            catch (Exception e)
            {

                ViewBag.error = e.Message;
                return View();
            }

        }

        [LogueadoAuthorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Listar()
        {
            return View(_CUListarUsuarios.ListarUsuarios());
        }

        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Details(int id)
        {
            try
            {
                DTOUsuario dto = _CUObtenerUsuario.ObtenerUsuario(id);
                return View(dto);
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }
        }
        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Edit(int id)
        {
            try
            {
                DTOUsuario dto = _CUObtenerUsuario.ObtenerUsuario(id);
                return View(dto);
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(DTOUsuario dto)
        {
            try
            {
                int? lid = HttpContext.Session.GetInt32("LogueadoId");
                dto.LogueadoId = lid;
                _CUActualizarUsuario.ActualizarUsuario(dto);
                ViewBag.msg = "Actualizado correctamente";
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }
            return View(dto);

        }
        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Delete(int id)
        {
            try
            {
                DTOUsuario dto = _CUObtenerUsuario.ObtenerUsuario(id);
                return View(dto);
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Delete(DTOUsuario dto)
        {
            try
            {
                int? lid = HttpContext.Session.GetInt32("LogueadoId");
                dto.LogueadoId = lid;
                _CUEliminarUsuario.EliminarUsuario(dto);
                ViewBag.msg = "Eliminado correctamente";
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }
            return RedirectToAction("Listar", "Usuario");
        }
    }
}
