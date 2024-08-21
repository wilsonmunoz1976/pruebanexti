namespace NextiWeb.Models
{
    public class DatoEvento
    {
        public int ci_id { get; set; }
        public string? fx_fecha { get; set; }
        public string? tx_lugar { get; set; }
        public string? tx_descripcion { get; set; }
        public decimal? va_precio { get; set; }
        public string? te_estado { get; set; }
    }
}
