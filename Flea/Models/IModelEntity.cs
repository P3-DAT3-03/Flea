using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    /// <summary>
    /// An interface specifying basic requirement to implement
    /// basic database operations on EF entity models.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The model or entity that is inheriting from this class
    /// </typeparam>
    /// <typeparam name="TContext">
    /// A <see cref="DbContext"/> type that contains a
    /// <see cref="DbSet{TEntity}"/> property for the entity type.
    /// </typeparam>
    public interface IModelEntity<TEntity, in TContext>
        where TEntity: class, IModelEntity<TEntity, TContext>
        where TContext: DbContext
    {
        /// <summary>
        /// A field containing the method info for the
        /// <see cref="GetDbSet"/> method. This is used
        /// to get DbSet in a static context.
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        private static readonly MethodInfo GetDbSetMethod;

        /// <summary>
        /// Invokes the method referenced through the method info
        /// object stored in <see cref="GetDbSetMethod"/>.
        /// </summary>
        /// <returns></returns>
        private static DbSet<TEntity> GetDbSetStatic(TContext ctx) => (DbSet<TEntity>) GetDbSetMethod.Invoke(null, new object?[]{ctx})!;
        
        static IModelEntity()
        {
            var type = typeof(IModelEntity<TEntity, TContext>);
            GetDbSetMethod = type.GetMethod(nameof(GetDbSet), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        }

        /// <summary>
        /// Gets the relevant <see cref="DbSet{TEntity}"/> instance
        /// for the given <see cref="DbContext"/> instance.
        /// </summary>
        /// <param name="ctx">
        /// The context from which a <see cref="DbSet{TEntity}"/>
        /// is to be retrieved from.
        /// </param>
        /// <returns>The relevant <see cref="DbSet{TEntity}"/></returns>
        protected DbSet<TEntity> GetDbSet(TContext ctx);

        /// <summary>
        /// Saves changes to an existing entity.
        /// </summary>
        /// <param name="ctx">The database context to perform the action on.</param>
        public async Task Save(TContext ctx)
        {
            var set = GetDbSet(ctx);
            set.Update((TEntity) this);
            await ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Save a new entity.
        /// </summary>
        /// <param name="ctx">The database context to perform the action on.</param>
        public async Task SaveNew(TContext ctx)
        {
            var set = GetDbSet(ctx);
            await set.AddAsync((TEntity) this);
            await ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="ctx">The database context to perform the action on.</param>
        public async Task Delete(TContext ctx)
        {
            var set = GetDbSet(ctx);
            set.Remove((TEntity)this);
            await ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all entities of this type.
        /// </summary>
        /// <param name="ctx">The database context to perform the action on.</param>
        /// <returns>A list of entities.</returns>
        public static Task<List<TEntity>> GetAll(TContext ctx)
        {
            var set = GetDbSetStatic(ctx);
            return set.ToListAsync();
        }

    }
}