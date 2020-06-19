using System;

namespace proyectoFinal_POO.Controlador
{
    public class OutOfBoundsException : Exception
    {
        public OutOfBoundsException(string Message) : base (Message) { }
    }
}
