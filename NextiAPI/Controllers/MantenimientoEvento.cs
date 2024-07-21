using NextiClassLibrary.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NLog;

namespace NextiAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MantenimientoEventoController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private bool UseDebug = false;

        //private readonly IConfiguration _configuration;
        private readonly ILogger<MantenimientoEventoController> _logger;

        private SqlConnection oConnection = new SqlConnection();
        private NextiClassLibrary.ClsConectividad oConectividad;

        public MantenimientoEventoController(ILogger<MantenimientoEventoController> ologger)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string? myDb1ConnectionString = configuration.GetConnectionString("DefaultConnection");

            UseDebug = Convert.ToBoolean(configuration.GetSection("Settings").GetSection("debug").Value);

            oConectividad = new NextiClassLibrary.ClsConectividad(myDb1ConnectionString);

            _logger = ologger;

            if (UseDebug) { logger.Info("Se inicializa RegistroFormularioController"); }

        }

        [HttpPost("ListarEventos/{id_evento}")]
        public Eventos ListarEventos(int id_evento)
        {
            return oConectividad.ListarEventos(id_evento);
        }

        [HttpPost("ConsultarEvento/{id_evento}")]
        public Evento ConsultarEvento(int id_evento)
        {
            return oConectividad.ConsultarEvento(id_evento);
        }


        [HttpPost("InsertarEvento")]
        public Respuesta InsertarEvento([FromBody] DatoEvento datoEvento)
        {
            return oConectividad.InsertarEvento(datoEvento);
        }

        [HttpPost("ModificarEvento")]
        public Respuesta ModificarEvento([FromBody] DatoEvento datoEvento)
        {
            return oConectividad.ModificarEvento(datoEvento);
        }

        [HttpPost("EliminarEvento/{id_evento}")]
        public Respuesta EliminarEvento(int id_evento)
        {
            return oConectividad.EliminarEvento(id_evento);
        }
    }
}
