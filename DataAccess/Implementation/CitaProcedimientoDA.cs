using Entities.Models;
using CommonMethods;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementation
{
    public class CitaProcedimientoDA : ICitaProcedimientoDA
    {
        private readonly string _connection;
        public Exceptions gObjExceptions = new Exceptions();

        public CitaProcedimientoDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<CitaProcedimiento> RecCitaProcedimientos()
        {
            var lista = new List<CitaProcedimiento>();

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecCitaProcedimientosPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(MapearCitaProcedimiento(reader));
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public CitaProcedimiento? RecCitaProcedimientoXId(int numCita, string codArticulo)
        {
            CitaProcedimiento? citaProc = null;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand("RecCitaProcedimientoXIdPA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("P_NUM_CITA", OracleDbType.Int32).Value = numCita;
                cmd.Parameters.Add("P_COD_ARTICULO", OracleDbType.Varchar2).Value = codArticulo;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    citaProc = MapearCitaProcedimiento(reader);
                }
            }
            catch (Exception ex)
            {
                gObjExceptions.LogError(ex);
                throw;
            }

            return citaProc;
        }

        public bool InsCitaProcedimiento(CitaProcedimiento citaProc)
        {
            return EjecutarProcedimiento("InsCitaProcedimientoPA", citaProc);
        }

        public bool ModCitaProcedimiento(CitaProcedimiento citaProc)
        {
            return EjecutarProcedimiento("ModCitaProcedimientoPA", citaProc);
        }

        public bool DelCitaProcedimiento(int numCita, string codArticulo)
        {
            return EjecutarProcedimiento("DelCitaProcedimientoPA", new CitaProcedimiento
            {
                NUM_CITA = numCita,
                COD_ARTICULO = codArticulo
            });
        }

        // =======================================================
        // MÉTODO CENTRALIZADO PARA EJECUTAR SP
        // =======================================================
        private bool EjecutarProcedimiento(string procedimiento, CitaProcedimiento citaProc)
        {
            bool resultado = false;

            try
            {
                using var connection = new OracleConnection(_connection);
                using var cmd = new OracleCommand(procedimiento, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (procedimiento == "DelCitaProcedimientoPA")
                {
                    cmd.Parameters.Add("P_NUM_CITA", OracleDbType.Int32).Value = citaProc.NUM_CITA;
                    cmd.Parameters.Add("P_COD_ARTICULO", OracleDbType.Varchar2).Value = citaProc.COD_ARTICULO;
                }
                else
                {
                    cmd.Parameters.Add("P_NUM_CITA", OracleDbType.Int32).Value = citaProc.NUM_CITA;
                    cmd.Parameters.Add("P_COD_ARTICULO", OracleDbType.Varchar2).Value = citaProc.COD_ARTICULO;
                    cmd.Parameters.Add("P_DES_DETALLE", OracleDbType.Varchar2).Value = citaProc.DES_DETALLE;

                    if (procedimiento == "ModCitaProcedimientoPA")
                    {
                        // Si el SP de modificación requiere una clave primaria compuesta,
                        // ya está incluida con NUM_CITA y COD_ARTICULO.
                    }
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
        private CitaProcedimiento MapearCitaProcedimiento(OracleDataReader reader)
        {
            return new CitaProcedimiento
            {
                NUM_CITA = Convert.ToInt32(reader["NUM_CITA"]),
                COD_ARTICULO = reader["COD_ARTICULO"]?.ToString() ?? string.Empty,
                DES_DETALLE = reader["DES_DETALLE"]?.ToString() ?? string.Empty
            };
        }
    }
}
