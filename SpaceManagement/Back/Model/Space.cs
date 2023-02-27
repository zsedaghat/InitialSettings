using SpaceManagment.Infrastructure;

namespace SpaceManagment.Model
{
    public class Space 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public long HostId { get; set; }
        public Host Host { get; set; }
        public List<SpaceSupervisor> SpaceSupervisors { get; set; }
        public List<SpaceActiveInterval> SpaceActiveIntervals { get; set; }
    }
}
