using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.Core.Business.Services;
using Net.Core.Common.Helpers;
using Net.Core.Models;
using Net.Core.Models.DbModel;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Text;
using System.Net;
using NETCore.MailKit.Core;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Alipay.AopSdk.Core;
using Net.Core.Models.ConfModel;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using Net.Core.Models.ViewModel;
using Newtonsoft.Json;




namespace Net.Core.Controllers
{
    /// <summary>
    /// 商品信息相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class ShangpinxinxiController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _savePath;
        private readonly long _uid;
        private readonly string _role;
        private readonly ShangpinxinxiService _bll;
        private readonly IEmailService _EmailService;
        private readonly ConfigService _configBLL;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ShangpinxinxiController(IHostingEnvironment hostingEnvironment, IEmailService emailService)
        {
            try
            {
                _hostingEnvironment = hostingEnvironment;
                _savePath = _hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar + ConfigHelper.GetConfig("SchemaName") + Path.DirectorySeparatorChar + "file" + Path.DirectorySeparatorChar;

                if (CacheHelper.TokenModel != null)
                {
                    _uid = CacheHelper.TokenModel.Uid;
                    _role = CacheHelper.TokenModel.Role;
                }

                _EmailService = emailService;
                _configBLL = new ConfigService();
            }
            catch
            {
                _uid = 0;
                _role = "游客";
            }

            _hostingEnvironment = hostingEnvironment;
            _savePath = _hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar + ConfigHelper.GetConfig("SchemaName") + Path.DirectorySeparatorChar + "file" + Path.DirectorySeparatorChar;
            _bll = new ShangpinxinxiService();
        }


