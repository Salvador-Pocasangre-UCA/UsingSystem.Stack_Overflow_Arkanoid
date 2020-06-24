using System;
using System.Collections.Generic;
using System.Data;

namespace proyectoFinal_POO
{
    class ScoreController
    {
        public static List<Score> GetScoreList()
        {
            string sql = "SELECT p.usuario, pu.puntaje puntaje " +
                          "FROM public.jugador p inner join public.puntaje pu " +
                          "on p.usuario=pu.usuario " +
                          "order by pu.puntaje desc " +
                          "fetch first 10 rows only";

            var dt = ConnectionDB.ExecuteQuery(sql);
            List<Score> list = new List<Score>();

            foreach (DataRow row in dt.Rows)
            {
                Score s = new Score
                {
                    Name = row[0].ToString(),
                    Scores = Convert.ToInt32(row[1].ToString())
                };
                list.Add(s);
            }

            return list;
        }

        public static void CreateScore(string User, int Score)
        {
            string sql = String.Format(
                "insert into puntaje(usuario, puntaje) " +
                "values('{0}', {1});",
                User, Score);

            ConnectionDB.ExecuteNonQuery(sql);
        }
    }
}
