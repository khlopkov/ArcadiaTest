using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ArcadiaTest.Models.Validators
{
    public class QualificationsValidatorAttribute : ValidationAttribute
    {
        private string[] _possibleValues = new[] {"russian", "italian", "japanese"};

        public override bool IsValid(object value)
        {
            if (value is ICollection<string> val)
            {
                if (val.Count <= 0)
                {
                    return false;
                }
                foreach (var qual in val)
                {
                    if (!_possibleValues.Contains(qual))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }
}