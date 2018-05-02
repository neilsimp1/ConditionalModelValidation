using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ConditionalModelValidation {
	public class RequiredIfNotEqualsAttribute : ValidationAttribute {

		public string OtherPropertyName { get; private set; }
		public string OtherPropertyDisplayName { get; set; }
		public object OtherPropertyValue { get; private set; }
		public override bool RequiresValidationContext { get { return true; } }

		public RequiredIfNotEqualsAttribute(string otherPropertyName, object otherPropertyValue = null)
				: base("'{0}' is required because '{1}' is equal to '{2}'.") {
			OtherPropertyName = otherPropertyName;
			OtherPropertyValue = otherPropertyValue;
		}

		public override string FormatErrorMessage(string name) {
			return string.Format(
				CultureInfo.CurrentCulture,
				ErrorMessageString,
				name,
				OtherPropertyDisplayName ?? OtherPropertyName,
				OtherPropertyValue
			);
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			if (validationContext == null)
				throw new ArgumentNullException("validationContext");

			var otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
			var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);

			if (!otherPropertyValue.Equals(OtherPropertyValue) && value == null)
				return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

			return ValidationResult.Success;
		}

	}
}
