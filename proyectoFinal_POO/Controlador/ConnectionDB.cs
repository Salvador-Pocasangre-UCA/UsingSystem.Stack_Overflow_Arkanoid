using Npgsql;
using System.Data;

namespace proyectoFinal_POO
{
    static class ConnectionDB
    {
        private static string host = "127.0.0.1",
            database = "bdd_Arkanoid",
            userId = "postgres",
            password = "uca";
        //public static string CadenaConexion = 
        //    "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=bddPedidos;";

        private static string ConnectionS = $"Server={host}; port=5432; User Id={userId};Password={password};" +
                                               $"Database={database}";

        public static DataTable ExecuteQuery(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionS);
            DataSet ds = new DataSet();

            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();

            return ds.Tables[0];
        }

        public static void ExecuteNonQuery(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionS);

            conn.Open();
            NpgsqlCommand nc = new NpgsqlCommand(sql, conn);
            nc.ExecuteNonQuery();
            conn.Close();
        }
    }
}
