using System;

namespace proyectoFinal_POO.Controlador
{
    public class EmptyNicknameException : Exception
    {
        public EmptyNicknameException(string Message) : base(Message) { }
    }
}
