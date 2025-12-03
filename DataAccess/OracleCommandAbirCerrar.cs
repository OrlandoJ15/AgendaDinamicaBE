using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess
{
    public class OracleCommandAbrirCerrar
    {
        private readonly string _connection;

        // Recibe la cadena de conexión (puede venir del appsettings.json o del constructor del repositorio)
        public OracleCommandAbrirCerrar(string connectionString)
        {
            _connection = connectionString;
        }

        /// <summary>
        /// Crea y abre un OracleCommand asociado a un procedimiento almacenado.
        /// </summary>
        public OracleCommand CrearComando(string procedimientoAlmacenado)
        {
            var connection = new OracleConnection(_connection);
            var command = new OracleCommand(procedimientoAlmacenado, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            return command;
        }

        /// <summary>
        /// Cierra y limpia los recursos del comando y la conexión.
        /// </summary>
        public void CerrarConexion(OracleCommand command)
        {
            if (command == null) return;

            if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                command.Connection.Close();

            command.Dispose();
        }
    }
}
