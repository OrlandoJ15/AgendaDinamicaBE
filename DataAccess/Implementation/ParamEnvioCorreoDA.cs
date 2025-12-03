using Entities.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using DataAccess.Interfaces;
using CommonMethods;

namespace DataAccess.Implementation
{
    public class ParamEnvioCorreoDA : IParamEnvioCorreoDA
    {
        private readonly IConfiguration _configuration;
        private readonly OracleConnection _connection;

        public ParamEnvioCorreoDA(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new OracleConnection(_configuration.GetConnectionString("OracleConnection"));
        }

        public List<ParamEnvioCorreo> RecParamsEnvioCorreo()
        {
            var lista = new List<ParamEnvioCorreo>();
            string query = "SELECT COMPAÑIA, DIRECCION_ENVIO, DESTINO, ASUNTO, MENSAJE, DIRECTORIO, ES_HTML, MODULO FROM PARAM_ENVIOCORREO";

            using (var cmd = new OracleCommand(query, _connection))
            {
                cmd.CommandType = CommandType.Text;
                _connection.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ParamEnvioCorreo
                        {
                            COMPAÑIA = dr["COMPAÑIA"].ToString(),
                            DIRECCION_ENVIO = dr["DIRECCION_ENVIO"].ToString(),
                            DESTINO = dr["DESTINO"].ToString(),
                            ASUNTO = dr["ASUNTO"].ToString(),
                            MENSAJE = dr["MENSAJE"].ToString(),
                            DIRECTORIO = dr["DIRECTORIO"].ToString(),
                            ES_HTML = dr["ES_HTML"].ToString(),
                            MODULO = dr["MODULO"].ToString()
                        });
                    }
                }

                _connection.Close();
            }

            return lista;
        }

        public ParamEnvioCorreo RecParamEnvioCorreoXId(string compania)
        {
            ParamEnvioCorreo param = null;
            string query = "SELECT * FROM PARAM_ENVIOCORREO WHERE COMPAÑIA = :COMPAÑIA";

            using (var cmd = new OracleCommand(query, _connection))
            {
                cmd.Parameters.Add(":COMPAÑIA", compania);
                _connection.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        param = new ParamEnvioCorreo
                        {
                            COMPAÑIA = dr["COMPAÑIA"].ToString(),
                            DIRECCION_ENVIO = dr["DIRECCION_ENVIO"].ToString(),
                            DESTINO = dr["DESTINO"].ToString(),
                            ASUNTO = dr["ASUNTO"].ToString(),
                            MENSAJE = dr["MENSAJE"].ToString(),
                            DIRECTORIO = dr["DIRECTORIO"].ToString(),
                            ES_HTML = dr["ES_HTML"].ToString(),
                            MODULO = dr["MODULO"].ToString()
                        };
                    }
                }

                _connection.Close();
            }

            return param;
        }

        public void InsParamEnvioCorreo(ParamEnvioCorreo param)
        {
            string query = @"INSERT INTO PARAM_ENVIOCORREO 
                            (COMPAÑIA, DIRECCION_ENVIO, DESTINO, ASUNTO, MENSAJE, DIRECTORIO, ES_HTML, MODULO)
                            VALUES (:COMPAÑIA, :DIRECCION_ENVIO, :DESTINO, :ASUNTO, :MENSAJE, :DIRECTORIO, :ES_HTML, :MODULO)";

            using (var cmd = new OracleCommand(query, _connection))
            {
                cmd.Parameters.Add(":COMPAÑIA", param.COMPAÑIA);
                cmd.Parameters.Add(":DIRECCION_ENVIO", param.DIRECCION_ENVIO);
                cmd.Parameters.Add(":DESTINO", param.DESTINO);
                cmd.Parameters.Add(":ASUNTO", param.ASUNTO);
                cmd.Parameters.Add(":MENSAJE", param.MENSAJE);
                cmd.Parameters.Add(":DIRECTORIO", param.DIRECTORIO);
                cmd.Parameters.Add(":ES_HTML", param.ES_HTML);
                cmd.Parameters.Add(":MODULO", param.MODULO);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void ModParamEnvioCorreo(ParamEnvioCorreo param)
        {
            string query = @"UPDATE PARAM_ENVIOCORREO 
                             SET DIRECCION_ENVIO = :DIRECCION_ENVIO,
                                 DESTINO = :DESTINO,
                                 ASUNTO = :ASUNTO,
                                 MENSAJE = :MENSAJE,
                                 DIRECTORIO = :DIRECTORIO,
                                 ES_HTML = :ES_HTML,
                                 MODULO = :MODULO
                             WHERE COMPAÑIA = :COMPAÑIA";

            using (var cmd = new OracleCommand(query, _connection))
            {
                cmd.Parameters.Add(":DIRECCION_ENVIO", param.DIRECCION_ENVIO);
                cmd.Parameters.Add(":DESTINO", param.DESTINO);
                cmd.Parameters.Add(":ASUNTO", param.ASUNTO);
                cmd.Parameters.Add(":MENSAJE", param.MENSAJE);
                cmd.Parameters.Add(":DIRECTORIO", param.DIRECTORIO);
                cmd.Parameters.Add(":ES_HTML", param.ES_HTML);
                cmd.Parameters.Add(":MODULO", param.MODULO);
                cmd.Parameters.Add(":COMPAÑIA", param.COMPAÑIA);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DelParamEnvioCorreo(string compania)
        {
            string query = "DELETE FROM PARAM_ENVIOCORREO WHERE COMPAÑIA = :COMPAÑIA";

            using (var cmd = new OracleCommand(query, _connection))
            {
                cmd.Parameters.Add(":COMPAÑIA", compania);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
