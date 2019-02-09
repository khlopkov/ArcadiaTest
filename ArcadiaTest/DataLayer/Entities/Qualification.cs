using System.Collections.Generic;

namespace ArcadiaTest.DataLayer.Entities
{
    public class Qualification
    {
        public Qualification()
        {
            CookQualifications = new HashSet<CookQualifications>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CookQualifications> CookQualifications { get; set; }
    }
}
