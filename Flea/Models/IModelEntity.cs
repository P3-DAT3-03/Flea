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
    public interface IModelEntity<TEntity, TContext>
        where TEntity: class, IModelEntity<TEntity, TContext>, new()
        where TContext: DbContext
    {
        /// <summary>
        /// A static instance of the entity used
        /// to fetch <see cref="DbSet{TEntity}"/>.
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        private static readonly TEntity Entity;

        /// <summary>
        /// Gets a DbSet through a static instance field.
        /// </summary>
        /// <returns>A DbSet for the entity type</returns>
        public static DbSet<TEntity> GetDbSetStatic(TContext ctx) => Entity.GetDbSet(ctx);
        
        static IModelEntity()
        {
            Entity = new TEntity();
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
        public DbSet<TEntity> GetDbSet(TContext ctx);
    }
}