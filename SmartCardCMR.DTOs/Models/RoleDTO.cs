using Newtonsoft.Json;

namespace SmartCardCRM.Model.Models
{
    public class RoleDTO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }
        public string RoleName { get; set; }
        public string[] Scopes { get; set; }
    }
}
