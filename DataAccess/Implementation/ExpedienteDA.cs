using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DataAccess.Implementation
{
    public class ExpedienteDA : IExpedienteDA
    {
        private readonly string _connection;
        private readonly Exceptions _exceptions;

        public ExpedienteDA(IConfiguration configuration, Exceptions exceptions)
        {
            _connection = configuration.GetConnectionString("OracleDb");
            _exceptions = exceptions;
        }

        public List<Expediente> RecExpediente()
        {
            var lista = new List<Expediente>();

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecExpedientePA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var expediente = new Expediente
                    {
                        NumExpediente = Convert.ToInt32(reader["NUM_EXPEDIENTE"]),
                        NumCarnet = reader["NUM_CARNET"]?.ToString(),
                        PrimerAp = reader["PRIMER_AP"]?.ToString(),
                        SegundoAp = reader["SEGUNDO_AP"]?.ToString(),
                        PrimerNom = reader["PRIMER_NOM"]?.ToString(),
                        SegundoNom = reader["SEGUNDO_NOM"]?.ToString(),
                        Sexo = reader["SEXO"]?.ToString(),
                        CodTipDoc = reader["COD_TIPDOC"]?.ToString(),
                        NumId = reader["NUM_ID"]?.ToString(),
                        CodClase = reader["COD_CLASE"]?.ToString(),
                        FecNacimiento = Convert.ToDateTime(reader["FEC_NACIMIENTO"]),
                        // ... agregar el resto de campos siguiendo el mismo patrón
                    };
                    lista.Add(expediente);
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
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
                using var cmd = new OracleCommand("RecExpedienteXId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("P_NUM_EXPEDIENTE", OracleDbType.Int32).Value = numExpediente;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    expediente = new Expediente
                    {
                        NumExpediente = Convert.ToInt32(reader["NUM_EXPEDIENTE"]),
                        NumCarnet = reader["NUM_CARNET"]?.ToString(),
                        PrimerAp = reader["PRIMER_AP"]?.ToString(),
                        SegundoAp = reader["SEGUNDO_AP"]?.ToString(),
                        PrimerNom = reader["PRIMER_NOM"]?.ToString(),
                        SegundoNom = reader["SEGUNDO_NOM"]?.ToString(),
                        Sexo = reader["SEXO"]?.ToString(),
                        CodTipDoc = reader["COD_TIPDOC"]?.ToString(),
                        NumId = reader["NUM_ID"]?.ToString(),
                        CodClase = reader["COD_CLASE"]?.ToString(),
                        FecNacimiento = Convert.ToDateTime(reader["FEC_NACIMIENTO"]),
                        // ... resto de campos
                    };
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return expediente;
        }

        public bool InsExpediente(Expediente expediente) => EjecutarProcedimiento("InsExpedientePA", expediente);
        public bool ModExpediente(Expediente expediente) => EjecutarProcedimiento("ModExpedientePA", expediente);
        public bool DelExpediente(int numExpediente) => EjecutarProcedimiento("DelExpedientePA", new Expediente { NumExpediente = numExpediente });

        private bool EjecutarProcedimiento(string procedimiento, Expediente expediente)
        {
            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand(procedimiento, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (procedimiento == "DelExpedientePA")
                {
                    cmd.Parameters.Add("P_NUM_EXPEDIENTE", OracleDbType.Int32).Value = expediente.NumExpediente;
                }
                else
                {
                    cmd.Parameters.Add("P_NUM_CARNET", OracleDbType.Varchar2).Value = expediente.NumCarnet;
                    cmd.Parameters.Add("P_PRIMER_AP", OracleDbType.Varchar2).Value = expediente.PrimerAp;
                    cmd.Parameters.Add("P_SEGUNDO_AP", OracleDbType.Varchar2).Value = expediente.SegundoAp;
                    cmd.Parameters.Add("P_PRIMER_NOM", OracleDbType.Varchar2).Value = expediente.PrimerNom;
                    cmd.Parameters.Add("P_SEGUNDO_NOM", OracleDbType.Varchar2).Value = expediente.SegundoNom;
                    cmd.Parameters.Add("P_SEXO", OracleDbType.Varchar2).Value = expediente.Sexo;
                    cmd.Parameters.Add("P_COD_TIPDOC", OracleDbType.Varchar2).Value = expediente.CodTipDoc;
                    cmd.Parameters.Add("P_NUM_ID", OracleDbType.Varchar2).Value = expediente.NumId;
                    cmd.Parameters.Add("P_COD_CLASE", OracleDbType.Varchar2).Value = expediente.CodClase;
                    cmd.Parameters.Add("P_FEC_NACIMIENTO", OracleDbType.Date).Value = expediente.FecNacimiento;
                    // ... agregar el resto de campos según necesidad

                    if (procedimiento == "ModExpedientePA")
                        cmd.Parameters.Add("P_NUM_EXPEDIENTE", OracleDbType.Int32).Value = expediente.NumExpediente;
                }

                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
        }
    }
}
