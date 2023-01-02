using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Utilities
{
    public class PropertyLessThanAttribute : ValidationAttribute
    {
        private readonly string _compareProperty;

        public PropertyLessThanAttribute(string compareProperty)
        {
            _compareProperty = compareProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var compareProperty = validationContext.ObjectType.GetProperty(_compareProperty);

            if (compareProperty == null)
                throw new ArgumentException("Property with this name not found");

            int comparePropertyValue = Convert.ToInt32(compareProperty.GetValue(validationContext.ObjectInstance));

            ErrorMessage = $"The input must be less than {comparePropertyValue}";

            if (Convert.ToInt32(value) > comparePropertyValue)
                return new ValidationResult(ErrorMessage);
            
            return ValidationResult.Success;
        }
    }
}
