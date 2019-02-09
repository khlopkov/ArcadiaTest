using System.Collections.Generic;
using System.Linq;
using ArcadiaTest.DataLayer.Entities;

namespace ArcadiaTest.DataLayer.Repositories
{
    public class CooksRepository : ICooksRepository
    {
        private readonly PostgresContext _dbCtx;

        public CooksRepository()
        {
            this._dbCtx = new PostgresContext();
        }

        public IEnumerable<Cook> FindAll()
        {
            return this._dbCtx.Cooks
                .Select(c => new Cook
                {
                    Id = c.Id,
                    LastName = c.LastName,
                    Name = c.Name,
                    RestaurantId = c.RestaurantId,
                    Restaurant = c.Restaurant,
                    SecondName = c.SecondName,
                    Shift = c.Shift,
                    Workday = c.Workday,
                    Workdays = c.Workdays,
                    CookQualifications = this._dbCtx.CookQualifications
                        .Where(cq => cq.CookId == c.Id)
                        .Join(this._dbCtx.Qualifications,
                            cq => cq.QualificationId,
                            q => q.Id,
                            (cq, q) => new CookQualifications
                            {
                                Cook = c,
                                CookId = cq.CookId,
                                Qualification = q,
                                QualificationId = q.Id
                            })
                        .ToList()
                })
                .ToList();
        }

        public Cook FindCookById(int id)
        {
            var cooks = this._dbCtx.Cooks;
            return cooks
                .Select(c => new Cook
                {
                    Id = c.Id,
                    LastName = c.LastName,
                    Name = c.Name,
                    RestaurantId = c.RestaurantId,
                    Restaurant = c.Restaurant,
                    SecondName = c.SecondName,
                    Shift = c.Shift,
                    Workday = c.Workday,
                    Workdays = c.Workdays,
                    CookQualifications = this._dbCtx.CookQualifications
                        .Where(cq => cq.CookId == c.Id)
                        .Join(this._dbCtx.Qualifications,
                            cq => cq.QualificationId,
                            q => q.Id,
                            (cq, q) => new CookQualifications
                            {
                                Cook = c,
                                CookId = cq.CookId,
                                Qualification = q,
                                QualificationId = q.Id
                            })
                        .ToList()
                })
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Cook> FindCooksByRestaurant(Restaurant restaurant)
        {
            var cooks = this._dbCtx.Cooks;
            return cooks
                .Select(c => new Cook
                {
                    Id = c.Id,
                    LastName = c.LastName,
                    Name = c.Name,
                    RestaurantId = c.RestaurantId,
                    Restaurant = c.Restaurant,
                    SecondName = c.SecondName,
                    Shift = c.Shift,
                    Workday = c.Workday,
                    Workdays = c.Workdays,
                    CookQualifications = this._dbCtx.CookQualifications
                        .Where(cq => cq.CookId == c.Id)
                        .Join(this._dbCtx.Qualifications,
                            cq => cq.QualificationId,
                            q => q.Id,
                            (cq, q) => new CookQualifications
                            {
                                Cook = c,
                                CookId = cq.CookId,
                                Qualification = q,
                                QualificationId = q.Id
                            })
                        .ToList()
                })
                .Where(c => c.RestaurantId == restaurant.Id)
                .ToList();
        }

        public IEnumerable<Cook> FindCooksByRestaurant(int restaurantId)
        {
            return this._dbCtx.Cooks
                .Select(c => new Cook
                {
                    Id = c.Id,
                    LastName = c.LastName,
                    Name = c.Name,
                    RestaurantId = c.RestaurantId,
                    Restaurant = c.Restaurant,
                    SecondName = c.SecondName,
                    Shift = c.Shift,
                    Workday = c.Workday,
                    Workdays = c.Workdays,
                    CookQualifications = this._dbCtx.CookQualifications
                        .Where(cq => cq.CookId == c.Id)
                        .Join(this._dbCtx.Qualifications,
                            cq => cq.QualificationId,
                            q => q.Id,
                            (cq, q) => new CookQualifications
                            {
                                Cook = c,
                                CookId = cq.CookId,
                                Qualification = q,
                                QualificationId = q.Id
                            })
                        .ToList()
                })
                .Where(c => c.RestaurantId == restaurantId)
                .ToList();
        }

        public Cook CreateNew(Cook cook)
        {
            var inserted =  this._dbCtx.Cooks.Add(cook).Entity;
            this._dbCtx.SaveChanges();
            return inserted;
        }

        public Cook UpdateCook(Cook cook)
        {
            var updated = this._dbCtx.Update(cook).Entity;
            this._dbCtx.SaveChanges();
            return updated;
        }

        public void DeleteCook(Cook cook)
        {
            this._dbCtx.Cooks.Remove(cook);
            this._dbCtx.SaveChanges();
        }
    }
}