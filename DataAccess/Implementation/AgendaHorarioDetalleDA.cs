using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementation
{
    public class AgendaHorarioDetalleDA : IAgendaHorarioDetalleAD
    {
        private readonly string _connection;
        public Exceptions gObjExceptions = new Exceptions();

        public AgendaHorarioDetalleDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<AgendaHorarioDetalle> RecAgendaHorarioDetalles()
        {
            var lista = new List<AgendaHorarioDetalle>();

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecAgendaHorarioDetallesPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var detalle = new AgendaHorarioDetalle
                    {
                        IdAgendaHorarioDetalle = Convert.ToInt32(reader["ID_AGENDAHORARIO_DETALLE"]),
                        IdAgendaHorario = reader["ID_AGENDAHORARIO"] == DBNull.Value ? null : Convert.ToInt32(reader["ID_AGENDAHORARIO"]),
                        NumDia = Convert.ToInt32(reader["NUM_DIA"]),
                        NumHoraInicio = Convert.ToInt32(reader["NUM_HORAINICIO"]),
                        NumHoraFinal = Convert.ToInt32(reader["NUM_HORAFINAL"]),
                        NumIntervaloCita = Convert.ToInt32(reader["NUM_INTERVALOCITA"]),
                        IndLibre = Convert.ToInt32(reader["IND_LIBRE"]) == 1,
                        NumCitas = reader["NUM_CITAS"] == DBNull.Value ? 1 : Convert.ToInt32(reader["NUM_CITAS"]),
                        UsrRegistra = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                        FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                        UsrActualiza = reader["USR_ACTUALIZA"]?.ToString(),
                        FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_ACTUALIZA"])
                    };
                    lista.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public AgendaHorarioDetalle? RecAgendaHorarioDetalleXId(int idDetalle)
        {
            AgendaHorarioDetalle? detalle = null;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecAgendaHorarioDetalleXIdPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("P_ID_AGENDAHORARIO_DETALLE", OracleDbType.Int32).Value = idDetalle;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    detalle = new AgendaHorarioDetalle
                    {
                        IdAgendaHorarioDetalle = Convert.ToInt32(reader["ID_AGENDAHORARIO_DETALLE"]),
                        IdAgendaHorario = reader["ID_AGENDAHORARIO"] == DBNull.Value ? null : Convert.ToInt32(reader["ID_AGENDAHORARIO"]),
                        NumDia = Convert.ToInt32(reader["NUM_DIA"]),
                        NumHoraInicio = Convert.ToInt32(reader["NUM_HORAINICIO"]),
                        NumHoraFinal = Convert.ToInt32(reader["NUM_HORAFINAL"]),
                        NumIntervaloCita = Convert.ToInt32(reader["NUM_INTERVALOCITA"]),
                        IndLibre = Convert.ToInt32(reader["IND_LIBRE"]) == 1,
                        NumCitas = reader["NUM_CITAS"] == DBNull.Value ? 1 : Convert.ToInt32(reader["NUM_CITAS"]),
                        UsrRegistra = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                        FecRegistra = Convert.ToDateTime(reader["FEC_REGISTRA"]),
                        UsrActualiza = reader["USR_ACTUALIZA"]?.ToString(),
                        FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_ACTUALIZA"])
                    };
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return detalle;
        }

        public bool InsAgendaHorarioDetalle(AgendaHorarioDetalle detalle)
        {
            return EjecutarProcedimiento("InsAgendaHorarioDetallePA", detalle);
        }

        public bool ModAgendaHorarioDetalle(AgendaHorarioDetalle detalle)
        {
            return EjecutarProcedimiento("ModAgendaHorarioDetallePA", detalle);
        }

        public bool DelAgendaHorarioDetalle(int idDetalle)
        {
            return EjecutarProcedimiento("DelAgendaHorarioDetallePA", new AgendaHorarioDetalle { IdAgendaHorarioDetalle = idDetalle });
        }

        // =======================================================
        // MÉTODO CENTRALIZADO PARA EJECUTAR SP
        // =======================================================
        private bool EjecutarProcedimiento(string procedimiento, AgendaHorarioDetalle detalle)
        {
            bool resultado = false;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand(procedimiento, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (procedimiento == "DelAgendaHorarioDetallePA")
                {
                    cmd.Parameters.Add("P_ID_AGENDAHORARIO_DETALLE", OracleDbType.Int32).Value = detalle.IdAgendaHorarioDetalle;
                }
                else
                {
                    cmd.Parameters.Add("P_ID_AGENDAHORARIO", OracleDbType.Int32).Value = (object?)detalle.IdAgendaHorario ?? DBNull.Value;
                    cmd.Parameters.Add("P_NUM_DIA", OracleDbType.Int32).Value = detalle.NumDia;
                    cmd.Parameters.Add("P_NUM_HORAINICIO", OracleDbType.Int32).Value = detalle.NumHoraInicio;
                    cmd.Parameters.Add("P_NUM_HORAFINAL", OracleDbType.Int32).Value = detalle.NumHoraFinal;
                    cmd.Parameters.Add("P_NUM_INTERVALOCITA", OracleDbType.Int32).Value = detalle.NumIntervaloCita;
                    cmd.Parameters.Add("P_IND_LIBRE", OracleDbType.Int32).Value = detalle.IndLibre ? 1 : 0;
                    cmd.Parameters.Add("P_NUM_CITAS", OracleDbType.Int32).Value = detalle.NumCitas;
                    cmd.Parameters.Add("P_USR_REGISTRA", OracleDbType.Varchar2).Value = detalle.UsrRegistra;
                    cmd.Parameters.Add("P_FEC_REGISTRA", OracleDbType.Date).Value = detalle.FecRegistra;
                    cmd.Parameters.Add("P_USR_ACTUALIZA", OracleDbType.Varchar2).Value = detalle.UsrActualiza ?? (object)DBNull.Value;
                    cmd.Parameters.Add("P_FEC_ACTUALIZA", OracleDbType.Date).Value = (object?)detalle.FecActualiza ?? DBNull.Value;

                    if (procedimiento == "ModAgendaHorarioDetallePA")
                        cmd.Parameters.Add("P_ID_AGENDAHORARIO_DETALLE", OracleDbType.Int32).Value = detalle.IdAgendaHorarioDetalle;
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
    }
}
