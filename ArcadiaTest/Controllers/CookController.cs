using System.Collections.Generic;
using ArcadiaTest.BusinessLayer.DTO;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.BusinessLayer.Services;
using ArcadiaTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaTest.Controllers
{
    public class CookController : Controller
    {
        private ICookService _cookService;
        private IRestaurantService _restaurantService;

        public CookController(ICookService cookService, IRestaurantService restaurantService)
        {
            this._cookService = cookService;
            this._restaurantService = restaurantService;
        }
        
        public ActionResult Index()
        {
            var cooks = _cookService.GetAllCooks();
            ViewBag.Cooks = cooks;
            return View();
        }

        [Route("cook/{id:int}")]
        public ActionResult SpecificCook(int id)
        {
            CookDTO cook;
            try
            {
                cook = _cookService.GetCookWithId(id);

            }
            catch (CookNotFoundException e)
            {
                return NotFound("Cook not found");
            }
            ViewBag.Cook = cook;
            return View();
        }


        [HttpGet, Route("cook/{id:int}/update")]
        public ActionResult UpdateCook(int id)
        {
            CookDTO cook;
            try
            {
                cook = _cookService.GetCookWithId(id);

            }
            catch (CookNotFoundException e)
            {
                return NotFound("Cook not found");
            }
            ViewBag.Cook = cook;
            var restaurants = this._restaurantService.GetRestaurants();
            ViewBag.Restaurants = restaurants;
            return View();
        }

        [HttpPost, Route("cook/{id:int}/update")]
        public ActionResult UpdateCook(int id, UpdateCookRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var quals = new List<CookDTO.QualificationsType>(req.Qualification.Count);
                foreach (var qual in req.Qualification)
                {
                    var addedQual = qual == "italian" ? CookDTO.QualificationsType.Italian :
                        qual == "russian" ? CookDTO.QualificationsType.Russian : CookDTO.QualificationsType.Japanese;
                    quals.Add(addedQual);
                }
                var shift = req.Shift == "evening" ? CookDTO.ShiftType.Evening : CookDTO.ShiftType.Morning;

                var workdays = req.Workdays == 5 ? CookDTO.WorkdaysType.Five : CookDTO.WorkdaysType.Two;
                this._cookService.UpdateCook(id, req.FirstName, req.SecondName, req.LastName, workdays,
                    quals, shift, req.WorkdayLength, req.RestaurantId);

            }
            catch (CookNotFoundException)
            {
                return NotFound("Cook not found");
            }
            catch (RestaurantNotFoundException)
            {
                return NotFound("Restaurant not found");
            }

            return RedirectToAction("SpecificCook", new {id = id});
        }

        public ActionResult DeleteCook(int id)
        {
            try
            {
                this._cookService.DeleteCook(id);
            }
            catch (CookNotFoundException e)
            {
                return NotFound("Cook not found");
            }

            return RedirectToAction("Index");
        }
    }
}