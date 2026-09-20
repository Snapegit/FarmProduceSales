using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net.Core.Common.Helpers;
using Net.Core.Models;
using Net.Core.Models.DbModel;


namespace Net.Core.Business.Services
{
    public class ShangpinfenleiService : BaseService<ShangpinfenleiDbModel>
    {
        private readonly long _uid;
        private readonly string _role;

        public ShangpinfenleiService()
        {
            try
            {
                if (CacheHelper.TokenModel != null)
                {
                    _uid = CacheHelper.TokenModel.Uid;
                    _role = CacheHelper.TokenModel.Role;
                }
            }
            catch
            {
                _uid = 0;
                _role = "游客";
            }
        }






        public PageModel<ShangpinfenleiDbModel> GetPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;

            string dbColumnName = Db.EntityMaintenance.GetDbColumnName<ShangpinfenleiDbModel>(sort);
            order = order.ToLower() == "asc" ? "ASC" : "DESC";


            List<ShangpinfenleiDbModel> ts = Db.Queryable<ShangpinfenleiDbModel>().Where(conModels).OrderBy(dbColumnName + " " + order).ToPageList(page, limit, ref totalNumber, ref totalPage);


            PageModel<ShangpinfenleiDbModel> t = new PageModel<ShangpinfenleiDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<ShangpinfenleiDbModel>()
                {
                    Total = totalNumber,
                    PageSize = limit,
                    TotalPage = totalPage,
                    CurrPage = page,
                    List = ts
                }
            };

            return t;
        }








    }
}
