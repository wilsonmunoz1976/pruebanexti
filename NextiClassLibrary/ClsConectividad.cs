
using NextiClassLibrary.Entidades;
using Microsoft.Data.SqlClient;
using NLog;
using System.Data;

namespace NextiClassLibrary
{
    public class ClsConectividad
    {
        public SqlConnection oConnection = new SqlConnection();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ClsConectividad(string? connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                oConnection.ConnectionString = connectionString;
                oConnection.Open();
            }
        }

        public Eventos ListarEventos(int? i_ci_id)
        {
            Respuesta oResp = new Respuesta();
            List<DatoEvento>? retEventos = new List<DatoEvento>();
            try
            {
                DataTable dt = new DataTable("tb0");
                SqlCommand cmd = oConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.pr_evento";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_accion", SqlDbType = SqlDbType.VarChar, Size = 2, Value = "L" });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_ci_id", SqlDbType = SqlDbType.BigInt, Value = i_ci_id });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.InputOutput, ParameterName = "@o_msgerror", SqlDbType = SqlDbType.VarChar, Size = 200 });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.ReturnValue, ParameterName = "@return_value", SqlDbType = SqlDbType.Int });
                dt.Load(cmd.ExecuteReader());

                oResp.codigo = Convert.ToInt16(cmd.Parameters["@return_value"].Value);
                oResp.mensaje = Convert.ToString(cmd.Parameters["@o_msgerror"].Value);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        retEventos.Add(new DatoEvento()
                        {
                            ci_id = Convert.ToInt32(dr["ci_id"]),
                            fx_fecha = Convert.ToDateTime(dr["fx_fecha"]),
                            tx_lugar = Convert.ToString(dr["tx_lugar"]),
                            tx_descripcion = Convert.ToString(dr["tx_descripcion"]),
                            va_precio = Convert.ToDecimal(dr["va_precio"]),
                            te_estado = Convert.ToString(dr["te_estado"])
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                oResp.codigo = -9;
                oResp.mensaje = ex.Message;
            }
            return new Eventos() { respuesta = oResp, datoEvento = retEventos };
        }

        public Evento ConsultarEvento(int i_ci_id)
        {
            Respuesta oResp = new Respuesta();
            DatoEvento? retEventos = null;
            try
            {
                DataTable dt = new DataTable("tb0");
                SqlCommand cmd = oConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.pr_evento";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_accion", SqlDbType = SqlDbType.VarChar, Size = 2, Value = "C" });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_ci_id", SqlDbType = SqlDbType.BigInt, Value = i_ci_id });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.InputOutput, ParameterName = "@o_msgerror", SqlDbType = SqlDbType.VarChar, Size = 200 });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.ReturnValue, ParameterName = "@return_value", SqlDbType = SqlDbType.Int });
                dt.Load(cmd.ExecuteReader());

