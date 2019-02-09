using System.Collections.Generic;
using System.Linq;
using ArcadiaTest.DataLayer.Entities;

namespace ArcadiaTest.DataLayer.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly PostgresContext _dbCtx;

        public RestaurantRepository()
        {
            this._dbCtx = new PostgresContext();
        }

        public Restaurant FindById(int id)
        {
            return this._dbCtx.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> FindAll()
        {
            return this._dbCtx.Restaurants.ToList();
        }
    }
}