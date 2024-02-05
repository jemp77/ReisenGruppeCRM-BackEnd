namespace SmartCardCRM.Model.Models
{
    public class ConfigurationSettingsDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsEnabled { get; set; }
    }
}
