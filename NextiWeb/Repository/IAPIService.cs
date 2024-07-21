using NextiWeb.Models;

namespace NextiWeb.Repository
{
    public interface IAPIService
    {
        Task<Eventos> ListarEventos(int id_evento);
        Task<Evento> ConsultarEvento(int id_evento);
        Task<Respuesta> InsertarEvento(DatoEvento datoEvento);
        Task<Respuesta> ModificarEvento(DatoEvento datoEvento);
        Task<Respuesta> EliminarEvento(int id_evento);
    }
}
