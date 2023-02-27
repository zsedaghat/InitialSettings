using SpaceManagment.Infrastructure;

namespace SpaceManagment.Model
{
    public class Reservation /*: BaseEntity<int>*/
    {
        public int Id { get; set; }
        public int? SpaceActiveIntervalId { get; set; }
        public long? ClientId { get; set; }
        public long? SupervisorId { get; set; }
        public int LastSupervisorId { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int? SpaceSupervisorId { get; set; }
        public InitiatorType InitiatorType { get; set; }
        public ReservationStatus Status { get; set; }
     //  public virtual Client Client { get; set; }
        public SpaceSupervisor SpaceSupervisor { get; set; }
        public SpaceActiveInterval SpaceActiveInterval { get; set; }
        public Supervisor Supervisor { get; set; }
    }
}
