using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace AccesoDatos.DBContext
{
    public class HCBContext : IDisposable
    {
        private readonly OracleConnection _connection;

        // Constructor recibe la cadena de conexión (puede venir del appsettings.json)
        public HCBContext(string connectionString)
        {
            _connection = new OracleConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }

        // Crea un comando listo para ejecutar
        public OracleCommand CreateCommand(string sql, CommandType type = CommandType.Text)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = type;
            return cmd;
        }

        // Ejecuta un SELECT y devuelve un DataTable
        public DataTable ExecuteQuery(string sql, params OracleParameter[] parameters)
        {
            var table = new DataTable();

            using (var cmd = CreateCommand(sql))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                OpenConnection();

                using (var adapter = new OracleDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

                CloseConnection();
            }

            return table;
        }

        // Ejecuta un INSERT, UPDATE o DELETE
        public int ExecuteNonQuery(string sql, params OracleParameter[] parameters)
        {
            using (var cmd = CreateCommand(sql))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                OpenConnection();
                int rows = cmd.ExecuteNonQuery();
                CloseConnection();

                return rows;
            }
        }

        // Liberar recursos
        public void Dispose()
        {
            CloseConnection();
            _connection.Dispose();
        }
    }
}
