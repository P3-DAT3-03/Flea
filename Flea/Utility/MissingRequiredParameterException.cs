using System;
using System.Reflection;

namespace Flea.Utility
{
	/// <summary>
	/// An exception that is thrown when parameters are not set properly.
	/// </summary>
	public class MissingRequiredParameterException : Exception
	{
		public MissingRequiredParameterException(MemberInfo propertyInfo)
			: this(propertyInfo.Name, propertyInfo.ReflectedType!.Name)
		{
		}

		public MissingRequiredParameterException(string propertyName, string typeName)
			: base($"Missing required parameter {propertyName} in {typeName}")
		{
		}
	}
}