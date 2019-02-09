using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ArcadiaTest.Models.Validators
{
    public class ShiftValidatorAttribute : ValidationAttribute 
    {
        
        private string[] _possibleValues = new[] {"evening", "morning"};

        public override bool IsValid(object value)
        {
            if (value is string val)
            {
                return _possibleValues.Contains(val);
            }

            return false;
        }
    }
}