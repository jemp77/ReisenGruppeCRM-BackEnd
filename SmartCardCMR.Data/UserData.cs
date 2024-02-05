using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCardCRM.Data
{
    public class UserData : BaseData
    {
        public UserData(SmartCardCRMContext context)
        {
            _context = context;
        }

        public List<UserDTO> Login(string userName, string password)
        {
            return new ExceptionLogData<List<UserDTO>>(_context).Log(this.GetType().Name,
                () => {
                    var listUser = _context.User.Where(x => x.UserName == userName
                    && x.Password == password
                    && x.IsActive == true).Include(x => x.UserRoles).ThenInclude(x => x.Role);
                    return new Mapper(MapperConfig).Map<List<UserDTO>>(listUser);
                });
        }
    }
}
