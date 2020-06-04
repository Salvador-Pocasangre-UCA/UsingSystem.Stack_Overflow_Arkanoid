﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoFinal_POO
{
    class ScoreDAO
    {
        public static List<Score> getScoreList()
        {
            string sql = "SELECT p.usuario, max(pu.puntaje) puntaje " +
                          "FROM public.jugador p inner join public.puntaje pu " +
                          "on p.usuario=pu.usuario " +
                          "group by p.usuario " +
                          "order by max(pu.puntaje) desc " +
                          "fetch first 10 rows only";

            var dt = ConexionBD.realizarConsulta(sql);
            List<Score> lista = new List<Score>();

            foreach (DataRow fila in dt.Rows)
            {
                Score s = new Score();
                s.name = fila[0].ToString();
                s.score = Convert.ToInt32(fila[1].ToString());
                lista.Add(s);
            }

            return lista;
        }
    }
}