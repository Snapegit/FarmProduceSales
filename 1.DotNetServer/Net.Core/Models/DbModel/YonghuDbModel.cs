using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Net.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 用户
    /// </summary>
    [SugarTable("yonghu")]
	public class YonghuDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }
		/// <summary>
		/// Desc: 用户账号
		/// </summary>
		[SugarColumn(ColumnName = "yonghuzhanghao")]
		public string Yonghuzhanghao { get; set; }
		/// <summary>
		/// Desc: 用户密码
		/// </summary>
		[SugarColumn(ColumnName = "yonghumima")]
		public string Yonghumima { get; set; }
		/// <summary>
		/// Desc: 用户姓名
		/// </summary>
		[SugarColumn(ColumnName = "yonghuxingming")]
		public string Yonghuxingming { get; set; }
		/// <summary>
		/// Desc: 头像
		/// </summary>
		[SugarColumn(ColumnName = "touxiang")]
		public string Touxiang { get; set; }
		/// <summary>
		/// Desc: 性别
		/// </summary>
		[SugarColumn(ColumnName = "xingbie")]
		public string Xingbie { get; set; }
		/// <summary>
		/// Desc: 手机号码
		/// </summary>
		[SugarColumn(ColumnName = "shoujihaoma")]
		public string Shoujihaoma { get; set; }
		/// <summary>
		/// Desc: 邮箱
		/// </summary>
		[SugarColumn(ColumnName = "youxiang")]
		public string Youxiang { get; set; }
		/// <summary>
		/// Desc: 余额
		/// </summary>
        [SugarColumn(ColumnName = "money")]
		public double? Money { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
