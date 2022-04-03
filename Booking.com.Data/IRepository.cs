using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Data
{
    public interface IRepository<TEntity, TKey> where TEntity: IEntity<TKey>
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Remove(TKey id);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        IList<TEntity> GetAll();
        TEntity GetById(TKey id);
        (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
            Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);
    }
}

