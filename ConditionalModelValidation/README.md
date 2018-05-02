# Conditional Model Validation

Inspired by post: https://stackoverflow.com/questions/26354853/conditionally-required-property-using-data-annotations#27666044

This library can be ussed to add annotations to mark model properties as conditionally required:
- Required if another property is null
- Required if another property is NOT null
- Required if another property equals some value
- Required if another property DOES NOT equal some value
- Required if another property equals anything from a list of values

## How to use
```
using ConditionalModelValidation;

public class MyModel {

	[Required] // Always required, from System.ComponentModel.DataAnnotations
	public string Name { get; set; }

	public bool IsBald { get; set; } 

	[RequiredIfEquals("IsBald", true)] // HairColor is required if IsBald = True
	public string HairColor { get; set; }

	[RequiredIfNotEquals("IsBald", true)] // NeedsHeadWax is required if IsBald != True
	public string NeedsHeadWax { get; set; }

	public List<string> ChildsNames { get; set; }

	[RequiredIfNotNull("ChildsNames")] // FavoriteChildName is required if ChildsNames is not null
	public string FavoriteChildName { get; set; }

	[RequiredIfNull("ChildsNames")] // ProspectiveChildName is required if ChildsNames is null
	public string ProspectiveChildName { get; set; }

	[RequiredIfEqualsAny("ChildsNames", new string[]("Bob", "Sue"))] // UNTESTED
	public string ProspectiveChildName { get; set; }

}
```
