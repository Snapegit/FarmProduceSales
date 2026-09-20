using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Net.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 菜单
    /// </summary>
    [SugarTable("menu")]
	public class MenuDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }
		/// <summary>
		/// Desc: 菜单
		/// </summary>
		[SugarColumn(ColumnName = "menujson")]
		public string Menujson { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
