using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Flea.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Flea.Utility
{
	public class GetQuery<TContext, TEntity> : QueryBuilder<TContext>
		where TContext : DbContext
		where TEntity : class, IModelEntity<TEntity, TContext>, new()
	{
		private IQueryable<TEntity> _query;

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
		/// A reference to the current <see cref="GetQuery{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public GetQuery<TContext, TEntity> Include(params string[] includeStrings)
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
		/// A reference to the current <see cref="GetQuery{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public GetQuery<TContext, TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> selector)
		{
			CheckDisposeStatus();
			_query = _query.Include(selector);
			return this;
		}

		public GetQuery<TContext, TEntity> ThenInclude<TSourceProperty, TTargetProperty>(
			Expression<Func<TSourceProperty, TTargetProperty>> selector
		)
		{
			CheckDisposeStatus();
			_query = _query switch
			{
				IIncludableQueryable<TEntity, TSourceProperty> subQueryA => 
					subQueryA.ThenInclude(selector),
				IIncludableQueryable<TEntity, IEnumerable<TSourceProperty>> subQueryB =>
					subQueryB.ThenInclude(selector),
				_ => throw new Exception(
					$"Cannot use ThenInclude on type {typeof(TSourceProperty).Name} without first selecting it through Include.")
			};

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
		/// Filters by the given predicate. Used to filter a list or select
		/// specific entities.
		/// </summary>
		/// <returns>
		/// A reference to the current <see cref="GetQuery{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public GetQuery<TContext, TEntity> Where(Expression<Func<TEntity, bool>> predicate)
		{
			CheckDisposeStatus();
			_query = _query.Where(predicate);
			return this;
		}

		/// <summary>
		/// Order the selection by a given key in ascending order.
		/// </summary>
		/// <param name="selector">A lambda expression that selects the key to order by.</param>
		/// <typeparam name="TKey">The type of the selected key.</typeparam>
		/// <returns>
		/// A reference to the current <see cref="GetQuery{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public GetQuery<TContext, TEntity> OrderAscending<TKey>(Expression<Func<TEntity, TKey>> selector)
		{
			CheckDisposeStatus();
			_query = _query switch
			{
				IOrderedQueryable<TEntity> orderedQuery => orderedQuery.ThenBy(selector),
				_ => _query.OrderBy(selector),
			};
			return this;
		}

		/// <summary>
		/// Order the selection by a given key in descending order.
		/// </summary>
		/// <param name="selector">A lambda expression that selects the key to order by.</param>
		/// <typeparam name="TKey">The type of the selected key.</typeparam>
		/// <returns>
		/// A reference to the current <see cref="GetQuery{TContext,TEntity}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public GetQuery<TContext, TEntity> OrderDescending<TKey>(Expression<Func<TEntity, TKey>> selector)
		{
			CheckDisposeStatus();
			_query = _query switch
			{
				IOrderedQueryable<TEntity> orderedQuery => orderedQuery.ThenByDescending(selector),
				_ => _query.OrderByDescending(selector),
			};
			return this;
		}

		public GetQuery(IDbContextFactory<TContext> factory) : base(factory)
		{
			_query = IModelEntity<TEntity, TContext>.GetDbSetStatic(Context!);
		}
	}
}