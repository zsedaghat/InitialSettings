using SpaceManagment.Model;

namespace SpaceManagment.DTO
{
    public class UserRoles
    {
        public UserDto user { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
