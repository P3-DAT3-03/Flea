using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Flea.Models;
using Microsoft.EntityFrameworkCore;

namespace Flea.Utility
{
	// NOTE: it may be worth considering splitting this class in two as it
	//		  currently performs two distinct tasks that make use of differing
	//		  subsets of the classes functionality while ignoring the other subset.
	
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
	/// <typeparam name="TEntity">The primary entity type on which the query acts.</typeparam>
	public class QueryBuilder<TContext, TEntity> : IDisposable
		where TContext : DbContext
		where TEntity : class, IModelEntity<TEntity, TContext>, new()
	{
		private TContext? _context;
		private IQueryable<TEntity> _query;

		private QueryBuilder(TContext context)
		{
			_context = context;
			_query = IModelEntity<TEntity, TContext>.GetDbSetStatic(context);
		}

		public QueryBuilder(IDbContextFactory<TContext> factory) : this(factory.CreateDbContext())
		{
		}

		/// <summary>
		/// Include one or more sub-entities in the query. This is used when you want
		/// to fetch an entity whose model contains foreign keys and you want to fetch
		/// the foreign objects as well.
		/// <example>
		/// In the following example it is used to fetch a <see cref="Reservation"/>
		/// in addition to its properties referencing <see cref="Customer"/> and
		/// <see cref="Event"/> objects.
		/// <code>
		/// <![CDATA[
		///		await factory
		///			.Query<BingoContext, Reservation>()
		///			.Include("ReservationOwner", "Event")
		///			.First(r => r.Id == 4);
		/// ]]>
		///	</code>
		/// </example>
		/// </summary>
		/// <param name="includeStrings">
		/// The strings specifying the foreign properties. If multiple levels of properties
		/// are desired the string should be specified as such <c>"Reservations.ReservationOwner"</c>,
		/// this particular example would be used in the context of fetching <see cref="Event"/>
		/// where we want to fetch both the associated <see cref="Reservation"/>s in addition
		/// to the owner objects of the <see cref="Reservation"/>s. 
		/// </param>
		/// <returns>
		/// A reference to the current <see cref="QueryBuilder{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public QueryBuilder<TContext, TEntity> Include(params string[] includeStrings)
		{
			CheckDisposeStatus();
			foreach (var includeString in includeStrings)
			{
				_query = _query.Include(includeString);
			}

			return this;
		}

		/// <summary>
		/// Include one or more sub-entities in the query. This is used when you want
		/// to fetch an entity whose model contains foreign keys and you want to fetch
		/// the foreign objects as well.
		/// <example>
		/// In the following example it is used to fetch a <see cref="Reservation"/>
		/// in addition to its properties referencing <see cref="Customer"/> and
		/// <see cref="Event"/> objects.
		/// <code>
		/// <![CDATA[
		///		await factory
		///			.Query<BingoContext, Reservation>()
		///			.Include(r => r.ReservationOwner)
		///			.Include(r => r.Event)
		///			.First(r => r.Id == 4);
		/// ]]>
		/// </code>
		/// </example>
		/// </summary>
		/// <param name="selector">A lambda function expression that selects the desired properties.</param>
		/// <typeparam name="TProperty">
		/// The type of the property being returned from the selector.
		/// This will in almost all cases be inferred by the compiler.
		/// </typeparam>
		/// <returns>
		/// A reference to the current <see cref="QueryBuilder{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public QueryBuilder<TContext, TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> selector)
		{
			CheckDisposeStatus();
			_query = _query.Include(selector);
			return this;
		}

		/// <summary>
		/// Attaches an entity to the current context. This is used when you
		/// want to add a new entity that has a relation with an already existing
		/// entity. If this is not used when doing so the entity framework
		/// will attempt to insert the related object as if it were a new object
		/// causing a primary key conflict.
		/// <example>
		/// An example showing its use in adding a new <see cref="Reservation"/>.
		/// <code>
		/// <![CDATA[
		///		await factory
		///			.Query<BingoContext, Reservation>()
		///			.With(someRelatedCustomer)
		///			.New(theReservation)
		///			.Save();
		/// ]]>
		/// </code>
		/// </example> 
		/// </summary>
		/// <param name="other">The entity that is to be added to the context of the current query.</param>
		/// <typeparam name="TOther">The type of the entity.</typeparam>
		/// <returns>
		/// A reference to the current <see cref="QueryBuilder{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public QueryBuilder<TContext, TEntity> With<TOther>(TOther other)
			where TOther : class, IModelEntity<TOther, TContext>, new()
		{
			CheckDisposeStatus();
			IModelEntity<TOther, TContext>.GetDbSetStatic(_context!).Attach(other);
			return this;
		}

		/// <summary>
		/// Filters by the given predicate. Used to filter a list or select
		/// specific entities.
		/// </summary>
		/// <returns>
		/// A reference to the current <see cref="QueryBuilder{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public QueryBuilder<TContext, TEntity> Where(Expression<Func<TEntity, bool>> predicate)
		{
			CheckDisposeStatus();
			_query = _query.Where(predicate);
			return this;
		}

		/// <summary>
		/// Gets the first entity matching the query.
		/// </summary>
		/// <returns>
		/// The first entity found if any.
		/// </returns>
		public async Task<TEntity?> First()
		{
			CheckDisposeStatus();
			try
			{
				return await _query.FirstAsync();
			}
			finally
			{
				Dispose();
			}
		}

		/// <summary>
		/// Gets the first entity matching the query and the given predicate.
		/// </summary>
		/// <param name="predicate">A function that selects a specific entity.</param>
		/// <returns>The first entity matching the predicate if any.</returns>
		public async Task<TEntity?> First(Expression<Func<TEntity, bool>> predicate)
		{
			CheckDisposeStatus();
			try
			{
				return await _query.FirstAsync(predicate);
			}
			finally
			{
				Dispose();
			}
		}

		/// <summary>
		/// Gets the first <c>n</c> elements matching the query.
		/// </summary>
		/// <param name="n">The number of elements to fetch.</param>
		/// <returns>A list containing the elements fetched.</returns>
		public async Task<List<TEntity>> Take(int n)
		{
			CheckDisposeStatus();
			try
			{
				return await _query.Take(n).ToListAsync();
			}
			finally
			{
				Dispose();
			}
		}

		/// <summary>
		/// Gets all elements matching the query.
		/// </summary>
		/// <returns>A list containing the fetched elements.</returns>
		public async Task<List<TEntity>> All()
		{
			CheckDisposeStatus();
			try
			{
				return await _query.ToListAsync();
			}
			finally
			{
				Dispose();
			}
		}

		/// <summary>
		/// Updates an entity.
		/// </summary>
		/// <param name="entity">The entity that is to updated.</param>
		/// <returns>
		/// A reference to the current <see cref="QueryBuilder{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public QueryBuilder<TContext, TEntity> Update(TEntity entity)
		{
			CheckDisposeStatus();
			var set = entity.GetDbSet(_context!);
			set.Update(entity);
			return this;
		}

		/// <summary>
		/// Creates a new entity.
		/// </summary>
		/// <param name="entity">The entity that is to created.</param>
		/// <returns>
		/// A reference to the current <see cref="QueryBuilder{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public QueryBuilder<TContext, TEntity> New(TEntity entity)
		{
			CheckDisposeStatus();
			var set = entity.GetDbSet(_context!);
			set.Add(entity);
			return this;
		}

		/// <summary>
		/// Save all changes that have been made.
		/// </summary>
		public async Task Save()
		{
			CheckDisposeStatus();
			try
			{
				await _context!.SaveChangesAsync();
			}
			finally
			{
				Dispose();
			}
		}

		/// <summary>
		/// Checks whether the object has been disposed.
		/// </summary>
		/// <exception cref="Exception">An exception is thrown if the object has been disposed.</exception>
		private void CheckDisposeStatus()
		{
			if (_context == null)
				throw new Exception("QueryBuilder has already been disposed.");
		}

		~QueryBuilder()
		{
			Dispose();
		}

		public void Dispose()
		{
			_context?.Dispose();
			_context = null;
			GC.SuppressFinalize(this);
		}
	}
}