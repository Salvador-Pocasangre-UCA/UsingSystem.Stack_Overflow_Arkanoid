using System;

namespace proyectoFinal_POO.Controlador
{
    public class ExceededMaxCharacterException : Exception
    {
        public ExceededMaxCharacterException(string Message) : base(Message) { }
    }
}
