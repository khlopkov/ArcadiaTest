using System.Collections.Generic;
using System.Linq;
using ArcadiaTest.BusinessLayer.DTO;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.DataLayer.Repositories;

namespace ArcadiaTest.BusinessLayer.Services
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository _restaurantRepository;
        private ICooksRepository _cooksRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository,
            ICooksRepository cooksRepository)
        {
            this._restaurantRepository = restaurantRepository;
            this._cooksRepository = cooksRepository;
        }
        
        public IEnumerable<RestaurantDTO> GetRestaurants()
        {
            var resaurantEntities = this._restaurantRepository.FindAll();
            var restaurantDTOs = new List<RestaurantDTO>(resaurantEntities.Count());
            foreach (var entity in resaurantEntities)
            {
                restaurantDTOs.Add(new RestaurantDTO(entity));
            }

            return restaurantDTOs;
        }

        public RestaurantDTO GetRestaurantWithId(int id)
        {
            var found = this._restaurantRepository.FindById(id);
            if (found == null)
            {
                throw new RestaurantNotFoundException(id);
            }
            return new RestaurantDTO(found);
        }

        public IEnumerable<CookDTO> GetWorkers(int restaurantId)
        {
            var cookEntities = this._cooksRepository.FindCooksByRestaurant(restaurantId);
            var cookDTOs = new List<CookDTO>(cookEntities.Count());
            foreach (var entity in cookEntities)
            {
                cookDTOs.Add(new CookDTO(entity));
            }

            return cookDTOs;
        }

        public IEnumerable<IEnumerable<DayGraphic>> GetTimetable(int restaurantId)
        {
            var workers = this.GetWorkers(restaurantId);
            return new Timetable(workers).GetTimetable();
        }
    }
}