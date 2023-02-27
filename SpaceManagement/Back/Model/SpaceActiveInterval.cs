using SpaceManagment.Infrastructure;

namespace SpaceManagment.Model
{
    public class SpaceActiveInterval /*: BaseEntity<int>*/
    {
        public int Id { get; set; }
        public int? SpaceId { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public Space Space { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
