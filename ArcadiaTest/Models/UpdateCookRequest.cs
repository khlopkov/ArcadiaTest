using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArcadiaTest.Models.Validators;

namespace ArcadiaTest.Models
{
    public class UpdateCookRequest
    {
        [MinLength(1, ErrorMessage = "Length should be from 1 to 20"),
         MaxLength(20, ErrorMessage = "Length should be from 1 to 20")]
        public string FirstName { get; set; }
        
        [MinLength(1, ErrorMessage = "Length should be from 1 to 20"), MaxLength(20, ErrorMessage = "Length should be from 1 to 20")]
        public string SecondName { get; set; }
        
        [MinLength(1, ErrorMessage = "Length should be from 1 to 20"),
         MaxLength(20, ErrorMessage = "Length should be from 1 to 20")]
        public string LastName { get; set; }
        
        [Range(4, 10)]
        public short WorkdayLength { get; set; }
        
        [WorkdaysValidator]
        public int Workdays { get; set; }
        
        [ShiftValidator]
        public string Shift { get; set; }
        
        [QualificationsValidator]
        public List<string> Qualification { get; set; }
        
        public int RestaurantId { get; set; }
    }
}