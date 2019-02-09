using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArcadiaTest.Models.Validators;

namespace ArcadiaTest.Models
{
    public class CreateCookRequest
    {
        [Required(ErrorMessage = "This is required field"),
         MinLength(1, ErrorMessage = "Length should be from 1 to 20"),
         MaxLength(20, ErrorMessage = "Length should be from 1 to 20")]
        public string FirstName { get; set; }
        
        [MinLength(1, ErrorMessage = "Length should be from 1 to 20"), MaxLength(20, ErrorMessage = "Length should be from 1 to 20")]
        public string SecondName { get; set; }
        
        [Required(ErrorMessage = "This is required field"),
         MinLength(1, ErrorMessage = "Length should be from 1 to 20"),
         MaxLength(20, ErrorMessage = "Length should be from 1 to 20")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "This is required field"), Range(4, 10)]
        public short WorkdayLength { get; set; }
        
        [Required(ErrorMessage = "This is required field"), WorkdaysValidator]
        public int Workdays { get; set; }
        
        [Required(ErrorMessage = "This is required field"), ShiftValidator]
        public string Shift { get; set; }
        
        [Required(ErrorMessage = "This is required field"), QualificationsValidator]
        public List<string> Qualification { get; set; }
        public List<string> AvailableQualifications { get; set; }
    }
}