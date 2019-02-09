using ArcadiaTest.DataLayer.Entities;

namespace ArcadiaTest.BusinessLayer.DTO
{
    public class RestaurantDTO
    {
        public int Id { get; }
        
        public string Title { get; }

        public RestaurantDTO(Restaurant restaurantEntity)
        {
            this.Id = restaurantEntity.Id;
            this.Title = restaurantEntity.Title;
        }
    }
}