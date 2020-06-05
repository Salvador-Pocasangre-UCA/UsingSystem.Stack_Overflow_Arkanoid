using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoFinal_POO
{
    public static class JugadorDAO
    {
        public static List<Jugador> getLista()
        {
            string sql = "select * from jugador";

            DataTable dt = ConexionBD.realizarConsulta(sql);

            List<Jugador> lista = new List<Jugador>();
            foreach (DataRow fila in dt.Rows)
            {
                Jugador u = new Jugador();
                u.usuario = fila[0].ToString();

                lista.Add(u);
            }
            return lista;
        }

        public static void crearNuevo(string usuario)
        {
            string sql = String.Format(
                "insert into jugador values('{0}');", usuario);

            ConexionBD.realizarAccion(sql);
        }

    }
}
