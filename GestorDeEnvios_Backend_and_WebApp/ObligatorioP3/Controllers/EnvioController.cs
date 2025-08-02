using DTOs.DTOs.DTOsEnvio;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.Filtros;
using ObligatorioP3.Models;
using ObligatorioP3.Models.ModelsEnvio;
using System.Net;
using System.Web;

namespace ObligatorioP3.Controllers
{
    public class EnvioController : Controller
    {

        private ICUCreateEnvio _CUCreateEnvio;
        private ICUFinalizarEnvio _CUFinalizarEnvio;
        private ICUObtenerEnvio _CUObtenerEnvio;
        private ICUListarEnvios _CUListarEnvios;
        private ICUListarEnviosEnProceso _CUListarEnviosEnProceso;
        private ICUCreateSeguimientoEnvio _CUCreateSeguimientoEnvio;

        public EnvioController(ICUCreateEnvio CUCreateEnvio, ICUFinalizarEnvio CUFinalizarEnvio, ICUObtenerEnvio CUObtenerEnvio, ICUListarEnvios CUListarEnvios, ICUListarEnviosEnProceso CUListarEnviosEnProceso, ICUCreateSeguimientoEnvio CUCreateSeguimientoEnvio)
        {
            _CUCreateEnvio = CUCreateEnvio;
            _CUFinalizarEnvio = CUFinalizarEnvio;
            _CUObtenerEnvio = CUObtenerEnvio;
            _CUListarEnvios = CUListarEnvios;
            _CUListarEnviosEnProceso = CUListarEnviosEnProceso;
            _CUCreateSeguimientoEnvio = CUCreateSeguimientoEnvio;
        }

        [LogueadoAuthorize]
        // GET: EnvioController
        public IActionResult Index()
        {
            return View(_CUListarEnvios.ListarEnvios());
        }

        [LogueadoAuthorize]
        // GET: EnvioController/ListarEnviosEnProceso
        public IActionResult ListarEnProceso()
        {
            //List<DTOEnvio> envios = _CUListarEnvios.ListarEnvios();
            //List<DTOEnvio> enviosEnProceso = envios.Where(e => e.Estado == "EnProceso").ToList();
            return View(_CUListarEnviosEnProceso.ListarEnviosEnProceso());
        }

        [LogueadoAuthorize]
        // GET: EnvioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnvioController/Create
        [HttpPost]
        public ActionResult Create(DTOCreateEnvio dto)
        {
            try
            {
                int? lid = HttpContext.Session.GetInt32("LogueadoId");
                dto.LogueadoId = lid;

                _CUCreateEnvio.CreateEnvio(dto);
                ViewBag.msg = "Creado correctamente";
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }
            return View();
        }

        [LogueadoAuthorize]
        // GET: EnvioController/FinalizarEnvio/?
        public ActionResult Finalizar(int id, string origen)
        {
            var ViewModel = new FinalizarEnvioViewModel();
            ViewModel.Origen = origen;
            ViewModel.dtoEnvio = _CUObtenerEnvio.ObtenerEnvio(id);

            return View(ViewModel);
        }
        // POST: EnvioController/FinalizarEnvio/?
        [HttpPost]
        public ActionResult Finalizar(DTOEnvio dto, string origen)
        {
            try
            {
                dto.LogueadoId = HttpContext.Session.GetInt32("LogueadoId");
                _CUFinalizarEnvio.FinalizarEnvio(dto);
                ViewBag.msg = "Finalizado correctamente";

            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }
            return Redirect("/Envio/" + origen);
        }

        [LogueadoAuthorize]
        // GET: EnvioController/Details/?
        public ActionResult Details(int id)
        {
            DTOEnvio dto = _CUObtenerEnvio.ObtenerEnvio(id);
            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
        }

        [LogueadoAuthorize]
        // GET: AgregarSeguimiento/?
        public ActionResult AgregarSeguimiento(int id)
        {
            var ViewModel = new AgregarSeguimientoViewModel();
            ViewModel.dtoEnvio = _CUObtenerEnvio.ObtenerEnvio(id);
            return View(ViewModel);
        }

        // POST: EnvioController/AgregarSeguimiento/?
        [HttpPost]
        public ActionResult AgregarSeguimiento(AgregarSeguimientoViewModel vm)
        {
            try
            {
                vm.dtoCreateSeguimientoEnvio.dtoComentario.UsuarioId = HttpContext.Session.GetInt32("LogueadoId");
                _CUCreateSeguimientoEnvio.CreateSeguimientoEnvio(vm.dtoCreateSeguimientoEnvio);
                ViewBag.msg = "Creado correctamente";
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }
            return RedirectToAction("Details", new { id = vm.dtoCreateSeguimientoEnvio.EnvioId });
        }
    }
}