                oResp.codigo = Convert.ToInt16(cmd.Parameters["@return_value"].Value);
                oResp.mensaje = Convert.ToString(cmd.Parameters["@o_msgerror"].Value);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    retEventos = new DatoEvento()
                    {
                        ci_id = Convert.ToInt32(dr["ci_id"]),
                        fx_fecha = Convert.ToDateTime(dr["fx_fecha"]),
                        tx_lugar = Convert.ToString(dr["tx_lugar"]),
                        tx_descripcion = Convert.ToString(dr["tx_descripcion"]),
                        va_precio = Convert.ToDecimal(dr["va_precio"]),
                        te_estado = Convert.ToString(dr["te_estado"])
                    };

                }
            }
            catch (Exception ex)
            {
                oResp.codigo = -9;
                oResp.mensaje = ex.Message;
            }
            return new Evento() { respuesta = oResp, datoEvento = retEventos };
        }

        public Respuesta InsertarEvento(DatoEvento datoEvento)
        {
            Respuesta oResp = new Respuesta() { codigo = 0, mensaje = "" };
            try
            {
                DataTable dt = new DataTable("tb0");
                SqlCommand cmd = oConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.pr_evento";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_accion", SqlDbType = SqlDbType.VarChar, Size = 2, Value = "I" });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_fx_fecha", SqlDbType = SqlDbType.DateTime, Value = datoEvento.fx_fecha  });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_tx_lugar", SqlDbType = SqlDbType.VarChar, Size = 100, Value = datoEvento.tx_lugar });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_tx_descripcion", SqlDbType = SqlDbType.VarChar, Size = 500, Value = datoEvento.tx_descripcion });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_va_precio", SqlDbType = SqlDbType.Money, Value = datoEvento.va_precio });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_te_estado", SqlDbType = SqlDbType.Char, Size = 1, Value = 'A' });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.InputOutput, ParameterName = "@o_msgerror", SqlDbType = SqlDbType.VarChar, Size = 200 });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.ReturnValue, ParameterName = "@return_value", SqlDbType = SqlDbType.Int });
                cmd.ExecuteNonQuery();

                oResp.codigo = Convert.ToInt16(cmd.Parameters["@return_value"].Value);
                oResp.mensaje = Convert.ToString(cmd.Parameters["@o_msgerror"].Value);

            }
            catch (Exception ex)
            {
                oResp.codigo = -9;
                oResp.mensaje = ex.Message;
            }

            return oResp;
        }

        public Respuesta ModificarEvento(DatoEvento datoEvento)
        {
            Respuesta oResp = new Respuesta() { codigo = 0, mensaje = "" };
            try
            {
                DataTable dt = new DataTable("tb0");
                SqlCommand cmd = oConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.pr_evento";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_accion", SqlDbType = SqlDbType.VarChar, Size = 2, Value = "U" });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_ci_id", SqlDbType = SqlDbType.BigInt, Value = datoEvento.ci_id });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_fx_fecha", SqlDbType = SqlDbType.DateTime, Value = datoEvento.fx_fecha });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_tx_lugar", SqlDbType = SqlDbType.VarChar, Size = 100, Value = datoEvento.tx_lugar });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_tx_descripcion", SqlDbType = SqlDbType.VarChar, Size = 500, Value = datoEvento.tx_descripcion });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_va_precio", SqlDbType = SqlDbType.Money, Value = datoEvento.va_precio });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_te_estado", SqlDbType = SqlDbType.Char, Size = 1, Value = 'A' });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.InputOutput, ParameterName = "@o_msgerror", SqlDbType = SqlDbType.VarChar, Size = 200 });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.ReturnValue, ParameterName = "@return_value", SqlDbType = SqlDbType.Int });
                cmd.ExecuteNonQuery();

                oResp.codigo = Convert.ToInt16(cmd.Parameters["@return_value"].Value);
                oResp.mensaje = Convert.ToString(cmd.Parameters["@o_msgerror"].Value);

            }
            catch (Exception ex)
            {
                oResp.codigo = -9;
                oResp.mensaje = ex.Message;
            }

            return oResp;
        }

        public Respuesta EliminarEvento(int id_evento)
        {
            Respuesta oResp = new Respuesta() { codigo = 0, mensaje = "" };
            try
            {
                DataTable dt = new DataTable("tb0");
                SqlCommand cmd = oConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.pr_evento";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_accion", SqlDbType = SqlDbType.VarChar, Size = 2, Value = "E" });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.Input, ParameterName = "@i_ci_id", SqlDbType = SqlDbType.BigInt, Value = id_evento });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.InputOutput, ParameterName = "@o_msgerror", SqlDbType = SqlDbType.VarChar, Size = 200 });
                cmd.Parameters.Add(new SqlParameter() { Direction = ParameterDirection.ReturnValue, ParameterName = "@return_value", SqlDbType = SqlDbType.Int });
                cmd.ExecuteNonQuery();

                oResp.codigo = Convert.ToInt16(cmd.Parameters["@return_value"].Value);
                oResp.mensaje = Convert.ToString(cmd.Parameters["@o_msgerror"].Value);

            }
            catch (Exception ex)
            {
                oResp.codigo = -9;
                oResp.mensaje = ex.Message;
            }

            return oResp;
        }

    }
}
