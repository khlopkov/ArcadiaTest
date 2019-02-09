using System;
using System.Collections.Generic;
using System.Linq;
using ArcadiaTest.DataLayer.Entities;

namespace ArcadiaTest.DataLayer.Repositories
{
    public class QualificationRepository : IQualificationRepository
    {
        private readonly PostgresContext _dbCtx;

        public QualificationRepository()
        {
            this._dbCtx = new PostgresContext();
        }

        public Qualification FindRussianQualification()
        {
            return this._dbCtx.Qualifications
                .FirstOrDefault(q => q.Name == "russian");
        }

        public Qualification FindItalianQualification()
        {
            return this._dbCtx.Qualifications
                .FirstOrDefault(q => q.Name == "italian");
        }

        public Qualification FindJapaneseQualification()
        {
            return this._dbCtx.Qualifications
                .FirstOrDefault(q => q.Name == "japanese");
        }

        public IEnumerable<Qualification> FindAll()
        {
            return this._dbCtx.Qualifications
                .ToList();
        }

        public CookQualifications AddCookQualification(Cook cook, Qualification qual)
        {
            return this._dbCtx.CookQualifications
                .Add(new CookQualifications {CookId = cook.Id, QualificationId = qual.Id}).Entity;
        }

        public void UpdateCookQualification(int cookId, IEnumerable<Qualification> qualifications)
        {
            using (var tx = this._dbCtx.Database.BeginTransaction())
            {
                try
                {
                    _dbCtx.CookQualifications
                        .RemoveRange(_dbCtx.CookQualifications.
                            Where(q => q.CookId == cookId));
                    _dbCtx.SaveChanges();
                    
                    foreach (var qual in qualifications)
                    {
                        _dbCtx.CookQualifications.Add(new CookQualifications
                            {CookId = cookId, QualificationId = qual.Id});
                    }
                    _dbCtx.SaveChanges();
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}