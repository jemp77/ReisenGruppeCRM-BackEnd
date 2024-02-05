using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class ExceptionLog
    {
        public int Id { get; set; }
        public string ComponentName { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionTraceLog { get; set; }
        public DateTime ExceptionDate { get; set; }
    }
}
