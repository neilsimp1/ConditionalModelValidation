using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ConditionalModelValidation {
	public class RequiredIfNotNullAttribute : ValidationAttribute {

		public string OtherPropertyName { get; private set; }
		public string OtherPropertyDisplayName { get; set; }
		public override bool RequiresValidationContext { get { return true; } }

		public RequiredIfNotNullAttribute(string otherPropertyName)
				: base("'{0}' is required because '{1}' is not null.") {
			OtherPropertyName = otherPropertyName;
		}

		public override string FormatErrorMessage(string name) {
			return string.Format(
				CultureInfo.CurrentCulture,
				ErrorMessageString,
				name,
				OtherPropertyDisplayName ?? OtherPropertyName
			);
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			if (validationContext == null)
				throw new ArgumentNullException("validationContext");

			var otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
			var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);

			if (otherPropertyValue != null && value == null)
				return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

			return ValidationResult.Success;
		}

	}
}
