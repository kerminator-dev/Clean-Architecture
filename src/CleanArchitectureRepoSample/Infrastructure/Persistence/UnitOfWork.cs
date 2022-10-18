using Application.Common.Interface;
using Domain.Master;
using Domain.Product;
using Infrastructure.Persistence.Configuration;
using Infrastructure.Persistence.DbContexts;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        private IRepository<AppSetting> _appSettingRepository;

        private IRepository<Category> _categoryRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<AppSetting> AppSettingRepository
        {
            get
            {
                if (_appSettingRepository == null)
                    this._appSettingRepository = new EFRepository<AppSetting>(_context);

                return _appSettingRepository;
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    this._categoryRepository = new EFRepository<Category>(_context);

                return _categoryRepository;
            }
        }

        public void BeginTransaction()
        {
            // throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            // throw new NotImplementedException();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RollbackTransaction()
        {
            // throw new NotImplementedException();
        }

        public Task<int> Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChanges();
        }
    }
}
