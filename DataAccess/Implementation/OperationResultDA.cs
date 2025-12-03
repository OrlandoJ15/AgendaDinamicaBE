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
    public class OperationResultDA : IOperationResultDA
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly Exceptions _excepciones = new Exceptions();

        public OperationResultDA(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OracleConnection");
        }

        public IEnumerable<OperationResult> ObtenerResultados()
        {
            var lista = new List<OperationResult>();

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_OPERATIONRESULT.SP_OBTENER_TODOS", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new OperationResult
                        {
                            Code = Convert.ToInt32(reader["CODE"]),
                            Message = reader["MESSAGE"].ToString(),
                            Data = reader["DATA"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public OperationResult ObtenerResultadoPorCodigo(int code)
        {
            OperationResult result = null;

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_OPERATIONRESULT.SP_OBTENER_POR_CODIGO", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CODE", OracleDbType.Int32).Value = code;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new OperationResult
                        {
                            Code = Convert.ToInt32(reader["CODE"]),
                            Message = reader["MESSAGE"].ToString(),
                            Data = reader["DATA"].ToString()
                        };
                    }
                }
            }

            return result;
        }

        public bool InsertarResultado(OperationResult result)
        {
            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_OPERATIONRESULT.SP_INSERTAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CODE", OracleDbType.Int32).Value = result.Code;
                cmd.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2).Value = result.Message;
                cmd.Parameters.Add("P_DATA", OracleDbType.Varchar2).Value = result.Data?.ToString();

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarResultado(int code)
        {
            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_OPERATIONRESULT.SP_ELIMINAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CODE", OracleDbType.Int32).Value = code;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
