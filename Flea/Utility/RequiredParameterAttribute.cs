using System;
using Microsoft.AspNetCore.Components;

namespace Flea.Utility
{
	/// <summary>
	/// An attribute that marks a parameter as required. Meaning that
	/// the parameter must contain a non null value when it is checked.
	///
	/// This should only be used on properties of a blazor component (<see cref="ComponentBase"/>)
	/// and should always be used in conjunction with <see cref="ParameterAttribute"/>.
	///
	/// To verify that all required parameters have non-null values see <see cref="ParameterChecker"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class RequiredParameterAttribute : Attribute
	{
		// The attribute doesn't need to contain any additional information.
	}
}