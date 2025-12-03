using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementation
{
    public class CitaReprogramadaDA : ICitaReprogramadaDA
    {
        private readonly string _connection;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaReprogramadaDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================
        public List<CitaReprogramada> RecCitasReprogramadas()
        {
            var lista = new List<CitaReprogramada>();

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecCitasReprogramadasPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(MapearCitaReprogramada(reader));
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public CitaReprogramada? RecCitaReprogramadaXId(int numCitaReprogramada)
        {
            CitaReprogramada? cita = null;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecCitaReprogramadaXIdPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("P_NUM_CITAREPROGRAMADA", OracleDbType.Int32).Value = numCitaReprogramada;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cita = MapearCitaReprogramada(reader);
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return cita;
        }

        public bool InsCitaReprogramada(CitaReprogramada cita)
        {
            return EjecutarProcedimiento("InsCitaReprogramadaPA", cita);
        }

        public bool ModCitaReprogramada(CitaReprogramada cita)
        {
            return EjecutarProcedimiento("ModCitaReprogramadaPA", cita);
        }

        public bool DelCitaReprogramada(int numCitaReprogramada)
        {
            return EjecutarProcedimiento("DelCitaReprogramadaPA", new CitaReprogramada { NumCitaReprogramada = numCitaReprogramada });
        }

        // =======================================================
        // MÉTODO CENTRALIZADO
        // =======================================================
        private bool EjecutarProcedimiento(string procedimiento, CitaReprogramada cita)
        {
            bool resultado = false;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand(procedimiento, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (procedimiento == "DelCitaReprogramadaPA")
                {
                    cmd.Parameters.Add("P_NUM_CITAREPROGRAMADA", OracleDbType.Int32).Value = cita.NumCitaReprogramada;
                }
                else
                {
                    cmd.Parameters.Add("P_NUM_AGENDA", OracleDbType.Int32).Value = cita.NumAgenda;
                    cmd.Parameters.Add("P_NUM_CITAANTERIOR", OracleDbType.Int32).Value = (object?)cita.NumCitaAnterior ?? DBNull.Value;
                    cmd.Parameters.Add("P_FEC_CITA", OracleDbType.Date).Value = cita.FecCita;
                    cmd.Parameters.Add("P_NUM_NUEVACITA", OracleDbType.Int32).Value = (object?)cita.NumNuevaCita ?? DBNull.Value;
                    cmd.Parameters.Add("P_FEC_NUEVACITA", OracleDbType.Date).Value = cita.FecNuevaCita;
                    cmd.Parameters.Add("P_USR_REPROGRAMA", OracleDbType.Varchar2).Value = cita.UsrRePrograma;
                    cmd.Parameters.Add("P_FEC_REPROGRAMA", OracleDbType.Date).Value = (object?)cita.FecRePrograma ?? DBNull.Value;

                    if (procedimiento == "ModCitaReprogramadaPA")
                        cmd.Parameters.Add("P_NUM_CITAREPROGRAMADA", OracleDbType.Int32).Value = cita.NumCitaReprogramada;
                }

                connection.Open();
                resultado = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return resultado;
        }

        // =======================================================
        // MAPEO DE RESULTADOS
        // =======================================================
        private CitaReprogramada MapearCitaReprogramada(OracleDataReader reader)
        {
            return new CitaReprogramada
            {
                NumCitaReprogramada = Convert.ToInt32(reader["NUM_CITAREPROGRAMADA"]),
                NumAgenda = Convert.ToInt32(reader["NUM_AGENDA"]),
                NumCitaAnterior = reader["NUM_CITAANTERIOR"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_CITAANTERIOR"]),
                FecCita = Convert.ToDateTime(reader["FEC_CITA"]),
                NumNuevaCita = reader["NUM_NUEVACITA"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_NUEVACITA"]),
                FecNuevaCita = Convert.ToDateTime(reader["FEC_NUEVACITA"]),
                UsrRePrograma = reader["USR_REPROGRAMA"]?.ToString() ?? string.Empty,
                FecRePrograma = reader["FEC_REPROGRAMA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_REPROGRAMA"])
            };
        }
    }
}
