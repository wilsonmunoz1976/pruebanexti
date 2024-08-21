using Microsoft.AspNetCore.Mvc;
using NextiWeb.Models;
using System.Diagnostics;
using NextiWeb.Repository;
using NextiWeb.Models;
using System.Net.Http.Headers;

namespace NextiWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAPIService _apiService;

        public HomeController(IAPIService ApiService)
        {
            _apiService = ApiService;
        }

        public async Task<IActionResult> Index()
        {
            Eventos modEventos = await _apiService.ListarEventos(0);
            return View(modEventos);
        }

		public async Task<IActionResult> ListarEventos()
		{
			Eventos modEventos = await _apiService.ListarEventos(0);
			return View("ListarEventos", modEventos);
		}
		public async Task<IActionResult> NuevoEvento()
		{
			return View("InsertarEvento");
		}

		
		public async Task<IActionResult> Regresar()
		{
			Eventos modEventos = await _apiService.ListarEventos(0);
			return View("Index", modEventos);
		}

		public async Task<IActionResult> InsertarEvento(DatoEvento datoEvento)
		{
            Respuesta respuesta = await _apiService.InsertarEvento(datoEvento);
            string[] msj = new string[2];

            msj[0] = respuesta.codigo.ToString();
            msj[1] = respuesta.mensaje;

			return Json(msj);
		}

		public async Task<IActionResult> ModificarEvento(DatoEvento datoEvento)
		{
			Respuesta respuesta = await _apiService.ModificarEvento(datoEvento);
			string[] msj = new string[2];

			msj[0] = respuesta.codigo.ToString();
			msj[1] = respuesta.mensaje;

			return Json(msj);
		}
		public async Task<IActionResult> EliminarEvento(int i_ci_id)
		{
			Respuesta respuesta = await _apiService.EliminarEvento(i_ci_id);
			string[] msj = new string[2];

			msj[0] = respuesta.codigo.ToString();
			msj[1] = respuesta.mensaje;

			return Json(msj);
		}

		public async Task<IActionResult> BuscarEvento(int i_ci_id)
		{
			Evento modEvento = await _apiService.ConsultarEvento(i_ci_id);
			return View("ActualizarEvento", modEvento);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
