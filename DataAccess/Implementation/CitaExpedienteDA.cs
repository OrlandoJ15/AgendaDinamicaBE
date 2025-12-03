using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementation
{
    public class TExpedienteAD : ITExpedienteAD
    {
        private readonly string _connection;
        public Exceptions gObjExceptions = new Exceptions();

        public TExpedienteAD(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<Expediente> RecExpedientes()
        {
            var lista = new List<Expediente>();

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecExpedientesPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(MapearExpediente(reader));
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public Expediente? RecExpedienteXId(int numExpediente)
        {
            Expediente? expediente = null;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecExpedienteXIdPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("P_NUM_EXPEDIENTE", OracleDbType.Int32).Value = numExpediente;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    expediente = MapearExpediente(reader);
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return expediente;
        }

        public bool InsExpediente(Expediente expediente)
        {
            return EjecutarProcedimiento("InsExpedientePA", expediente);
        }

        public bool ModExpediente(Expediente expediente)
        {
            return EjecutarProcedimiento("ModExpedientePA", expediente);
        }

        public bool DelExpediente(int numExpediente)
        {
            return EjecutarProcedimiento("DelExpedientePA", new Expediente { NumExpediente = numExpediente });
        }

        // =======================================================
        // MÉTODO CENTRALIZADO DE EJECUCIÓN
        // =======================================================

        private bool EjecutarProcedimiento(string procedimiento, Expediente expediente)
        {
            bool resultado = false;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand(procedimiento, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // ⚙️ Si solo elimina, solo pasa el ID
                if (procedimiento == "DelExpedientePA")
                {
                    cmd.Parameters.Add("P_NUM_EXPEDIENTE", OracleDbType.Int32).Value = expediente.NumExpediente;
                }
                else
                {
                    cmd.Parameters.Add("P_NUM_EXPEDIENTE", OracleDbType.Int32).Value = expediente.NumExpediente;
                    cmd.Parameters.Add("P_NUM_CARNET", OracleDbType.Varchar2).Value = expediente.NumCarnet;
                    cmd.Parameters.Add("P_PRIMER_AP", OracleDbType.Varchar2).Value = expediente.PrimerAp;
                    cmd.Parameters.Add("P_SEGUNDO_AP", OracleDbType.Varchar2).Value = expediente.SegundoAp;
                    cmd.Parameters.Add("P_PRIMER_NOM", OracleDbType.Varchar2).Value = expediente.PrimerNom;
                    cmd.Parameters.Add("P_SEGUNDO_NOM", OracleDbType.Varchar2).Value = expediente.SegundoNom;
                    cmd.Parameters.Add("P_SEXO", OracleDbType.Varchar2).Value = expediente.Sexo;
                    cmd.Parameters.Add("P_COD_TIPDOC", OracleDbType.Varchar2).Value = expediente.CodTipDoc;
                    cmd.Parameters.Add("P_NUM_ID", OracleDbType.Varchar2).Value = expediente.NumId;
                    cmd.Parameters.Add("P_FEC_NACIMIENTO", OracleDbType.Date).Value = expediente.FecNacimiento;
                    cmd.Parameters.Add("P_CORREO_ELECTRONICO", OracleDbType.Varchar2).Value = expediente.CorreoElectronico;
                    cmd.Parameters.Add("P_USUARIO", OracleDbType.Varchar2).Value = expediente.Usuario;
                    cmd.Parameters.Add("P_IND_FALLECIDO", OracleDbType.Varchar2).Value = expediente.IndFallecido;
                    cmd.Parameters.Add("P_IND_ORIGEN", OracleDbType.Varchar2).Value = expediente.IndOrigen;
                    cmd.Parameters.Add("P_IND_CRONICO", OracleDbType.Int16).Value = expediente.IndCronico ? 1 : 0;
                    cmd.Parameters.Add("P_ESTADO_CIVIL", OracleDbType.Varchar2).Value = expediente.EstadoCivil;
                    // ⚠️ Puedes agregar más campos según tus procedimientos almacenados
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
        private Expediente MapearExpediente(OracleDataReader reader)
        {
            return new Expediente
            {
                NumExpediente = Convert.ToInt32(reader["NUM_EXPEDIENTE"]),
                NumCarnet = reader["NUM_CARNET"]?.ToString(),
                PrimerAp = reader["PRIMER_AP"]?.ToString(),
                SegundoAp = reader["SEGUNDO_AP"]?.ToString(),
                PrimerNom = reader["PRIMER_NOM"]?.ToString(),
                SegundoNom = reader["SEGUNDO_NOM"]?.ToString(),
                Sexo = reader["SEXO"]?.ToString(),
                NumId = reader["NUM_ID"]?.ToString(),
                FecNacimiento = reader["FEC_NACIMIENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_NACIMIENTO"]),
                CorreoElectronico = reader["CORREO_ELECTRONICO"]?.ToString(),
                Usuario = reader["USUARIO"]?.ToString(),
                EstadoCivil = reader["ESTADO_CIVIL"]?.ToString(),
                IndFallecido = reader["IND_FALLECIDO"]?.ToString() ?? "N",
                IndCronico = reader["IND_CRONICO"] != DBNull.Value && Convert.ToInt32(reader["IND_CRONICO"]) == 1,
                IndOrigen = reader["IND_ORIGEN"]?.ToString() ?? "SIGH"
            };
        }
    }
}
