using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ArcadiaTest.Models.Validators
{
    public class WorkdaysValidatorAttribute : ValidationAttribute
    {
        private int[] _possibleValues = new[] {2, 5};

        public override bool IsValid(object value)
        {
            if (value is int val)
            {
                return _possibleValues.Contains(val);
            }

            return false;
        }
    }
}