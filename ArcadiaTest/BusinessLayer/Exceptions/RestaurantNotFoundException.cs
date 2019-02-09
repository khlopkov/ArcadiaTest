using System;

namespace ArcadiaTest.BusinessLayer.Exceptions
{
    public class RestaurantNotFoundException : Exception
    {
        public RestaurantNotFoundException(int id) : base($"Restaurant with id {id} not found"){}
    }
}