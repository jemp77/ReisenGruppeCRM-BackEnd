namespace SmartCardCRM.Model.Models
{
    public class UserRolesDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public RoleDTO Role { get; set; }
    }
}
