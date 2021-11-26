using System;
using Microsoft.EntityFrameworkCore;

namespace Flea.Utility
{
	
	/// <summary>
	/// A class that helps in setting up a database context
	/// to execute a query.
	/// </summary>
	/// <remarks>
	/// It should be noted that these query builders are single use
	/// meaning that all final operations will cause the object
	/// to be disposed. (Final operations are all methods on the
	/// class that do not return the object itself.)
	/// </remarks>
	/// <typeparam name="TContext">The context on which to perform the query.</typeparam>
	public abstract class QueryBuilder<TContext> : IDisposable
		where TContext : DbContext
	{
		protected TContext? Context;

		private QueryBuilder(TContext context)
		{
			Context = context;
		}

		protected QueryBuilder(IDbContextFactory<TContext> factory) : this(factory.CreateDbContext())
		{
		}

		/// <summary>
		/// Checks whether the object has been disposed.
		/// </summary>
		/// <exception cref="Exception">An exception is thrown if the object has been disposed.</exception>
		protected void CheckDisposeStatus()
		{
			if (Context == null)
				throw new Exception("QueryBuilder has already been disposed.");
		}

		~QueryBuilder()
		{
			Dispose();
		}

		public void Dispose()
		{
			Context?.Dispose();
			Context = null;
			GC.SuppressFinalize(this);
		}
	}
}