using System;

namespace proyectoFinal_POO.Controlador
{
    public class NoRemainingLivesException : Exception
    {
        public NoRemainingLivesException(string Message) : base(Message) { }
    }
}
