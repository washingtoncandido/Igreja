using FICR.Cooperation.Humanism.Data.Context;
using FICR.Cooperation.Humanism.Data.Contracts;
using FICR.Cooperation.Humanism.Data.Contracts.Base;
using System.Data;

namespace FICR.Cooperation.Humanism.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CooperationDbContext _dbContext;

        public UnitOfWork(CooperationDbContext dbContext)
        {
            this._dbContext = dbContext;
            ForceBeginTransaction();
        }
        private IEventRepository _eventRepository;

        private IContactRepository _contactRepository;



        #region Repo properties

        public IEventRepository EventRepository =>
            _eventRepository ??= new EventRepository(_dbContext);

        public IContactRepository ContactRepository => _contactRepository ??= new ContactRepository(_dbContext);

        #endregion


        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void ForceBeginTransaction()
        {
            _dbContext.Database.CurrentTransaction?.Dispose();
            _dbContext.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public async Task<int> SaveChangesAsync()
        {
            await CommitAsync();
            return await _dbContext.SaveChangesAsync();
        }

        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }
    }
}
