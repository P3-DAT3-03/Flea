using System.Threading.Tasks;
using Flea.Models;
using Microsoft.EntityFrameworkCore;

namespace Flea.Utility
{
	public class UpdateQuery<TContext> : QueryBuilder<TContext> where TContext : DbContext
	{
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
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns>
		/// A reference to the current <see cref="UpdateQuery{TContext}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public UpdateQuery<TContext> With<TEntity>(TEntity other)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
		{
			CheckDisposeStatus();
			IModelEntity<TEntity, TContext>.GetDbSetStatic(Context!).Attach(other);
			return this;
		}
		
		/// <summary>
		/// Updates an entity.
		/// </summary>
		/// <param name="entity">The entity that is to updated.</param>
		/// <returns>
		/// A reference to the current <see cref="UpdateQuery{TContext}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public UpdateQuery<TContext> Update<TEntity>(TEntity entity)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
		{
			CheckDisposeStatus();
			var set = entity.GetDbSet(Context!);
			set.Update(entity);
			return this;
		}

		/// <summary>
		/// Creates a new entity.
		/// </summary>
		/// <param name="entity">The entity that is to created.</param>
		/// <returns>
		/// A reference to the current <see cref="UpdateQuery{TContext}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public UpdateQuery<TContext> New<TEntity>(TEntity entity)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
		{
			CheckDisposeStatus();
			var set = entity.GetDbSet(Context!);
			set.Add(entity);
			return this;
		}
		
		/// <summary>
		/// Creates a new entity.
		/// </summary>
		/// <param name="entity">The entity that is to created.</param>
		/// <returns>
		/// A reference to the current <see cref="UpdateQuery{TContext}"/> (<c>this</c>)
		/// allowing for chaining of commands.
		/// </returns>
		public UpdateQuery<TContext> New<TEntity>(params TEntity[] entity)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
		{
			CheckDisposeStatus();
			var set = entity[0].GetDbSet(Context!);
			set.AddRange(entity);
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
				await Context!.SaveChangesAsync();
			}
			finally
			{
				Dispose();
			}
		}

		public UpdateQuery(IDbContextFactory<TContext> factory) : base(factory)
		{
		}
	}
}