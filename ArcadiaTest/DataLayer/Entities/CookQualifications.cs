namespace ArcadiaTest.DataLayer.Entities
{
    public class CookQualifications
    {
        public int CookId { get; set; }
        public int QualificationId { get; set; }

        public virtual Cook Cook { get; set; }
        public virtual Qualification Qualification { get; set; }
    }
}
