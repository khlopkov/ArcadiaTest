using System.Collections.Generic;
using ArcadiaTest.BusinessLayer.DTO;

namespace ArcadiaTest.BusinessLayer.Services
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantDTO> GetRestaurants();
        RestaurantDTO GetRestaurantWithId(int id);
        //TODO change from void
        IEnumerable<CookDTO> GetWorkers(int restaurantId);
        IEnumerable<IEnumerable<DayGraphic>> GetTimetable(int restaurantId);
    }
}