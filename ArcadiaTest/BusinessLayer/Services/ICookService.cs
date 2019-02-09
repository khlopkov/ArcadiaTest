using System.Collections.Generic;
using ArcadiaTest.BusinessLayer.DTO;

namespace ArcadiaTest.BusinessLayer.Services
{
    public interface ICookService
    {
        CookDTO GetCookWithId(int id);
        IEnumerable<CookDTO> GetAllCooks();
        void CreateNewCook(string firstName, string secondName, string lastName,
            CookDTO.WorkdaysType workdays, List<CookDTO.QualificationsType> qualifications,
            CookDTO.ShiftType shift, short workdayLength, int restaurantId);

        void UpdateCook(int id, string firstName, string secondName, string lastName,
            CookDTO.WorkdaysType workdays, List<CookDTO.QualificationsType> qualifications,
            CookDTO.ShiftType shift, short workdayLength, int restaurantId);

        void DeleteCook(int id);
    }
}