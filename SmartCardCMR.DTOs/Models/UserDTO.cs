using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartCardCRM.Model.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsActive { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<UserRolesDTO> UserRoles { get; set; }
    }
}
