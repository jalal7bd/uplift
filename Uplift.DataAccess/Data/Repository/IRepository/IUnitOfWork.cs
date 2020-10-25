using System;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; }
        IFrequencyRepository Frequency { get; }
        IProductRepository Product { get; }
        IOrderHeaderRepository OrderHeader{ get; }
        IOrderDetailsRepository OrderDetails { get; }
        IUserRepository User { get; }
        void Save();
    }
}
