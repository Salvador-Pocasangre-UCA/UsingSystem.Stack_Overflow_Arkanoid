using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoFinal_POO.Controlador
{
    public class WrongKeyPressedException : Exception
    {
        public WrongKeyPressedException(string Message) : base(Message) { }
    }
}
