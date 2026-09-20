using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Net.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 我的收藏
    /// </summary>
    [SugarTable("storeup")]
	public class StoreupDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }
		/// <summary>
		/// Desc: refid
		/// </summary>
        [SugarColumn(ColumnName = "refid")]
		public long? Refid { get; set; } = 0;
		/// <summary>
		/// Desc: 表名
		/// </summary>
		[SugarColumn(ColumnName = "tablename")]
		public string Tablename { get; set; }
		/// <summary>
		/// Desc: 名称
		/// </summary>
		[SugarColumn(ColumnName = "name")]
		public string Name { get; set; }
		/// <summary>
		/// Desc: 图片
		/// </summary>
		[SugarColumn(ColumnName = "picture")]
		public string Picture { get; set; }
		/// <summary>
		/// Desc: 类型(1:收藏,21:赞,22:踩,31:竞拍参与,41:关注)
		/// </summary>
		[SugarColumn(ColumnName = "type")]
		public string Type { get; set; }
		/// <summary>
		/// Desc: 推荐类型
		/// </summary>
		[SugarColumn(ColumnName = "inteltype")]
		public string Inteltype { get; set; }
		/// <summary>
		/// Desc: 备注
		/// </summary>
		[SugarColumn(ColumnName = "remark")]
		public string Remark { get; set; }
		/// <summary>
		/// Desc: 用户id
		/// </summary>
        [SugarColumn(ColumnName = "userid")]
		public long? Userid { get; set; } = 0;

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
