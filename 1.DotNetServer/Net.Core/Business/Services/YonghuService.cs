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
    public class YonghuService : BaseService<YonghuDbModel>
    {
        private readonly long _uid;
        private readonly string _role;

        public YonghuService()
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

		public dynamic Login(string username, string password)
        {
            return CurrentDb.GetSingle(it => it.Yonghuzhanghao == username && it.Yonghumima == password);
        }
        
        public bool ResetPass(string username)
        {
            string mima = "123456";
            mima = FuncHelper.MD5("123456");
            return CurrentDb.Update(it => new YonghuDbModel() { Yonghumima = mima }, it => it.Yonghuzhanghao == username);
        }





        public PageModel<YonghuDbModel> GetPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;

            string dbColumnName = Db.EntityMaintenance.GetDbColumnName<YonghuDbModel>(sort);
            order = order.ToLower() == "asc" ? "ASC" : "DESC";


            List<YonghuDbModel> ts = Db.Queryable<YonghuDbModel>().Where(conModels).OrderBy(dbColumnName + " " + order).ToPageList(page, limit, ref totalNumber, ref totalPage);


            PageModel<YonghuDbModel> t = new PageModel<YonghuDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<YonghuDbModel>()
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
