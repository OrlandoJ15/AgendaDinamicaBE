using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using DataAccess;


namespace DataAccess.Implementation
{
    public class AgendaDA : IAgendaDA
    {
        private readonly string _connection;
        public readonly Exceptions _exceptions;

        public AgendaDA(IConfiguration configuration,Exceptions exceptions)
        {
            _connection = configuration.GetConnectionString("OracleDb");
            _exceptions = exceptions;
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<Agenda> RecAgenda()
        {
            var lista = new List<Agenda>();

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("RecAgendaPA", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Cursor de salida (si el SP devuelve registros)
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var agenda = new Agenda
                            {
                                NumAgenda = Convert.ToInt32(reader["NUM_AGENDA"]),
                                NumTipoAgenda = Convert.ToInt32(reader["NUM_TIPOAGENDA"]),
                                IndEstado = Convert.ToInt32(reader["IND_ESTADO"]),
                                NomAgenda = reader["NOM_AGENDA"]?.ToString() ?? string.Empty,
                                IdEdificio = reader["ID_EDIFICIO"]?.ToString(),
                                NumPiso = reader["NUM_PISO"]?.ToString(),
                                CodProf = reader["COD_PROF"]?.ToString(),
                                DesUbicacion = reader["DES_UBICACION"]?.ToString(),
                                DesObservacion = reader["DES_OBSERVACION"]?.ToString(),
                                UsrRegistra = reader["USR_REGISTRA"]?.ToString(),
                                FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                                UsrActualiza = reader["USR_ACTUALIZA"]?.ToString(),
                                FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_ACTUALIZA"])
                            };
                            lista.Add(agenda);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public Agenda? RecAgendaXId(int numAgenda)
        {
            Agenda? agenda = null;

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("RecAgendaXId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_NUM_AGENDA", OracleDbType.Int32).Value = numAgenda;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            agenda = new Agenda
                            {
                                NumAgenda = Convert.ToInt32(reader["NUM_AGENDA"]),
                                NumTipoAgenda = Convert.ToInt32(reader["NUM_TIPOAGENDA"]),
                                IndEstado = Convert.ToInt32(reader["IND_ESTADO"]),
                                NomAgenda = reader["NOM_AGENDA"]?.ToString() ?? string.Empty,
                                IdEdificio = reader["ID_EDIFICIO"]?.ToString(),
                                NumPiso = reader["NUM_PISO"]?.ToString(),
                                CodProf = reader["COD_PROF"]?.ToString(),
                                DesUbicacion = reader["DES_UBICACION"]?.ToString(),
                                DesObservacion = reader["DES_OBSERVACION"]?.ToString(),
                                UsrRegistra = reader["USR_REGISTRA"]?.ToString(),
                                FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                                UsrActualiza = reader["USR_ACTUALIZA"]?.ToString(),
                                FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_ACTUALIZA"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return agenda;
        }

        public bool InsAgenda(Agenda agenda)
        {
            return EjecutarProcedimiento("InsAgendaPA", agenda);
        }

        public bool ModAgenda(Agenda agenda)
        {
            return EjecutarProcedimiento("ModAgendaPA", agenda);
        }

        public bool DelAgenda(int numAgenda)
        {
            return EjecutarProcedimiento("DelAgendaPA", new Agenda { NumAgenda = numAgenda });
        }

        // =======================================================
        // MÉTODO CENTRALIZADO PARA EJECUTAR SP
        // =======================================================
        private bool EjecutarProcedimiento(string procedimiento, Agenda agenda)
        {
            bool resultado = false;

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand(procedimiento, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros según operación
                    if (procedimiento == "DelAgendaPA")
                    {
                        cmd.Parameters.Add("P_NUM_AGENDA", OracleDbType.Int32).Value = agenda.NumAgenda;
                    }
                    else
                    {
                        cmd.Parameters.Add("P_NUM_TIPOAGENDA", OracleDbType.Int32).Value = agenda.NumTipoAgenda;
                        cmd.Parameters.Add("P_NOM_AGENDA", OracleDbType.Varchar2).Value = agenda.NomAgenda;
                        cmd.Parameters.Add("P_ID_EDIFICIO", OracleDbType.Varchar2).Value = agenda.IdEdificio;
                        cmd.Parameters.Add("P_NUM_PISO", OracleDbType.Varchar2).Value = agenda.NumPiso;
                        cmd.Parameters.Add("P_COD_PROF", OracleDbType.Varchar2).Value = agenda.CodProf;
                        cmd.Parameters.Add("P_DES_UBICACION", OracleDbType.Varchar2).Value = agenda.DesUbicacion;
                        cmd.Parameters.Add("P_DES_OBSERVACION", OracleDbType.Varchar2).Value = agenda.DesObservacion;
                        cmd.Parameters.Add("P_USR_REGISTRA", OracleDbType.Varchar2).Value = agenda.UsrRegistra;

                        if (procedimiento == "ModAgendaPA")
                            cmd.Parameters.Add("P_NUM_AGENDA", OracleDbType.Int32).Value = agenda.NumAgenda;
                    }

                    connection.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return resultado;
        }
    }
}
