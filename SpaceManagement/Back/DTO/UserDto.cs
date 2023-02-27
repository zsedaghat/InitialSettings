using SpaceManagment.Model;
using System.ComponentModel.DataAnnotations;

namespace SpaceManagment.DTO
{
    public class UserDto
    {
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
