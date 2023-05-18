using System;
using Microsoft.EntityFrameworkCore;

namespace TrackApp.Repository
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        IEnumerable<TEntity> GetAll();
    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:class
	{
        TrackAppDbContext _dbContext;
        DbSet<TEntity> dbset;
		public Repository(TrackAppDbContext db)
		{
            _dbContext = db;
            dbset = _dbContext.Set<TEntity>();
		}

        public void Add(TEntity obj)
        {
            _dbContext.Add(obj);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbset.AsEnumerable();
        }

        public void Remove(TEntity obj)
        {
            _dbContext.Remove(obj);
            _dbContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}

