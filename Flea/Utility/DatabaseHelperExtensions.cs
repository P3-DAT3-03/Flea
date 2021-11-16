using System.Collections.Generic;
using System.Threading.Tasks;
using Flea.Models;
using Microsoft.EntityFrameworkCore;

namespace Flea.Utility
{
	public static class DatabaseHelperExtensions
	{
		public static async Task<List<TEntity>> GetAll<TContext, TEntity>(this IDbContextFactory<TContext> factory)
			where TEntity : class, IModelEntity<TEntity, TContext>
			where TContext : DbContext
		{
			await using var ctx = factory.CreateDbContext();
			return await IModelEntity<TEntity, TContext>.GetAll(ctx);
		}

		public static async Task Save<TContext, TEntity>(this IDbContextFactory<TContext> factory, TEntity entity)
			where TEntity : class, IModelEntity<TEntity, TContext>
			where TContext : DbContext
		{
			await using var ctx = factory.CreateDbContext();
			await entity.Save(ctx);
		}
		
		public static async Task SaveNew<TContext, TEntity>(this IDbContextFactory<TContext> factory, TEntity entity)
			where TEntity : class, IModelEntity<TEntity, TContext>
			where TContext : DbContext
		{
			await using var ctx = factory.CreateDbContext();
			await entity.SaveNew(ctx);
		}
		
		public static async Task Delete<TContext, TEntity>(this IDbContextFactory<TContext> factory, TEntity entity)
			where TEntity : class, IModelEntity<TEntity, TContext>
			where TContext : DbContext
		{
			await using var ctx = factory.CreateDbContext();
			await entity.Delete(ctx);
		}
	}
}