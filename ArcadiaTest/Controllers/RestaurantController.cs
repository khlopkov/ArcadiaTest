using System.Collections.Generic;
using System.Linq;
using ArcadiaTest.BusinessLayer.DTO;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.BusinessLayer.Services;
using ArcadiaTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaTest.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ICookService _cookService;
        
        public RestaurantController(IRestaurantService restaurantService, ICookService cookService)
        {
            this._restaurantService = restaurantService;
            this._cookService = cookService;
        }

        public ActionResult Index()
        {
            var restaurants = _restaurantService.GetRestaurants();
            ViewBag.Restaurants = restaurants;
            return View();
        }

        [Route("restaurant/{id:int}")]
        public ActionResult SpecificRestaurant(int id)
        {
            RestaurantDTO requiredRestaurant;
            try
            {
                requiredRestaurant = _restaurantService.GetRestaurantWithId(id);
            }
            catch (RestaurantNotFoundException)
            {
                return NotFound("restaurant not found");
            }
            ViewBag.Restaurant = requiredRestaurant;
            var workers = _restaurantService.GetWorkers(id);
            ViewBag.Workers = workers;
            return View();
        }

        [HttpGet, Route("restaurant/{restaurantId:int}/add_cook")]
        public ActionResult AddCook(int restaurantId)
        {
            RestaurantDTO requiredRestaurant;
            try
            {
                requiredRestaurant = _restaurantService.GetRestaurantWithId(restaurantId);
            }
            catch (RestaurantNotFoundException)
            {
                return NotFound("restaurant not found");
            }
            ViewBag.Restaurant = requiredRestaurant;
            var model = new CreateCookRequest{AvailableQualifications = new List<string>{"russian", "italian", "japanese"}};
            return View(model);
        }

        [HttpPost, Route("restaurant/{restaurantId:int}/add_cook")]
        public ActionResult AddCook(int restaurantId, CreateCookRequest req)
        {
            try
            {
                _restaurantService.GetRestaurantWithId(restaurantId);
            }
            catch (RestaurantNotFoundException)
            {
                return NotFound("Restaurant not found");
            }
            
            if (!ModelState.IsValid)
            {
                var errField = ModelState.FirstOrDefault(f => ModelState[f.Key].Errors.Count != 0);
                return BadRequest(ModelState[errField.Key].Errors + errField.Key);
            }
            var shift = req.Shift == "evening" ? CookDTO.ShiftType.Evening : CookDTO.ShiftType.Morning;
            var workdays = req.Workdays == 5 ? CookDTO.WorkdaysType.Five : CookDTO.WorkdaysType.Two;
            var quals = new List<CookDTO.QualificationsType>(req.Qualification.Count);
            foreach (var qual in req.Qualification)
            {
                var addedQual = qual == "italian" ? CookDTO.QualificationsType.Italian :
                    qual == "russian" ? CookDTO.QualificationsType.Russian : CookDTO.QualificationsType.Japanese;
                quals.Add(addedQual);
            }
            this._cookService.CreateNewCook(req.FirstName,
                req.SecondName,
                req.LastName,
                workdays,
                quals,
                shift,
                req.WorkdayLength,
                restaurantId);
            return RedirectToAction("SpecificRestaurant", new {id = restaurantId});
        }

        [Route("restaurant/{restaurantId:int}/timetable")]
        public ActionResult Timetable(int restaurantId)
        {
            ViewBag.Timetable=this._restaurantService.GetTimetable(restaurantId);
            return View();
        }
    }
}