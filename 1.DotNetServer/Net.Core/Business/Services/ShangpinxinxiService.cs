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
    public class ShangpinxinxiService : BaseService<ShangpinxinxiDbModel>
    {
        private readonly long _uid;
        private readonly string _role;

        public ShangpinxinxiService()
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




		public int BrowseClick(long id)
        {
            ShangpinxinxiDbModel updateObj = new ShangpinxinxiDbModel();
            return Db.Updateable(updateObj).UpdateColumns(it => new { it.Clicknum }).ReSetValue(it => it.Clicknum == (it.Clicknum + 1)).Where(it => it.Id == id).ExecuteCommand();
        }


        public PageModel<ShangpinxinxiDbModel> GetPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;

            string dbColumnName = Db.EntityMaintenance.GetDbColumnName<ShangpinxinxiDbModel>(sort);
            order = order.ToLower() == "asc" ? "ASC" : "DESC";


            List<ShangpinxinxiDbModel> ts = Db.Queryable<ShangpinxinxiDbModel>().Where(conModels).OrderBy(dbColumnName + " " + order).ToPageList(page, limit, ref totalNumber, ref totalPage);


            PageModel<ShangpinxinxiDbModel> t = new PageModel<ShangpinxinxiDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<ShangpinxinxiDbModel>()
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
