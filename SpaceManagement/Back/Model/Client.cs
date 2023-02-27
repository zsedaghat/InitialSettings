using SpaceManagment.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceManagment.Model
{
    public class Client : User 
    {
        public virtual List<Reservation> Reservations { get; set; }
    }
}
