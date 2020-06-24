using System;
using System.Collections.Generic;
using System.Data;

namespace proyectoFinal_POO
{
    public static class PlayerController
    {
        public static List<Player> GetList()
        {
            string sql = "select * from jugador";

            DataTable dt = ConnectionDB.ExecuteQuery(sql);

            List<Player> list = new List<Player>();
            foreach (DataRow row in dt.Rows)
            {
                Player u = new Player
                {
                    Username = row[0].ToString()
                };

                list.Add(u);
            }
            return list;
        }

        public static void CreatePlayer(string username)
        {
            string sql = String.Format(
                "insert into jugador values('{0}');", username);

            ConnectionDB.ExecuteNonQuery(sql);
        }

    }
}
