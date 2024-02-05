using System;
using System.Collections.Generic;

namespace SmartCardCRM.Data.Entities
{
    public partial class Quoter
    {
        public Quoter()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Destination { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
