using SQLite;
using System.Linq.Expressions;
using TaskManagement.MVVM.Models;

namespace TaskManagement.Persistence.Respositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        private SQLiteAsyncConnection _dbConnection;

        public async Task InitializeAsync()
        {
            await SetUpDb();
        }

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment
                    .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TaskManagement.db");

                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<MainTask>();
                await _dbConnection.CreateTableAsync<SubTask>();
            }
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            entity.Validate();
            await InitializeAsync();
            var test = entity.Id;
            return await _dbConnection.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            entity.Validate();
            await InitializeAsync();
            return await _dbConnection.UpdateAsync(entity);
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                entity.Validate();
            }

            await InitializeAsync();
            return await _dbConnection.UpdateAllAsync(entities);
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            await InitializeAsync();
            return await _dbConnection.DeleteAsync(entity);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            await InitializeAsync();
            return await _dbConnection.FindAsync<TEntity>(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            await InitializeAsync();
            return await _dbConnection.Table<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await InitializeAsync();
            return await _dbConnection.Table<TEntity>().Where(predicate).ToListAsync();
        }
    }
}
