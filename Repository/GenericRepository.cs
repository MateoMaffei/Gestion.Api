using Gestion.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gestion.Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        ///  Obtener todos los registros
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Buscar con expresión (filtro)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Agregar nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Actualizar registro evitando duplicado en el tracker
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            // Obtener la key primaria
            var key = _context.Model.FindEntityType(typeof(T))!
                .FindPrimaryKey()!.Properties.First();

            var keyValue = entity.GetType().GetProperty(key.Name)!.GetValue(entity, null);

            // Verificar si ya hay una instancia trackeada
            var existingEntity = await _dbSet.FindAsync(keyValue);

            if (existingEntity != null)
            {
                // Actualiza los valores sin adjuntar una segunda instancia
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                // Si no está siendo trackeada, la adjunta normalmente
                _dbSet.Update(entity);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Eliminar por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verificar existencia de un registro
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
