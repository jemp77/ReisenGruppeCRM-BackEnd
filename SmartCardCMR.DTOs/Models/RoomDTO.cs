using System.Collections.Generic;

namespace SmartCardCRM.Model.Models
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int IdQuoter { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public List<int> KidsAges { get; set; }
    }
}
