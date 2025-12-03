using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementation
{
    public class CitaCanceladaDA : ICitaCanceladaDA
    {
        private readonly string _connection;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaCanceladaDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<CitaCancelada> RecCitasCanceladas()
        {
            var lista = new List<CitaCancelada>();

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecCitasCanceladasPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(MapearCitaCancelada(reader));
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public CitaCancelada? RecCitaCanceladaXId(int numCitaCancelada)
        {
            CitaCancelada? cita = null;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecCitaCanceladaXIdPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("P_NUM_CITACANCELADA", OracleDbType.Int32).Value = numCitaCancelada;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cita = MapearCitaCancelada(reader);
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return cita;
        }

        public bool InsCitaCancelada(CitaCancelada cita)
        {
            return EjecutarProcedimiento("InsCitaCanceladaPA", cita);
        }

        public bool ModCitaCancelada(CitaCancelada cita)
        {
            return EjecutarProcedimiento("ModCitaCanceladaPA", cita);
        }

        public bool DelCitaCancelada(int numCitaCancelada)
        {
            return EjecutarProcedimiento("DelCitaCanceladaPA", new CitaCancelada { NumCitaCancelada = numCitaCancelada });
        }

        // =======================================================
        // MÉTODO CENTRALIZADO PARA EJECUTAR SP
        // =======================================================
        private bool EjecutarProcedimiento(string procedimiento, CitaCancelada cita)
        {
            bool resultado = false;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand(procedimiento, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (procedimiento == "DelCitaCanceladaPA")
                {
                    cmd.Parameters.Add("P_NUM_CITACANCELADA", OracleDbType.Int32).Value = cita.NumCitaCancelada;
                }
                else
                {
                    cmd.Parameters.Add("P_NUM_AGENDA", OracleDbType.Int32).Value = cita.NumAgenda;
                    cmd.Parameters.Add("P_FEC_CITA", OracleDbType.Date).Value = cita.FecCita;
                    cmd.Parameters.Add("P_FEC_REGISTRA", OracleDbType.Date).Value = cita.FecRegistra;
                    cmd.Parameters.Add("P_USR_REGISTRA", OracleDbType.Varchar2).Value = cita.UsrRegistra;
                    cmd.Parameters.Add("P_IND_REPROGRAMADA", OracleDbType.Int32).Value = cita.IndReprogramada ? 1 : 0;
                    cmd.Parameters.Add("P_IND_SEGUIMIENTO", OracleDbType.Int32).Value = cita.IndSeguimiento ? 1 : 0;
                    cmd.Parameters.Add("P_NUM_EXPEDIENTE", OracleDbType.Int32).Value = (object?)cita.NumExpediente ?? DBNull.Value;
                    cmd.Parameters.Add("P_NOM_PACIENTE", OracleDbType.Varchar2).Value = cita.NomPaciente;
                    cmd.Parameters.Add("P_NUM_MOTIVO", OracleDbType.Int32).Value = cita.NumMotivo;
                    cmd.Parameters.Add("P_DES_MOTIVO", OracleDbType.Varchar2).Value = cita.DesMotivo;
                    cmd.Parameters.Add("P_FEC_FINALCITA", OracleDbType.Date).Value = (object?)cita.FecFinalCita ?? DBNull.Value;
                    cmd.Parameters.Add("P_NUM_CITAANTERIOR", OracleDbType.Int32).Value = (object?)cita.NumCitaAnterior ?? DBNull.Value;
                    cmd.Parameters.Add("P_NUM_NUEVAAGENDA", OracleDbType.Int32).Value = (object?)cita.NumNuevaAgenda ?? DBNull.Value;
                    cmd.Parameters.Add("P_NUM_NUEVACITA", OracleDbType.Int32).Value = (object?)cita.NumNuevaCita ?? DBNull.Value;
                    cmd.Parameters.Add("P_FEC_NUEVACITA", OracleDbType.Date).Value = (object?)cita.FecNuevaCita ?? DBNull.Value;
                    cmd.Parameters.Add("P_USR_REPROGRAMA", OracleDbType.Varchar2).Value = cita.UsrRePrograma ?? (object)DBNull.Value;
                    cmd.Parameters.Add("P_FEC_REPROGRAMA", OracleDbType.Date).Value = (object?)cita.FecRePrograma ?? DBNull.Value;
                    cmd.Parameters.Add("P_DES_SEGUIMIENTO", OracleDbType.Varchar2).Value = cita.DesSeguimiento ?? (object)DBNull.Value;
                    cmd.Parameters.Add("P_NUM_TIPOCITA", OracleDbType.Int32).Value = (object?)cita.NumTipoCita ?? DBNull.Value;

                    if (procedimiento == "ModCitaCanceladaPA")
                        cmd.Parameters.Add("P_NUM_CITACANCELADA", OracleDbType.Int32).Value = cita.NumCitaCancelada;
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
        // MÉTODO PRIVADO PARA MAPEAR LECTURA DE ORACLE
        // =======================================================
        private CitaCancelada MapearCitaCancelada(OracleDataReader reader)
        {
            return new CitaCancelada
            {
                NumCitaCancelada = Convert.ToInt32(reader["NUM_CITACANCELADA"]),
                NumAgenda = Convert.ToInt32(reader["NUM_AGENDA"]),
                FecCita = Convert.ToDateTime(reader["FEC_CITA"]),
                FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                UsrRegistra = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                IndReprogramada = Convert.ToInt32(reader["IND_REPROGRAMADA"]) == 1,
                IndSeguimiento = Convert.ToInt32(reader["IND_SEGUIMIENTO"]) == 1,
                NumExpediente = reader["NUM_EXPEDIENTE"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_EXPEDIENTE"]),
                NomPaciente = reader["NOM_PACIENTE"]?.ToString() ?? string.Empty,
                NumMotivo = Convert.ToInt32(reader["NUM_MOTIVO"]),
                DesMotivo = reader["DES_MOTIVO"]?.ToString() ?? string.Empty,
                FecFinalCita = reader["FEC_FINALCITA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_FINALCITA"]),
                NumCitaAnterior = reader["NUM_CITAANTERIOR"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_CITAANTERIOR"]),
                NumNuevaAgenda = reader["NUM_NUEVAAGENDA"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_NUEVAAGENDA"]),
                NumNuevaCita = reader["NUM_NUEVACITA"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_NUEVACITA"]),
                FecNuevaCita = reader["FEC_NUEVACITA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_NUEVACITA"]),
                UsrRePrograma = reader["USR_REPROGRAMA"]?.ToString(),
                FecRePrograma = reader["FEC_REPROGRAMA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_REPROGRAMA"]),
                DesSeguimiento = reader["DES_SEGUIMIENTO"]?.ToString(),
                NumTipoCita = reader["NUM_TIPOCITA"] == DBNull.Value ? null : Convert.ToInt32(reader["NUM_TIPOCITA"])
            };
        }
    }
}
