using System.ComponentModel.DataAnnotations;

namespace SpaceManagment.DTO
{
    public class HostDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;
        //[Required]
        [StringLength(500)]
        public string Password { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string OrganizationName { get; set; }

    }
}