        /// <summary>
        /// 分页接口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页记录的长度</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序（默认asc）</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Page(int page = 1, int limit = 10, string sort = "id", string order = "asc")
        {
            try
            {
            	List<IConditionalModel> conModels = new List<IConditionalModel>();
                var pricestart = HttpContext.Request.Query["pricestart"].ToString();
                if (!string.IsNullOrEmpty(pricestart))
                {
                    conModels.Add(new ConditionalModel() { FieldName = "price", ConditionalType = ConditionalType.GreaterThanOrEqual, FieldValue = pricestart });
                }
                var priceend = HttpContext.Request.Query["priceend"].ToString();
                if (!string.IsNullOrEmpty(priceend))
                {
                    conModels.Add(new ConditionalModel() { FieldName = "price", ConditionalType = ConditionalType.LessThanOrEqual, FieldValue = priceend });
                }
                if(CacheHelper.TokenModel != null && CacheHelper.TokenModel.Tablename == "shangjia") {
                    conModels.Add(new ConditionalModel() { FieldName = "shangjiazhanghao", ConditionalType = ConditionalType.Equal, FieldValue = CacheHelper.TokenModel.Uname.ToString() });
                }
				ShangpinxinxiDbModel entityObj = new ShangpinxinxiDbModel();
				var hasProperty = entityObj.GetType().GetProperty(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(CacheHelper.TokenModel.UNickname));
				if (_role != "Admin" && hasProperty != null)
				{
					conModels.Add(new ConditionalModel() { FieldName = CacheHelper.TokenModel.UNickname, ConditionalType = ConditionalType.Equal, FieldValue = CacheHelper.TokenModel.Uname });
				}
                var shangpinmingcheng = HttpContext.Request.Query["shangpinmingcheng"].ToString();
                if (!string.IsNullOrEmpty(shangpinmingcheng))
                {
                    if (shangpinmingcheng.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shangpinmingcheng", ConditionalType = ConditionalType.Like, FieldValue = shangpinmingcheng });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shangpinmingcheng", ConditionalType = ConditionalType.Equal, FieldValue = shangpinmingcheng });
                    }
                }
                var shangpinfenlei = HttpContext.Request.Query["shangpinfenlei"].ToString();
                if (!string.IsNullOrEmpty(shangpinfenlei))
                {
                    if (shangpinfenlei.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shangpinfenlei", ConditionalType = ConditionalType.Like, FieldValue = shangpinfenlei });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shangpinfenlei", ConditionalType = ConditionalType.Equal, FieldValue = shangpinfenlei });
                    }
                }
                var price = HttpContext.Request.Query["price"].ToString();
                if (!string.IsNullOrEmpty(price))
                {
                    if (price.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "price", ConditionalType = ConditionalType.Like, FieldValue = price });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "price", ConditionalType = ConditionalType.Equal, FieldValue = price });
                    }
                }

                return Json(_bll.GetPageList(page, limit, sort, order, conModels));
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 分页接口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页记录的长度</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序（默认asc）</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult List(int page = 1, int limit = 10, string sort = "id", string order = "asc")
        {
            try
            {
                List<IConditionalModel> conModels = new List<IConditionalModel>();
                var pricestart = HttpContext.Request.Query["pricestart"].ToString();
                if (!string.IsNullOrEmpty(pricestart))
                {
                    conModels.Add(new ConditionalModel() { FieldName = "price", ConditionalType = ConditionalType.GreaterThanOrEqual, FieldValue = pricestart });
                }
                var priceend = HttpContext.Request.Query["priceend"].ToString();
                if (!string.IsNullOrEmpty(priceend))
                {
                    conModels.Add(new ConditionalModel() { FieldName = "price", ConditionalType = ConditionalType.LessThanOrEqual, FieldValue = priceend });
                }
                var shangpinmingcheng = HttpContext.Request.Query["shangpinmingcheng"].ToString();
				if (!string.IsNullOrEmpty(shangpinmingcheng))
                {
                    if (shangpinmingcheng.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shangpinmingcheng", ConditionalType = ConditionalType.Like, FieldValue = shangpinmingcheng });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shangpinmingcheng", ConditionalType = ConditionalType.Equal, FieldValue = shangpinmingcheng });
                    }
                }
                var shangpinfenlei = HttpContext.Request.Query["shangpinfenlei"].ToString();
				if (!string.IsNullOrEmpty(shangpinfenlei))
                {
                    if (shangpinfenlei.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shangpinfenlei", ConditionalType = ConditionalType.Like, FieldValue = shangpinfenlei });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shangpinfenlei", ConditionalType = ConditionalType.Equal, FieldValue = shangpinfenlei });
                    }
                }
                var price = HttpContext.Request.Query["price"].ToString();
				if (!string.IsNullOrEmpty(price))
                {
                    if (price.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "price", ConditionalType = ConditionalType.Like, FieldValue = price });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "price", ConditionalType = ConditionalType.Equal, FieldValue = price });
                    }
                }

                return Json(_bll.GetPageList(page, limit, sort, order, conModels));
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Save([FromBody] ShangpinxinxiDbModel entity)
        {
            try
            {
                Random rd = new Random();
                int i = rd.Next(0, 1000000000);
                entity.Id = DateTime.Now.Ticks / 100000 + i;
                if (_bll.BaseInsert(entity) > 0)
                {
                    return Json(new { Code = 0, Msg = "添加成功！" });
                }

                return Json(new { Code = -1, Msg = "添加失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add([FromBody] ShangpinxinxiDbModel entity)
        {
            try
            {

                Random rd = new Random();
                int i = rd.Next(0, 1000000000);
                entity.Id = DateTime.Now.Ticks / 100000 + i;

                if (_bll.BaseInsert(entity) > 0)
                {
                    return Json(new { Code = 0, Msg = "添加成功！" });
                }

                return Json(new { Code = -1, Msg = "添加失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 更新接口
        /// </summary>
        /// <param name="entity">更新实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Update([FromBody] ShangpinxinxiDbModel entity)
        {
			if (entity == null)
			{
				return Json(new { Code = 0, Msg = "" });
			}

            try
            {
                if (_bll.BaseUpdate(entity))
                {
                    return Json(new { Code = 0, Msg = "编辑成功！" });
                }

                return Json(new { Code = -1, Msg = "编辑失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="ids">主键int[]</param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = "Admin,Client")]
        public JsonResult Delete([FromBody] dynamic[] ids)
        {
            try
            {
                if (_bll.BaseDels(ids))
                {
                    return Json(new { Code = 0, Msg = "删除成功！" });
                }

                return Json(new { Code = -1, Msg = "删除失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 详情接口
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Info(long id)
        {
            try
            {
                _bll.BrowseClick(id);
                return Json(new { Code = 0, Data = _bll.BaseGetById(id) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 详情接口
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [HttpGet("{id}")]
        public JsonResult Detail(long id)
        {
            try
            {
                _bll.BrowseClick(id);
                return Json(new { Code = 0, Data = _bll.BaseGetById(id) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        


		/// <summary>
        /// 获取需要提醒的记录数接口
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="type">类型（1表示数字比较提醒，2表示日期比较提醒）</param>
        /// <param name="remindStart">remindStart小于等于columnName满足条件提醒,当比较日期时，该值表示天数</param>
        /// <param name="remindEnd">columnName小于等于remindEnd 满足条件提醒,当比较日期时，该值表示天数</param>
        /// <returns></returns>
        [HttpGet("{columnName}/{type}")]
        public JsonResult Remind(string columnName, int type, int remindStart=-1, int remindEnd=-1)
        {
            try
            {
                string where = "";
                if (CacheHelper.TokenModel.Role == "Client")
                {
                    where += " AND shangjiazhanghao = '" + CacheHelper.TokenModel.Uname.ToString() + "' ";
                }
                return Json(new { Code = 0, Count = _bll.Common("shangpinxinxi", columnName, "", type, "remind", remindStart, remindEnd, where) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }





        /// <summary>
        /// 按值统计接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="xColumnName">列名</param>
        /// <param name="yColumnName">列名</param>
        /// <returns></returns>
        [HttpGet("{xColumnName}/{yColumnName}")]
        public JsonResult Value(string xColumnName, string yColumnName)
        {
            try
            {
                string where = " WHERE 1 = 1 ";
                string tableName = CacheHelper.TokenModel.Tablename;
                if (tableName == "shangjia")
                {
                    where += " AND shangjiazhanghao = '" + CacheHelper.TokenModel.Uname.ToString() + "' ";
                }
                if ("shangpinxinxi" == "orders")
                {
                    where += " AND status IN ('已支付', '已发货', '已完成') ";
                }

                return Json(new { Code = 0, Data = _bll.Common("shangpinxinxi", xColumnName, yColumnName, 0, "value", 0, 0, where) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 按时间统计类型接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="xColumnName">列名</param>
        /// <param name="yColumnName">列名</param>
        /// <param name="timeStatType">类型</param>
        /// <returns></returns>
        [HttpGet("{xColumnName}/{yColumnName}/{timeStatType}")]
        public JsonResult Value(string xColumnName, string yColumnName, string timeStatType)
        {
            try
            {
                string where = " WHERE 1 = 1 ";
                if (CacheHelper.TokenModel.Role == "Client")
                {
                    where += " AND shangjiazhanghao = '" + CacheHelper.TokenModel.Uname.ToString() + "' ";
                }
                if ("shangpinxinxi" == "orders")
                {
                    where += " AND status IN ('已支付', '已发货', '已完成') ";
                }

                return Json(new { Code = 0, Data = _bll.StatDate("shangpinxinxi", xColumnName, yColumnName, timeStatType, where) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 类别统计接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        [HttpGet("{columnName}")]
        public JsonResult Group(string columnName)
        {
            try
            {
                string where = " WHERE 1 = 1 ";
                if (CacheHelper.TokenModel.Role == "Client" && CacheHelper.TokenModel.UNickname == "shangjiazhanghao")
                {
                    where += " AND shangjiazhanghao = '" + CacheHelper.TokenModel.Uname.ToString() + "' ";
                }

                return Json(new { Code = 0, Data = _bll.Common("shangpinxinxi", columnName, "", 0, "group", 0, 0, where) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }













    }
}
