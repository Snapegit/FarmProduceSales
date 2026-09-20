using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Net.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 商家
    /// </summary>
    [SugarTable("shangjia")]
	public class ShangjiaDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }
		/// <summary>
		/// Desc: 商家账号
		/// </summary>
		[SugarColumn(ColumnName = "shangjiazhanghao")]
		public string Shangjiazhanghao { get; set; }
		/// <summary>
		/// Desc: 密码
		/// </summary>
		[SugarColumn(ColumnName = "mima")]
		public string Mima { get; set; }
		/// <summary>
		/// Desc: 商家名称
		/// </summary>
		[SugarColumn(ColumnName = "shangjiamingcheng")]
		public string Shangjiamingcheng { get; set; }
		/// <summary>
		/// Desc: 头像
		/// </summary>
		[SugarColumn(ColumnName = "touxiang")]
		public string Touxiang { get; set; }
		/// <summary>
		/// Desc: 联系人
		/// </summary>
		[SugarColumn(ColumnName = "lianxiren")]
		public string Lianxiren { get; set; }
		/// <summary>
		/// Desc: 联系方式
		/// </summary>
		[SugarColumn(ColumnName = "lianxifangshi")]
		public string Lianxifangshi { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
