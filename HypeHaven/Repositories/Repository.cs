﻿using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HypeHaven.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HypeHavenContext _context;

        public Repository(HypeHavenContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return Save();
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Save();
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        //not implemented
        #region not implemented
        public async Task<IEnumerable<T>> GetAllForSpecifedUser()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
