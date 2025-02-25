﻿using DExpSql;
using MDbContext;
using Project.AppCore.Repositories;
using Project.Common.Attributes;
using System.Data;
using System.Linq.Expressions;

namespace Project.AppCore
{
    [IgnoreAutoInject]
    public class RepositoryBase<T> : LightDb, IRepositoryBase<T>
    {
        public virtual Task<int> DeleteAsync(Expression<Func<T, bool>>? whereExpression)
        {
            Db.DbSet.Delete<T>()
                .Where(whereExpression);
            return Db.ExecuteAsync();
        }

        public virtual Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? whereExpression, int index = 0, int size = 0, Expression<Func<T, object>>? orderByExpression = null, bool asc = true)
        {
            var context = Db;
            var sql = context.DbSet.Select<T>();
            if (whereExpression is null) whereExpression = e => true;
            sql = sql.Where(whereExpression);
            if (orderByExpression != null)
            {
                if (asc)
                {
                    sql = sql.OrderByAsc(orderByExpression);
                }
                else
                {
                    sql = sql.OrderByDesc(orderByExpression);
                }
            }
            if (index * size > 0)
            {
                sql = sql.Paging(index, size);
            }
            return context.QueryAsync<T>();
        }

        public Task<int> GetCountAsync(Expression<Func<T, bool>>? whereExpression)
        {
            Db.DbSet.Count<T>().Where(whereExpression);
            return Db.SingleAsync<int>();
        }

        public virtual Task<T> GetSingleAsync(Expression<Func<T, bool>>? whereExpression)
        {
            Db.DbSet.Select<T>().Where(whereExpression);
            return Db.SingleAsync<T>();
        }

        public virtual async Task<T> InsertAsync(T item)
        {
            Db.DbSet.Insert(item);
            var flag = await Db.ExecuteAsync();
            return flag > 0 ? item : default;
        }

        public virtual Task<int> UpdateAsync(T item, Expression<Func<T, bool>>? whereExpression)
        {
            Db.DbSet.Update(item).Where(whereExpression);
            return Db.ExecuteAsync();
        }

        public virtual Task<int> UpdateAsync(Expression<Func<object>> updateExpression, Expression<Func<T, bool>>? whereExpression)
        {
            Db.DbSet.Update<T>(updateExpression).Where(whereExpression);
            return Db.ExecuteAsync();
        }

        public virtual ExpressionSqlCore<T> Select()
        {
            return Db.DbSet.Select<T>();
        }

        public virtual ExpressionSqlCore<T> Count()
        {
            return Db.DbSet.Count<T>();
        }

        public virtual Task<IEnumerable<M>> Query<M>()
        {
            return Db.QueryAsync<M>();
        }
        public Task<IEnumerable<M>> Query<M>(string sql, object param)
        {
            return Db.QueryAsync<M>(sql, param);
        }
        public virtual Task<DataTable> Table()
        {
            return Db.QueryDataTableAsync();
        }

        public Task<DataTable> Table(string sql, object param)
        {
            return Db.QueryDataTableAsync(sql, param);
        }
        public virtual Task<M> Single<M>()
        {
            return Db.SingleAsync<M>();
        }
        public Task<M> Single<M>(string sql, object param)
        {
            return Db.SingleAsync<M>(sql, param);
        }
        public Task<M> Request<M>(Func<DbContext, Task<M>> func)
        {
            return func.Invoke(Db);
        }


    }
}
