using FICR.Cooperation.Humanism.Entities.Base;

namespace FICR.Cooperation.Humanism.Entities
{
    public class Event : BaseEntity
    {
        public string title { get; set; }
        public string description { get; set; }
        public string imagePath { get; set; }
        public DateTime scheduling { get; set; } 
        public Event()
        { 
        }
        public Event(string title, string description, string imagePath, DateTime scheduling)
        {
            this.title = title;
            this.description = description;
            this.imagePath = imagePath;
            this.scheduling = scheduling;
        }
    }
}
