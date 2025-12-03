using Entities.Models;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using CommonMethods;
using System.Data;
using System;
using System.Collections.Generic;

namespace DataAccess.Implementation
{
    public class ProfesionalDA : IProfesionalDA
    {
        private readonly string _connection;
        private readonly Exceptions _exceptions;

        public ProfesionalDA(IConfiguration configuration, Exceptions exceptions)
        {
            _connection = configuration.GetConnectionString("OracleDb");
            _exceptions = exceptions;
        }

        public List<Profesional> RecProfesionales()
        {
            var lista = new List<Profesional>();
            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecProfesionalesPA", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Profesional
                    {
                        CodProf = reader["COD_PROF"]?.ToString(),
                        NomProf = reader["NOM_PROF"]?.ToString(),
                        Estado = reader["ESTADO"]?.ToString(),
                        IndSic = reader["IND_SIC"]?.ToString(),
                        Tipo = reader["TIPO"]?.ToString(),
                        CodSociedad = reader["COD_SOCIEDAD"]?.ToString(),
                        CodEspec = reader["COD_ESPEC"]?.ToString(),
                        // ...otros campos que necesites mapear
                        FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : Convert.ToDateTime(reader["FEC_ACTUALIZA"])
                    });
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
            return lista;
        }

        public Profesional? RecProfesionalXCod(string codProf)
        {
            Profesional? prof = null;
            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecProfesionalXCod", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_COD_PROF", OracleDbType.Varchar2).Value = codProf;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    prof = new Profesional
                    {
                        CodProf = reader["COD_PROF"]?.ToString(),
                        NomProf = reader["NOM_PROF"]?.ToString(),
                        Estado = reader["ESTADO"]?.ToString(),
                        IndSic = reader["IND_SIC"]?.ToString()
                        // ...otros campos
                    };
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }
            return prof;
        }

        public bool InsProfesional(Profesional prof) => EjecutarProcedimiento("InsProfesionalPA", prof);
        public bool ModProfesional(Profesional prof) => EjecutarProcedimiento("ModProfesionalPA", prof);
        public bool DelProfesional(string codProf) => EjecutarProcedimiento("DelProfesionalPA", new Profesional { CodProf = codProf });

        private bool EjecutarProcedimiento(string procedimiento, Profesional prof)
        {
            bool resultado = false;
            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand(procedimiento, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros genéricos
                if (procedimiento == "DelProfesionalPA")
                {
                    cmd.Parameters.Add("P_COD_PROF", OracleDbType.Varchar2).Value = prof.CodProf;
                }
                else
                {
                    cmd.Parameters.Add("P_COD_PROF", OracleDbType.Varchar2).Value = prof.CodProf;
                    cmd.Parameters.Add("P_NOM_PROF", OracleDbType.Varchar2).Value = prof.NomProf;
                    cmd.Parameters.Add("P_ESTADO", OracleDbType.Varchar2).Value = prof.Estado;
                    // ... otros parámetros según SP

                    if (procedimiento == "ModProfesionalPA")
                        cmd.Parameters.Add("P_COD_PROF", OracleDbType.Varchar2).Value = prof.CodProf;
                }

                connection.Open();
                resultado = cmd.ExecuteNonQuery() > 0;
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
