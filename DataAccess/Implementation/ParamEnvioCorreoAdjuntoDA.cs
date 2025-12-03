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
    public class ParamEnvioCorreoAdjuntoDA : IParamEnvioCorreoAdjuntoDA
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly Exceptions _excepciones = new Exceptions();

        public ParamEnvioCorreoAdjuntoDA(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OracleConnection");
        }

        public IEnumerable<ParamEnvioCorreoAdjunto> ObtenerAdjuntos()
        {
            var lista = new List<ParamEnvioCorreoAdjunto>();

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_PARAMENVIOCORREO_ADJUNTO.SP_OBTENER_TODOS", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ParamEnvioCorreoAdjunto
                        {
                            NOMBRE = reader["NOMBRE"].ToString(),
                            CONTENIDO = reader["CONTENIDO"].ToString().Split(',')
                        });
                    }
                }
            }

            return lista;
        }

        public ParamEnvioCorreoAdjunto ObtenerAdjuntoPorNombre(string nombre)
        {
            ParamEnvioCorreoAdjunto adjunto = null;

            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_PARAMENVIOCORREO_ADJUNTO.SP_OBTENER_POR_NOMBRE", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_NOMBRE", OracleDbType.Varchar2).Value = nombre;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        adjunto = new ParamEnvioCorreoAdjunto
                        {
                            NOMBRE = reader["NOMBRE"].ToString(),
                            CONTENIDO = reader["CONTENIDO"].ToString().Split(',')
                        };
                    }
                }
            }

            return adjunto;
        }

        public bool InsertarAdjunto(ParamEnvioCorreoAdjunto adjunto)
        {
            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_PARAMENVIOCORREO_ADJUNTO.SP_INSERTAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_NOMBRE", OracleDbType.Varchar2).Value = adjunto.NOMBRE;
                cmd.Parameters.Add("P_CONTENIDO", OracleDbType.Varchar2).Value = string.Join(",", adjunto.CONTENIDO);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarAdjunto(string nombre)
        {
            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand("PKG_PARAMENVIOCORREO_ADJUNTO.SP_ELIMINAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_NOMBRE", OracleDbType.Varchar2).Value = nombre;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
