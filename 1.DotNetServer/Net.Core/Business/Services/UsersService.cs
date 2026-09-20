using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net.Core.Models.DbModel;

namespace Net.Core.Business.Services
{
    public class UsersService : BaseService<UsersDbModel>
    {
        public dynamic Login(string username, string password)
        {
            return CurrentDb.GetSingle(it => it.Username == username && it.Password == password);
        }
    }
}
