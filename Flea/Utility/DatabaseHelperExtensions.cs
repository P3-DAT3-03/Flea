using System.Collections.Generic;
using System.Threading.Tasks;
using Flea.Models;
using Microsoft.EntityFrameworkCore;

namespace Flea.Utility
{
	/// <summary>
	/// A static class containing various extension methods that assist
	/// in simplifying various basic database operations.
	/// </summary>
	public static class DatabaseHelperExtensions
	{
		/// <summary>
		/// Gets all entities of a given type from a specified database context.
		/// </summary>
		/// <param name="factory">The factory that will supply the database context instance.</param>
		/// <typeparam name="TContext">The database context type.</typeparam>
		/// <typeparam name="TEntity">The entity that we are retrieving.</typeparam>
		/// <returns>A list of all entities in the given context.</returns>
		public static async Task<List<TEntity>> GetAll<TContext, TEntity>(this IDbContextFactory<TContext> factory)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
			where TContext : DbContext
		{
			await using var ctx = factory.CreateDbContext();
			return await IModelEntity<TEntity, TContext>.GetDbSetStatic(ctx).ToListAsync();
		}
		
		// TODO maybe rename this one the use of update can seem odd since it object contains a method call Update
		public static UpdateQuery<TContext> Update<TContext>(this IDbContextFactory<TContext> factory)
			where TContext : DbContext =>
			new(factory);
		
		public static GetQuery<TContext, TEntity> Get<TContext, TEntity>(this IDbContextFactory<TContext> factory)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
			where TContext : DbContext =>
			new(factory);


		/// <summary>
		/// Save/update an entity instance in the database.
		/// </summary>
		/// <param name="factory">The factory that will supply the database context instance.</param>
		/// <param name="entity">The entity that should be updated.</param>
		/// <typeparam name="TContext">The database context type.</typeparam>
		/// <typeparam name="TEntity">The type of the entity that is being updated.</typeparam>
		public static async Task Save<TContext, TEntity>(this IDbContextFactory<TContext> factory, TEntity entity)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
			where TContext : DbContext
		{
			await using var ctx = factory.CreateDbContext();
			var set = entity.GetDbSet(ctx);
			set.Update(entity);
			await ctx.SaveChangesAsync();
		}

		/// <summary>
		/// Save/create a new entity in the database. 
		/// </summary>
		/// <param name="factory">The factory that will supply the database context instance.</param>
		/// <param name="entity">The entity that should be created.</param>
		/// <typeparam name="TContext">The database context type.</typeparam>
		/// <typeparam name="TEntity">The type of the entity that is being created.</typeparam>
		public static async Task SaveNew<TContext, TEntity>(this IDbContextFactory<TContext> factory, TEntity entity)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
			where TContext : DbContext
		{
			await using var ctx = factory.CreateDbContext();
			var set = entity.GetDbSet(ctx);
			await set.AddAsync(entity);
			await ctx.SaveChangesAsync();
		}

		/// <summary>
		/// Delete an entity instance from the database.
		/// </summary>
		/// <param name="factory">The factory that will supply the database context instance.</param>
		/// <param name="entity">The entity that is to be removed.</param>
		/// <typeparam name="TContext">The database context type.</typeparam>
		/// <typeparam name="TEntity">The type of the entity that is to be deleted.</typeparam>
		public static async Task Delete<TContext, TEntity>(this IDbContextFactory<TContext> factory, TEntity entity)
			where TEntity : class, IModelEntity<TEntity, TContext>, new()
			where TContext : DbContext
		{
			await using var ctx = factory.CreateDbContext();
			var set = entity.GetDbSet(ctx);
			set.Remove(entity);
			await ctx.SaveChangesAsync();
		}
	}
}