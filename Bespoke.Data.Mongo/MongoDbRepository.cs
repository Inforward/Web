using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Bespoke.Data.Mongo
{
    public abstract class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected MongoDatabase database;
        protected MongoCollection<TEntity> collection;

        protected MongoDbRepository()
        {
            GetDatabase();
            GetCollection();
        }

        public bool Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = entity.CreateDate;

            return collection.Insert(entity).Ok;
        }

        public bool Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;

            return collection.Save(entity).DocumentsAffected > 0;
        }

        public bool Delete(TEntity entity)
        {
            return collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.AsQueryable<TEntity>().Where(predicate.Compile()).ToList();
        }

        public IList<TEntity> GetAll()
        {
            return collection.FindAllAs<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return collection.FindOneByIdAs<TEntity>(id);
        }

        protected void GetDatabase()
        {
            var client = new MongoClient(GetConnectionString());
            var server = client.GetServer();

            database = server.GetDatabase(GetDatabaseName());
        }

        protected string GetConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("MongoDbConnectionString").Replace("{DB_NAME}", GetDatabaseName());
        }

        protected string GetDatabaseName()
        {
            return ConfigurationManager.AppSettings.Get("MongoDbDatabaseName");
        }

        protected void GetCollection()
        {
            collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}
