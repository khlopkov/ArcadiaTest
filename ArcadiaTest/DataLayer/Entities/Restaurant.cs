using System;
using System.Collections.Generic;

namespace ArcadiaTest.DataLayer.Entities
{
    public class Restaurant
    {
        public Restaurant()
        {
            Cook = new HashSet<Cook>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Cook> Cook { get; set; }
    }
}
