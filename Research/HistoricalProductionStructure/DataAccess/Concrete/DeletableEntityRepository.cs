﻿namespace HistoricalProductionStructure.DataAccess.Concrete
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Contracts;
    using Domain.Contracts;

    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T> 
        where T : class, IDeletableEntity, IEntity
    {
        public DeletableEntityRepository(IDbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => x.IsDeleted != true);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public override void Delete(T entity)
        {
            var entry = this.Context.Entry(entity);
            entity.DeletedOn = DateTime.Now;
            entity.IsDeleted = true;
            entry.State = EntityState.Modified;
        }
    }
}