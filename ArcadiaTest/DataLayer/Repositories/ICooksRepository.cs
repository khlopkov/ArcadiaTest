using System.Collections.Generic;
using ArcadiaTest.DataLayer.Entities;

namespace ArcadiaTest.DataLayer.Repositories
{
    public interface ICooksRepository
    {
        IEnumerable<Cook> FindAll();
        Cook FindCookById(int id);
        IEnumerable<Cook> FindCooksByRestaurant(Restaurant restaurant);
        IEnumerable<Cook> FindCooksByRestaurant(int restaurantId);
        Cook CreateNew(Cook cook);
        Cook UpdateCook(Cook cook);
        void DeleteCook(Cook cook);
    }
}