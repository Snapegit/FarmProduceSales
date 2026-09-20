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
    public class ShangjiaService : BaseService<ShangjiaDbModel>
    {
        private readonly long _uid;
        private readonly string _role;

        public ShangjiaService()
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
            return CurrentDb.GetSingle(it => it.Shangjiazhanghao == username && it.Mima == password);
        }
        
        public bool ResetPass(string username)
        {
            string mima = "123456";
            mima = FuncHelper.MD5("123456");
            return CurrentDb.Update(it => new ShangjiaDbModel() { Mima = mima }, it => it.Shangjiazhanghao == username);
        }





        public PageModel<ShangjiaDbModel> GetPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;

            string dbColumnName = Db.EntityMaintenance.GetDbColumnName<ShangjiaDbModel>(sort);
            order = order.ToLower() == "asc" ? "ASC" : "DESC";


            List<ShangjiaDbModel> ts = Db.Queryable<ShangjiaDbModel>().Where(conModels).OrderBy(dbColumnName + " " + order).ToPageList(page, limit, ref totalNumber, ref totalPage);


            PageModel<ShangjiaDbModel> t = new PageModel<ShangjiaDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<ShangjiaDbModel>()
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
