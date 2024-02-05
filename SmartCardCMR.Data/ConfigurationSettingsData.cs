using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartCardCRM.Data
{
    public class ConfigurationSettingsData : BaseData
    {
        public ConfigurationSettingsData(SmartCardCRMContext context)
        {
            _context = context;
        }

        public List<ConfigurationSettingsDTO> GetAllConfigurations()
        {
            return new Mapper(MapperConfig).Map<List<ConfigurationSettingsDTO>>(_context.ConfigurationSettings.Where(x => x.IsHidden == false).ToList());
        }

        public ConfigurationSettingsDTO GetValue(string key)
        {
            return new Mapper(MapperConfig).Map<ConfigurationSettingsDTO>(_context.ConfigurationSettings.Where(x => x.Key == key).FirstOrDefault());
        }

        public void UpdateConfigurations(List<ConfigurationSettingsDTO> listConfigurationSettingsDTO)
        {
            listConfigurationSettingsDTO.ForEach(x => { x.Value = x.Value.Replace(", ", ",").Replace(" ,", ",").TrimStart().TrimEnd(); });
            var listConfigurationSettings = new Mapper(MapperConfig).Map<List<ConfigurationSettings>>(listConfigurationSettingsDTO);
            _context.ConfigurationSettings.UpdateRange(listConfigurationSettings);
            _context.SaveChanges();
        }

        public void UpdateValue(string key, string value)
        {
            var config = _context.ConfigurationSettings.Where(x => x.Key == key).FirstOrDefault();
            config.Value = value;
            _context.Entry(config).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
