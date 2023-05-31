using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Создать объект в бд
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Create(TEntity item);
        
        /// <summary>
        /// Поиск по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(Guid id);
        
        /// <summary>
        /// Получение объекта из бд
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Get();
        
        /// <summary>
        /// Получение объекта с условием из бд
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        
        /// <summary>
        /// Удаление объекта из бд
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Remove(TEntity item);
        
        /// <summary>
        /// Обновление объекта из бд
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Update(TEntity item);
        
        /// <summary>
        /// Получение объекта с вложенными полями с условием из бд
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        
        /// <summary>
        /// Получение объекта с вложенными полями с условием из бд
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetWithInclude(object includeProperty,
            params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
