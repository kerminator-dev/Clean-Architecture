using Domain.Master;
using Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interface
{
    public interface IUnitOfWork
    {
        IRepository<AppSetting> AppSettingRepository { get; }
        IRepository<Category> CategoryRepository { get; }
         

        Task<int> SaveAsync();

        Task<int> Save();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
