using System;
using System.Collections.Generic;
using System.Linq;
using ArcadiaTest.BusinessLayer.DTO;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.DataLayer.Entities;
using ArcadiaTest.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc.Internal;

namespace ArcadiaTest.BusinessLayer.Services
{
    public class CookService : ICookService
    {
        private IRestaurantRepository _restaurantRepository;
        private ICooksRepository _cooksRepository;
        private IQualificationRepository _qualificationRepository;

        public CookService(
            ICooksRepository cooksRepository,
            IRestaurantRepository restaurantRepository,
            IQualificationRepository qualificationRepository
            )
        {
            this._cooksRepository = cooksRepository;
            this._restaurantRepository = restaurantRepository;
            this._qualificationRepository = qualificationRepository;
        }
        
        public CookDTO GetCookWithId(int id)
        {
            var cook = this._cooksRepository.FindCookById(id);
            if (cook == null)
            {
                throw new CookNotFoundException(id);
            }
            return new CookDTO(cook);
        }

        public IEnumerable<CookDTO> GetAllCooks()
        {
            var cookEntities = this._cooksRepository.FindAll();
            var cookDTOs = new List<CookDTO>(cookEntities.Count());
            foreach (var entity in cookEntities)
            {
                cookDTOs.Add(new CookDTO(entity));
            }

            return cookDTOs;
        }

        public void CreateNewCook(string firstName, string secondName, string lastName, CookDTO.WorkdaysType workdays, List<CookDTO.QualificationsType> qualifications,
            CookDTO.ShiftType shift, short workdayLength, int restaurantId)
        {
            if (firstName == null || lastName == null || firstName == "" || lastName == ""
                || qualifications == null || !qualifications.Any())
            {
                throw new ArgumentException("null or empty required arguments");
            }
            var cook = new Cook
            {
                Name = firstName, SecondName = secondName, LastName = lastName,
                Workday = workdayLength, Workdays = workdays.ToNumber(), Shift = shift.ShiftCode(), RestaurantId = restaurantId
            };
            cook = this._cooksRepository.CreateNew(cook);
            foreach (var qualification in qualifications)
            {
                var qualEntity = qualification == CookDTO.QualificationsType.Russian
                    ?
                    this._qualificationRepository.FindRussianQualification()
                    : 
                    qualification == CookDTO.QualificationsType.Italian
                        ? this._qualificationRepository.FindItalianQualification()
                        :
                        this._qualificationRepository.FindJapaneseQualification();
                this._qualificationRepository.AddCookQualification(cook, qualEntity);
            }
        }

        public void UpdateCook(int id, string firstName, string secondName, string lastName, CookDTO.WorkdaysType workdays,
            List<CookDTO.QualificationsType> qualifications, CookDTO.ShiftType shift, short workdayLength, int restaurantId)
        {
            var cook = _cooksRepository.FindCookById(id);
            if (cook == null)
            {
                throw new CookNotFoundException(id);
            }

            if (cook.Name != firstName && !string.IsNullOrEmpty(firstName))
            {
                cook.Name = firstName;
            }
            if (cook.SecondName != secondName)
            {
                cook.SecondName = secondName;
            }
            
            if (cook.LastName != lastName && !string.IsNullOrEmpty(lastName))
            {
                cook.LastName = lastName;
            }

            if (cook.Shift != shift.ShiftCode())
            {
                cook.Shift = shift.ShiftCode();
            }

            if (cook.Workdays != workdays.ToNumber())
            {
                cook.Workdays = workdays.ToNumber();
            }
            
            if (cook.Workday != workdayLength)
            {
                cook.Workday = workdayLength;
            }

            if (cook.RestaurantId != restaurantId)
            {
                var restaurant = _restaurantRepository.FindById(restaurantId);
                if (restaurant == null)
                {
                    throw new RestaurantNotFoundException(restaurantId);
                }

                cook.RestaurantId = restaurantId;
                cook.Restaurant = restaurant;
            }

            this._cooksRepository.UpdateCook(cook);

            if (qualifications == null || qualifications.Count == 0)
            {
                return;
            }

            var qualSet = qualifications.ToHashSet();
            var qualEntities = new List<Qualification>(qualifications.Count);
            foreach (var qualification in qualifications)
            {
                var qualEntity = qualification == CookDTO.QualificationsType.Russian
                    ?
                    this._qualificationRepository.FindRussianQualification()
                    : 
                    qualification == CookDTO.QualificationsType.Italian
                        ? this._qualificationRepository.FindItalianQualification()
                        :
                        this._qualificationRepository.FindJapaneseQualification();
                qualEntities.Add(qualEntity);
            }
            this._qualificationRepository.UpdateCookQualification(id, qualEntities);
        }

        public void DeleteCook(int id)
        {
            var found = this._cooksRepository.FindCookById(id);
            if (found == null)
            {
                throw new CookNotFoundException(id);
            }
            this._cooksRepository.DeleteCook(found);
        }
    }
}