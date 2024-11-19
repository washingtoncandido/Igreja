using FICR.Cooperation.Humanism.Entities.DTO.Base;

namespace FICR.Cooperation.Humanism.Entities.DTO.EventoDTO
{
    public class EventOutputDTO : BaseOutputDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public string imagePath { get; set; }
        public string scheduling { get; set; }
    }
}
