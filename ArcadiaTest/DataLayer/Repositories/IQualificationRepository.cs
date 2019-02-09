using System.Collections.Generic;
using ArcadiaTest.DataLayer.Entities;

namespace ArcadiaTest.DataLayer.Repositories
{
    public interface IQualificationRepository
    {
        Qualification FindRussianQualification();
        Qualification FindItalianQualification();
        Qualification FindJapaneseQualification();
        IEnumerable<Qualification> FindAll();
        CookQualifications AddCookQualification(Cook cook, Qualification qual);

        void UpdateCookQualification(int cookId, IEnumerable<Qualification> qualifications);
    }
}