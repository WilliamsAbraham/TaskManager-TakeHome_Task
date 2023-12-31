﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbset;
        private readonly ApplicationContext Context;
        public Repository(ApplicationContext _context)
        {
               Context = _context;
               dbset = Context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
          var res =  await dbset.AddAsync(entity); 
            if(res.Entity == null)
            {

                throw new InvalidOperationException("Something went wrong");
            }
            await Context.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(TEntity entity)
        {
            dbset.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbset.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
          return await  dbset.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            dbset.Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}
