using SpaceManagment.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceManagment.Model
{
    public class Supervisor :User
    {
        public long? HostId { get; set; }
        public List<SpaceSupervisor> SpaceSupervisors { get; set; }
        public List<Reservation> Reservations { get; set; }
        public  Host Host { get; set; }
    }
}
