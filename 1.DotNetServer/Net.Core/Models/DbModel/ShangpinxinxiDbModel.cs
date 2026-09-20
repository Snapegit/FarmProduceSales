using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Net.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 商品信息
    /// </summary>
    [SugarTable("shangpinxinxi")]
	public class ShangpinxinxiDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }
		/// <summary>
		/// Desc: 商品名称
		/// </summary>
		[SugarColumn(ColumnName = "shangpinmingcheng")]
		public string Shangpinmingcheng { get; set; }
		/// <summary>
		/// Desc: 商品分类
		/// </summary>
		[SugarColumn(ColumnName = "shangpinfenlei")]
		public string Shangpinfenlei { get; set; }
		/// <summary>
		/// Desc: 商品图片
		/// </summary>
		[SugarColumn(ColumnName = "shangpintupian")]
		public string Shangpintupian { get; set; }
		/// <summary>
		/// Desc: 产地
		/// </summary>
		[SugarColumn(ColumnName = "chandi")]
		public string Chandi { get; set; }
		/// <summary>
		/// Desc: 规格
		/// </summary>
		[SugarColumn(ColumnName = "guige")]
		public string Guige { get; set; }
		/// <summary>
		/// Desc: 商品详情
		/// </summary>
		[SugarColumn(ColumnName = "shangpinxiangqing")]
		public string Shangpinxiangqing { get; set; }
		/// <summary>
		/// Desc: 收藏数量
		/// </summary>
        [SugarColumn(ColumnName = "storeupnum")]
		public int? Storeupnum { get; set; } = 0;
		/// <summary>
		/// Desc: 点击次数
		/// </summary>
        [SugarColumn(ColumnName = "clicknum")]
		public int? Clicknum { get; set; } = 0;
		/// <summary>
		/// Desc: 单限
		/// </summary>
        [SugarColumn(ColumnName = "onelimittimes")]
		public int? Onelimittimes { get; set; } = 0;
		/// <summary>
		/// Desc: 库存
		/// </summary>
        [SugarColumn(ColumnName = "alllimittimes")]
		public int? Alllimittimes { get; set; } = 0;
		/// <summary>
		/// Desc: 价格
		/// </summary>
        [SugarColumn(ColumnName = "price")]
		public double? Price { get; set; }
		/// <summary>
		/// Desc: 商家账号
		/// </summary>
		[SugarColumn(ColumnName = "shangjiazhanghao")]
		public string Shangjiazhanghao { get; set; }
		/// <summary>
		/// Desc: 商家名称
		/// </summary>
		[SugarColumn(ColumnName = "shangjiamingcheng")]
		public string Shangjiamingcheng { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
