using FICR.Cooperation.Humanism.Data.Contracts;
using FICR.Cooperation.Humanism.Data.Repository;
using FICR.Cooperation.Humanism.Entities;
using Microsoft.EntityFrameworkCore;


namespace FICR.Cooperation.Humanism.Data
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
