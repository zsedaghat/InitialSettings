using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SpaceManagment.Model
{
    public class User : IdentityUser<long>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode{ get; set; }
        public bool IsActive { get; set; }
    }
}
