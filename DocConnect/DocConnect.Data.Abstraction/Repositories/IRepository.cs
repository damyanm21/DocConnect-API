namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Abstraction of repository access methods
    /// </summary>
    /// <typeparam name="T">Repository type / db table</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>IEnumerable</returns>
        Task<IEnumerable<T>> All();

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>IEnumerable</returns>
        Task<IEnumerable<T>> AllReadonly();

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        /// <param name="id">record identificator</param>
        /// <returns>Single record</returns>
        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdsAsync(int[] id);

        /// <summary>
        /// Adds entity to the database
        /// </summary>
        /// <param name="entity">Entity to add</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Ads collection of entities to the database
        /// </summary>
        /// <param name="entities">Enumerable list of entities</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Updates a record in database
        /// </summary>
        /// <param name="entity">Entity for record to be updated</param>
        void Update(T entity);

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="id">Identificator of record to be deleted</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        void Delete(T entity);

        /// <summary>
        /// Saves all made changes in trasaction
        /// </summary>
        /// <returns>Error code</returns>
        Task<int> SaveChangesAsync();
    }
}

