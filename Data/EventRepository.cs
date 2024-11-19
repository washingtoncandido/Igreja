using FICR.Cooperation.Humanism.Data.Context;
using FICR.Cooperation.Humanism.Data.Contracts;
using FICR.Cooperation.Humanism.Data.Repository;
using FICR.Cooperation.Humanism.Entities;
using Microsoft.EntityFrameworkCore;


namespace FICR.Cooperation.Humanism.Data
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(CooperationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
