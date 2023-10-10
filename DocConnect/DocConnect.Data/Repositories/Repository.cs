using DocConnect.Data.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DocConnect.Data.Repositories
{
    /// <summary>
    /// Implementation of repository access methods
    /// for Relational Database Engine
    /// </summary>
    /// <typeparam name="T">Type of the data table to which 
    /// current reposity is attached</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Entity framework DB context holding connection information and properties
        /// and tracking entity states 
        /// </summary>
        protected DbContext Context { get; set; }

        /// <summary>
        /// Representation of table in database
        /// </summary>
        protected DbSet<T> DbSet()
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// Adds entity to the database
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public async Task AddAsync(T entity)
        {
            await DbSet().AddAsync(entity);
        }

        /// <summary>
        /// Adds collection of entities to the database
        /// </summary>
        /// <param name="entities">Enumerable list of entities</param>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await DbSet().AddRangeAsync(entities);
        }

        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>IEnumerable</returns>
        public async Task<IEnumerable<T>> All()
        {
            return await DbSet().ToListAsync();
        }

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>IEnumerable</returns>
        public async Task<IEnumerable<T>> AllReadonly()
        {
            return await DbSet()
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="id">Identificator of record to be deleted</param>
        public async Task DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            Delete(entity);
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        public void Delete(T entity)
        {
            EntityEntry entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                DbSet().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        /// <param name="id">record identificator</param>
        /// <returns>Single record</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet().FindAsync(id);
        }

        public async Task<T> GetByIdsAsync(int[] id)
        {
            return await DbSet().FindAsync(id);
        }

        /// <summary>
        /// Saves all made changes in trasaction
        /// </summary>
        /// <returns>Error code</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a record in database
        /// </summary>
        /// <param name="entity">Entity for record to be updated</param>
        public void Update(T entity)
        {
            DbSet().Update(entity);
        }
    }
}

