using System;
using System.Collections.Generic;
using ArcadiaTest.DataLayer.Entities;


namespace ArcadiaTest.BusinessLayer.DTO
{
    public class CookDTO
    {
        public int Id { get; }
        public string Name { get; }
        public string SecondName { get; }
        public string LastName { get; }
        public int RestaurantId { get; }
        public ShiftType Shift { get; }
        public WorkdaysType Workdays { get; }
        public short WorkdayLength { get; }
        public IEnumerable<QualificationsType> Qualifications { get; }

        public CookDTO(string name,
            string lastName,
            string secondName,
            int restaurantId,
            ShiftType shift,
            WorkdaysType workdays,
            short workdayLength,
            IEnumerable<QualificationsType> qualifications)
        {
            this.Name = name;
            this.LastName = lastName;
            this.SecondName = secondName;
            this.RestaurantId = restaurantId;
            this.Shift = shift;
            this.Workdays = workdays;
            this.WorkdayLength = workdayLength;
            this.Qualifications = qualifications;
        }
            

        public CookDTO(Cook cookEntity)
        {
            this.Id = cookEntity.Id;
            this.Name = cookEntity.Name;
            this.SecondName = cookEntity.SecondName;
            this.RestaurantId = cookEntity.RestaurantId;
            this.LastName = cookEntity.LastName;
            this.WorkdayLength = cookEntity.Workday;
            
            switch (cookEntity.Shift)
            {
                case "e":
                    this.Shift = ShiftType.Evening;
                    break;
                case "m":
                    this.Shift = ShiftType.Morning;
                    break;
                default:
                    throw new ArgumentException("shift in cook entity can be only \"e\" or \"m\"");
            }

            switch (cookEntity.Workdays)
            {
                case 5:
                    this.Workdays = WorkdaysType.Five;
                    break;
                case 2:
                    this.Workdays = WorkdaysType.Two;
                    break;
                default:
                    throw new ArgumentException("workdays in cook entity can be only 5 or 2");
            }

            var qualifications = new List<QualificationsType>();
            foreach (var qual in cookEntity.CookQualifications)
            {
                if (qual.Qualification.Name == "russian")
                {
                    qualifications.Add(QualificationsType.Russian);
                }
                else if (qual.Qualification.Name == "italian")
                {
                    qualifications.Add(QualificationsType.Italian);
                }
                else
                {
                    qualifications.Add(QualificationsType.Japanese);
                }
            }
            this.Qualifications = qualifications;
        }

        public enum ShiftType
        {
            Evening,
            Morning
        }

        public enum WorkdaysType
        {
            Two = 2,
            Five = 5
        }

        public enum QualificationsType
        {
            Russian,
            Italian,
            Japanese
        }
    }

    public static class ShiftTypeExtension
    {
        public static string ShiftCode(this CookDTO.ShiftType shift)
        {
            return shift == CookDTO.ShiftType.Evening ? "e" : "m";
        }
    }

    public static class WorkdaysTypeExtension
    {
        public static short ToNumber(this CookDTO.WorkdaysType workdays)
        {
            return workdays == CookDTO.WorkdaysType.Five ? (short)5 : (short)2;
        }
    }
}