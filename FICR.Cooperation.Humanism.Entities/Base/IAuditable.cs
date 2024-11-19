
namespace FICR.Cooperation.Humanism.Entities.Base
{
    internal interface IAuditable
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
