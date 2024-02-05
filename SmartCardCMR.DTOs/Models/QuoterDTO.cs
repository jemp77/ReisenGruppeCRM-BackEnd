using System;
using System.Collections.Generic;

namespace SmartCardCRM.Model.Models
{
    public class QuoterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Destination { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public List<RoomDTO> Rooms { get; set; }
    }
}
