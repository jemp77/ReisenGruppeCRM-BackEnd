using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class ConfigurationSettings
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsHidden { get; set; }
    }
}
