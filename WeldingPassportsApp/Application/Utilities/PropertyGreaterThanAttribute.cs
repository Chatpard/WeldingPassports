using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Utilities
{
    public class PropertyGreaterThanAttribute : ValidationAttribute
    {
        private readonly string[] _compareProperties;

        public PropertyGreaterThanAttribute(string[] compareProperties)
        {
            _compareProperties = compareProperties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<int> values = new List<int>();

            foreach (string property in _compareProperties)
            {
                var compareProperty = validationContext.ObjectType.GetProperty(property);

                if (compareProperty == null)
                    throw new ArgumentException("Property with this name not found");

                values.Add(Convert.ToInt32(compareProperty.GetValue(validationContext.ObjectInstance)));
            }

            ErrorMessage = $"The input must be greater than {values.Max()}";

            if (Convert.ToInt32(value) < values.Max())
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
