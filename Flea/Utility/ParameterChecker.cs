using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Flea.Utility
{
	/// <summary>
	/// A utility class that aids in checking that blazor component
	/// parameters are valid, while providing useful errors.
	/// </summary>
	public static class ParameterChecker
	{
		/// <summary>
		/// Checks that all parameters marked by <see cref="RequiredParameterAttribute"/> in a
		/// <see cref="ComponentBase"/> instance are set to a non-null value.
		/// </summary>
		/// <param name="instance">The object instance that is to checked.</param>
		/// <typeparam name="T">
		/// A type inheriting from <see cref="ComponentBase"/>.
		/// All blazor razor components inherit from this implicitly.
		/// </typeparam>
		/// <exception cref="MissingRequiredParameterException">An exception is thrown if a null value is found.</exception>
		public static void CheckParameters<T>(T instance) where T : ComponentBase
		{
			var properties = typeof(T).GetProperties();
			foreach (var property in properties)
			{
				var attr = property.GetCustomAttribute<RequiredParameterAttribute>();
				if (attr is null)
					continue;

				if (property.GetValue(instance) is null)
					throw new MissingRequiredParameterException(property);
			}
		}

		/// <summary>
		/// Check whether a specific parameter is null.
		/// </summary>
		/// <param name="_">The component on which the parameter exists.</param>
		/// <param name="value">The value of the parameter that is to be checked.</param>
		/// <param name="name">The name of the parameter that is being checked.</param>
		/// <typeparam name="TComponent">The type of the component.</typeparam>
		/// <typeparam name="TParameter">The type of the parameter.</typeparam>
		/// <exception cref="MissingRequiredParameterException">An exception is thrown when `value == null`.</exception>
		public static void CheckParameter<TComponent, TParameter>(
			this TComponent _,
			TParameter? value,
			string name
		)
			where TComponent : ComponentBase
		{
			if (value is null)
				throw new MissingRequiredParameterException(name, typeof(TComponent).Name);
		}
	}
}