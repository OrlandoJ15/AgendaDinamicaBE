using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class NotificationDA : INotificationDA
    {
        private readonly string _connection;

        public NotificationDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        public async Task<MachoteMensaje> ObtenerMachoteAsync(int numMachote, string codigoIdioma)
        {
            var machote = new MachoteMensaje();

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.NOTIFICACION.machotemsj_consultar_key", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("pnum_machotesms", OracleDbType.Int32).Value = numMachote;
            cmd.Parameters.Add("pcod_idioma", OracleDbType.Varchar2).Value = codigoIdioma;
            cmd.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                machote.Num_Id = Convert.ToInt32(reader["Num_Id"]);
                machote.Titulo = reader["Titulo"]?.ToString();
                machote.Des_Mensaje_Correo = reader["Des_Mensaje_Correo"]?.ToString();
                machote.Des_Mensaje_SMS = reader["Des_Mensaje_SMS"]?.ToString();
                machote.Ind_Activo = reader["Ind_Activo"]?.ToString();
                machote.Ind_AplicaIngles = reader["Ind_AplicaIngles"]?.ToString();
                machote.Correo_ConfirmacionNuevaCita = reader["Correo_ConfirmacionNuevaCita"]?.ToString();
            }

            return machote;
        }

        public async Task<string> ObtenerDireccionEnvioAsync(int numMachote)
        {
            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("FUNCION_OBTENER_CORREO", conn) // reemplazar por tu función
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("pnum_machote", OracleDbType.Int32).Value = numMachote;
            var pCorreo = new OracleParameter("pCorreo", OracleDbType.Varchar2, 4000)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pCorreo);

            await cmd.ExecuteNonQueryAsync();
            return pCorreo.Value?.ToString() ?? string.Empty;
        }

        public async Task<List<CitaProcedimiento>> ObtenerProcedimientosCitaAsync(int numCita)
        {
            var lista = new List<CitaProcedimiento>();

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.CITAPROCEDIMIENTO.procedimientos_x_cita", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("pnum_cita", OracleDbType.Int32).Value = numCita;
            cmd.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new CitaProcedimiento
                {
                    NUM_CITA = Convert.ToInt32(reader["NUM_CITA"]),
                    COD_ARTICULO = reader["COD_ARTICULO"]?.ToString(),
                    DES_DETALLE = reader["DES_DETALLE"]?.ToString()
                });
            }

            return lista;
        }

        public async Task<ArticuloDetalle> ObtenerArticuloDetalleAsync(string codArticulo)
        {
            var detalle = new ArticuloDetalle();

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("SIGH.CRM_ARTICULOS_SINONIMOS.BuscarArticuloDetalle", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("pCodArticulo", OracleDbType.Varchar2).Value = codArticulo;
            cmd.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                detalle.Nom_Articulo = reader["Nom_Articulo"]?.ToString();
                detalle.Desc_Categoria = reader["Desc_Categoria"]?.ToString();
                detalle.Precio = Convert.ToDecimal(reader["Precio"]);
                detalle.Moneda = reader["Moneda"]?.ToString();
            }

            return detalle;
        }

        /*
        public async Task<List<string>> EnviarCorreoAsync(ParamEnvioCorreo correo, List<ParamEnvioCorreoAdjunto> adjuntos, bool enviarAppointment)
        {
            // Aquí llamamos a tu helper interno que ya envía correo
            //var notificaciones = new AGENDA_Notificaciones();
            //return await notificaciones.EnviarCorreoAdjuntosAsync(correo, adjuntos, enviarAppointment);
        }*/
        
    }
}
