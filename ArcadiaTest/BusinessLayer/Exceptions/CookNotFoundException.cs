using System;

namespace ArcadiaTest.BusinessLayer.Exceptions
{
    public class CookNotFoundException : Exception
    {
        public CookNotFoundException(int id) : base($"Cook with id {id} not found")
        {
        }
    }
}