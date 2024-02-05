using Microsoft.AspNetCore.Mvc;
using SmartCardCRM.Data;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using System.Collections.Generic;

namespace SmartCardCRM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationSettingsController : ControllerBase
    {
        private readonly ConfigurationSettingsData ConfigurationSettingsData;

        public ConfigurationSettingsController(SmartCardCRMContext context)
        {
            ConfigurationSettingsData = new ConfigurationSettingsData(context);
        }

        [HttpGet]
        public ActionResult<dynamic> GetConfigurationSettings()
        {
            return new { data = ConfigurationSettingsData.GetAllConfigurations() };
        }

        [HttpGet("{key}")]
        public dynamic GetConfiguration(string key)
        {
            return new { data = new { value = ConfigurationSettingsData.GetValue(key).Value } };
        }

        [HttpPost]
        [Route("[action]")]
        public void PostUpdateConfigurations(List<ConfigurationSettingsDTO> listConfigurationSettingsDTO)
        {
            ConfigurationSettingsData.UpdateConfigurations(listConfigurationSettingsDTO);
        }
    }
}
