using Oracle.ManagedDataAccess.Client;

namespace AgendaHCB.Services
{
    public class OracleService
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public OracleService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OracleDb");
        }

        public async Task<List<Dictionary<string, object>>> EjecutarQueryAsync(string query)
        {
            var resultado = new List<Dictionary<string, object>>();

            using var conexion = new OracleConnection(_connectionString);
            await conexion.OpenAsync();

            using var comando = new OracleCommand(query, conexion);
            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var fila = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    fila[reader.GetName(i)] = reader.GetValue(i);
                resultado.Add(fila);
            }

            return resultado;
        }
    }
}
