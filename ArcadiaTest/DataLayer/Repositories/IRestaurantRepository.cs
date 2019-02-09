using System.Collections.Generic;
using ArcadiaTest.DataLayer.Entities;

namespace ArcadiaTest.DataLayer.Repositories
{
    public interface IRestaurantRepository
    {
        Restaurant FindById(int id);
        IEnumerable<Restaurant> FindAll();
    }
}