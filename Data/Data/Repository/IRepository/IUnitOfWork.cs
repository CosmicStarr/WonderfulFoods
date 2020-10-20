using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Data.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository CategoryRepository { get;}
        void Save();
    }
}
