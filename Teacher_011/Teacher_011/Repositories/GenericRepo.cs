using Teacher_011.Models;
using Teacher_011.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Teacher_011.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class, new()
    {
        DbContext db;
        DbSet<T> dbSet;
        public GenericRepo(DbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }

        public T Get(Expression<Func<T, bool>> predicate, string include = "")
        {
            if(include == "")
                return dbSet.FirstOrDefault(predicate);
            else
                return dbSet.Include(include).FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll(string include = "")
        {
            if (include == "")
                return dbSet.ToList();
            else
                return dbSet.Include(include).ToList();
        }

        public void Insert(T item)
        {
            dbSet.Add(item);
            db.SaveChanges();
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var item = dbSet.FirstOrDefault(predicate);
            if (item !=null)
            {
                dbSet.Remove(item);
                db.SaveChanges();
            }
        }

        public void ExecuteCommand(string sql)
        {
            db.Database.ExecuteSqlCommand(sql);
        }

        public IEnumerable<K> ExecuteSqlCollection<K>(string sql) where K : class, new()
        {
            return db.Database.SqlQuery<K>(sql).ToList();
        }

        public K ExecuteSqlSingle<K>(string sql) where K : class, new()
        {
             return db.Database.SqlQuery<K>(sql).FirstOrDefault();
        }

        
    }
}