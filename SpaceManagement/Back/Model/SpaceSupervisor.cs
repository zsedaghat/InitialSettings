using SpaceManagment.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceManagment.Model
{
    public class SpaceSupervisor /*: BaseEntity<int>*/
    {
        public int Id { get; set; }
        public long? SupervisorId { get; set; }
        public int? SpaceId { get; set; }
        public Supervisor Supervisor { get; set; }
        public Space Space { get; set; }
    }
}
