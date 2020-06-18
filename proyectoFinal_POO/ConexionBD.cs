using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace proyectoFinal_POO
{
    static class ConexionBD
    {
        private static string host = "127.0.0.1",
            database = "Arkanoid",
            userId = "postgres",
            password = "CONTRABASE";
        //public static string CadenaConexion = 
        //    "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=bddPedidos;";

        private static string CadenaConexion = $"Server={host}; port=5432; User Id={userId};Password={password};" +
                                               $"Database={database}";

        public static DataTable realizarConsulta(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(CadenaConexion);
            DataSet ds = new DataSet();

            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();

            return ds.Tables[0];
        }

        public static void realizarAccion(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(CadenaConexion);

            conn.Open();
            NpgsqlCommand nc = new NpgsqlCommand(sql, conn);
            nc.ExecuteNonQuery();
            conn.Close();
        }
    }
}
