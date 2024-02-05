using AutoMapper;
using SmartCardCRM.Data.Entities;

namespace SmartCardCRM.Data
{
    public abstract class BaseData
    {
        protected SmartCardCRMContext _context;
        protected IConfigurationProvider MapperConfig { get; set; }

        public BaseData()
        {
            MapperConfig = AutoMapperConfiguration.Configure();
        }
    }
}
