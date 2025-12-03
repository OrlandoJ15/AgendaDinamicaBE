using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DataAccess.Implementation
{
    public class EspecialidadesDA : IEspecialidadesDA
    {
        private readonly string _connection;

        public EspecialidadesDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        public async Task<List<Especialidad>> RecEspecialidadesxUsuarioAsync(int tipoAgenda, int usuarioId, int sucursal)
        {
            var list = new List<Especialidad>();

            await using var connection = new OracleConnection(_connection);
            await using var cmd = new OracleCommand("especialidades_consul_usuario", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("pnum_tipoagenda", OracleDbType.Int32).Value = tipoAgenda;
            cmd.Parameters.Add("pusuario_id", OracleDbType.Int32).Value = usuarioId;
            cmd.Parameters.Add("psucursal", OracleDbType.Int32).Value = sucursal;
            cmd.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            await connection.OpenAsync();

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Especialidad
                {
                    COD_ESPEC = reader["cod_espec"]?.ToString(),
                    DESC_ESPEC = reader["desc_espec"]?.ToString()
                });
            }

            return list;
        }
    }
}
