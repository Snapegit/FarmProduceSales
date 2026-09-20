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
    public class StoreupService : BaseService<StoreupDbModel>
    {
        private readonly long _uid;
        private readonly string _role;

        public StoreupService()
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






        public PageModel<StoreupDbModel> GetPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;

            string dbColumnName = Db.EntityMaintenance.GetDbColumnName<StoreupDbModel>(sort);
            order = order.ToLower() == "asc" ? "ASC" : "DESC";


            List<StoreupDbModel> ts = Db.Queryable<StoreupDbModel>().Where(conModels).OrderBy(dbColumnName + " " + order).ToPageList(page, limit, ref totalNumber, ref totalPage);


            PageModel<StoreupDbModel> t = new PageModel<StoreupDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<StoreupDbModel>()
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
