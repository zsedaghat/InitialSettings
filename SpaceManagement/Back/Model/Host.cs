using Microsoft.Extensions.Hosting;
using SpaceManagment.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceManagment.Model
{
    public class Host : User
    {
        public string OrganizationName { get; set; }
        public List<Space> Spaces { get; set; }
        public List<Supervisor> Supervisors { get; set; }
    }
}
