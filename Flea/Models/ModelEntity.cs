using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    /// <summary>
    /// An abstract class used to implement basic
    /// database operations on EF entity models.
    /// TODO remove mentions of abstract classes in favor interface
    /// </summary>
    /// <typeparam name="TEntity">
    /// The model or entity that is inheriting from this class
    /// </typeparam>
    /// <typeparam name="TContext">
    /// A <see cref="DbContext"/> type that contains a
    /// <see cref="DbSet{TEntity}"/> property for the entity type.
    /// </typeparam>
    public interface IModelEntity<TEntity, TContext>
        where TEntity: class, IModelEntity<TEntity, TContext>
        where TContext: DbContext
    {
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

    }
}