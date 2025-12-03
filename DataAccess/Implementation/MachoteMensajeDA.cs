using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;
using Entities.Models;
using DataAccess.Interfaces;
using CommonMethods;

namespace DataAccess.Implementation
{
    public class MachoteMensajeDA : IMachoteMensajeDA
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly Exceptions _excepciones = new Exceptions();

        public MachoteMensajeDA(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OracleConnection");
        }

        public IEnumerable<MachoteMensaje> ObtenerMachotes()
        {
            var lista = new List<MachoteMensaje>();

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_MACHOTEMENSAJE.SP_OBTENER_TODOS", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(MapearMachote(reader));
                    }
                }
            }

            return lista;
        }

        public MachoteMensaje ObtenerMachotePorId(int numId)
        {
            MachoteMensaje machote = null;

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_MACHOTEMENSAJE.SP_OBTENER_POR_ID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_NUM_ID", OracleDbType.Int32).Value = numId;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        machote = MapearMachote(reader);
                    }
                }
            }

            return machote;
        }

        public bool InsertarMachote(MachoteMensaje machote)
        {
            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_MACHOTEMENSAJE.SP_INSERTAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("P_TITULO", OracleDbType.Varchar2).Value = machote.Titulo;
                cmd.Parameters.Add("P_DES_MENSAJE_CORREO", OracleDbType.Clob).Value = machote.Des_Mensaje_Correo;
                cmd.Parameters.Add("P_DES_MENSAJE_SMS", OracleDbType.Clob).Value = machote.Des_Mensaje_SMS;
                cmd.Parameters.Add("P_IND_ACTIVO", OracleDbType.Varchar2).Value = machote.Ind_Activo;
                cmd.Parameters.Add("P_IND_APLICA_INGLES", OracleDbType.Varchar2).Value = machote.Ind_AplicaIngles;
                cmd.Parameters.Add("P_CORREO_CONF_NUEVA_CITA", OracleDbType.Varchar2).Value = machote.Correo_ConfirmacionNuevaCita;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ActualizarMachote(MachoteMensaje machote)
        {
            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_MACHOTEMENSAJE.SP_ACTUALIZAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("P_NUM_ID", OracleDbType.Int32).Value = machote.Num_Id;
                cmd.Parameters.Add("P_TITULO", OracleDbType.Varchar2).Value = machote.Titulo;
                cmd.Parameters.Add("P_DES_MENSAJE_CORREO", OracleDbType.Clob).Value = machote.Des_Mensaje_Correo;
                cmd.Parameters.Add("P_DES_MENSAJE_SMS", OracleDbType.Clob).Value = machote.Des_Mensaje_SMS;
                cmd.Parameters.Add("P_IND_ACTIVO", OracleDbType.Varchar2).Value = machote.Ind_Activo;
                cmd.Parameters.Add("P_IND_APLICA_INGLES", OracleDbType.Varchar2).Value = machote.Ind_AplicaIngles;
                cmd.Parameters.Add("P_CORREO_CONF_NUEVA_CITA", OracleDbType.Varchar2).Value = machote.Correo_ConfirmacionNuevaCita;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarMachote(int numId)
        {
            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_MACHOTEMENSAJE.SP_ELIMINAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_NUM_ID", OracleDbType.Int32).Value = numId;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private MachoteMensaje MapearMachote(OracleDataReader reader)
        {
            return new MachoteMensaje
            {
                Num_Id = Convert.ToInt32(reader["NUM_ID"]),
                Titulo = reader["TITULO"].ToString(),
                Des_Mensaje_Correo = reader["DES_MENSAJE_CORREO"].ToString(),
                Des_Mensaje_SMS = reader["DES_MENSAJE_SMS"].ToString(),
                Ind_Activo = reader["IND_ACTIVO"].ToString(),
                Ind_AplicaIngles = reader["IND_APLICA_INGLES"].ToString(),
                Correo_ConfirmacionNuevaCita = reader["CORREO_CONF_NUEVA_CITA"].ToString()
            };
        }
    }
}
