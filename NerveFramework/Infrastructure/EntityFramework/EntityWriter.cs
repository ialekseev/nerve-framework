using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NerveFramework.Core.Entities;

namespace NerveFramework.Infrastructure.EntityFramework
{
    internal class EntityWriter : IWriteEntities
    {
        private readonly DbContext _context;

        public EntityWriter(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
        }

        public IQueryable<TEntity> EagerLoad<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression) where TEntity : Entity
        {
            // Include will eager load data into the query
            if (query != null && expression != null) query = query.Include(expression);
            return query;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
        {
            // AsNoTracking returns entities that are not attached to the DbContext
            return new EntitySet<TEntity>(_context.Set<TEntity>().AsNoTracking(), this);
        }

        public TEntity Get<TEntity>(object firstKeyValue, params object[] otherKeyValues) where TEntity : Entity
        {
            if (firstKeyValue == null) throw new ArgumentNullException("firstKeyValue");
            var keyValues = new List<object> { firstKeyValue };
            if (otherKeyValues != null) keyValues.AddRange(otherKeyValues);
            return _context.Set<TEntity>().Find(keyValues.ToArray());
        }

        public Task<TEntity> GetAsync<TEntity>(object firstKeyValue, params object[] otherKeyValues) where TEntity : Entity
        {
            if (firstKeyValue == null) throw new ArgumentNullException("firstKeyValue");
            var keyValues = new List<object> { firstKeyValue };
            if (otherKeyValues != null) keyValues.AddRange(otherKeyValues);
            return _context.Set<TEntity>().FindAsync(keyValues.ToArray());
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : Entity
        {
            return new EntitySet<TEntity>(_context.Set<TEntity>(), this);
        }

        public void Create<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (_context.Entry(entity).State == EntityState.Detached) _context.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (_context.Entry(entity).State != EntityState.Deleted)
                _context.Set<TEntity>().Remove(entity);
        }

        public void Reload<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Entry(entity).Reload();
        }

        public Task ReloadAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            return _context.Entry(entity).ReloadAsync();
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                var sb = new StringBuilder();
                sb.AppendLine(exception.Message);
                foreach (var validationError in exception.EntityValidationErrors)
                {
                    sb.AppendLine(String.Format("Entity \"{0}\" in state \"{1}\", errors:", validationError.Entry.Entity.GetType().Name, validationError.Entry.State));
                    foreach (var error in validationError.ValidationErrors)
                    {
                        sb.AppendLine(String.Format("\t(Property: \"{0}\", Error: \"{1}\")", error.PropertyName, error.ErrorMessage));
                    }
                }
                throw new DbEntityValidationException(sb.ToString(), exception);
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void DiscardChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries().Where(x => x != null))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public Task DiscardChangesAsync()
        {
            var reloadTasks = new List<Task>();
            foreach (var entry in _context.ChangeTracker.Entries().Where(x => x != null))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        reloadTasks.Add(entry.ReloadAsync());
                        break;
                }
            }
            return Task.WhenAll(reloadTasks);
        }
    }
}
