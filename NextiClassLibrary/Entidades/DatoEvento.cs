using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextiClassLibrary.Entidades
{
    public class DatoEvento
    {
        public int ci_id { get; set; }
        public DateTime? fx_fecha { get; set; }
        public string? tx_lugar { get; set; }
        public string? tx_descripcion { get; set; }
        public decimal? va_precio { get; set; }
        public string? te_estado {  get; set; }
    }
}
