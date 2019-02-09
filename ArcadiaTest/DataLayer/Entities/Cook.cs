using System.Collections.Generic;

namespace ArcadiaTest.DataLayer.Entities
{
    public class Cook
    {
        public Cook()
        {
            CookQualifications = new HashSet<CookQualifications>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Shift { get; set; }
        public short Workday { get; set; }
        public short Workdays { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<CookQualifications> CookQualifications { get; set; }
    }
}
