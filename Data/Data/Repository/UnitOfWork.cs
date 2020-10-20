using Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using WonderfulFoods.Data.Data;

namespace Data.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(db);
        }
        public ICategoryRepository CategoryRepository { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
