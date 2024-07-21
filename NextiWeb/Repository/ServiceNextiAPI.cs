using NextiWeb.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace NextiWeb.Repository
{
    public class ServiceNextiAPI : IAPIService
    {
        private static string _urlBaseApi = "";
        private readonly HttpClient _httpClient;

        public ServiceNextiAPI()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _urlBaseApi = builder.GetSection("ConexionApi:UrlBase").Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_urlBaseApi);
        }

        public async Task<Evento> ConsultarEvento(int id_evento)
        {
            Evento? retEvento = new();

            StringContent? content = null;
            HttpResponseMessage response = await _httpClient.PostAsync("MantenimientoEvento/ConsultarEvento/" + id_evento.ToString(), content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                retEvento = JsonConvert.DeserializeObject<Evento>(respuesta);
                return retEvento;
            } else
            {
                return await Task.FromResult(retEvento);
            }
        }



        public async Task<Eventos> ListarEventos(int id_evento)
        {
            Eventos? retEventos = new();

            StringContent? content = null;
            HttpResponseMessage response = await _httpClient.PostAsync("MantenimientoEvento/ListarEventos/" + id_evento.ToString(), content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                retEventos = JsonConvert.DeserializeObject<Eventos>(respuesta);
                return retEventos;
            }
            else
            {
                return await Task.FromResult(retEventos);
            }

        }

        public async Task<Respuesta> EliminarEvento(int id_evento)
        {
            Respuesta? retRespuesta = new();

            StringContent? content = null;
            HttpResponseMessage response = await _httpClient.PostAsync("MantenimientoEvento/EliminarEvento/" + id_evento.ToString(), content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                retRespuesta = JsonConvert.DeserializeObject<Respuesta>(respuesta);
                return retRespuesta;
            }
            else
            {
                return await Task.FromResult(retRespuesta);
            }
        }

        public async Task<Respuesta> InsertarEvento(DatoEvento datoEvento)
        {
            Respuesta? retRespuesta = new();

            StringContent? content = new(JsonConvert.SerializeObject(datoEvento), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("MantenimientoEvento/InsertarEvento", content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                retRespuesta = JsonConvert.DeserializeObject<Respuesta>(respuesta);
                return retRespuesta;
            }
            else
            {
                return await Task.FromResult(retRespuesta);
            }
        }

        public async Task<Respuesta> ModificarEvento(DatoEvento datoEvento)
        {
            Respuesta? retRespuesta = new();

            StringContent? content = new(JsonConvert.SerializeObject(datoEvento), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("MantenimientoEvento/ModificarEvento", content);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                retRespuesta = JsonConvert.DeserializeObject<Respuesta>(respuesta);
                return retRespuesta;
            }
            else
            {
                return await Task.FromResult(retRespuesta);
            }
        }
    }
}
