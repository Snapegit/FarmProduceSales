using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Net.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 商品订单
    /// </summary>
    [SugarTable("orders")]
	public class OrdersDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }
		/// <summary>
		/// Desc: 订单编号
		/// </summary>
        [SugarColumn(ColumnName = "orderid", IsOnlyIgnoreUpdate = true)]
		public string Orderid { get; set; }
		/// <summary>
		/// Desc: 商品表名
		/// </summary>
		[SugarColumn(ColumnName = "tablename")]
		public string Tablename { get; set; }
		/// <summary>
		/// Desc: 商品id
		/// </summary>
        [SugarColumn(ColumnName = "goodid")]
		public long? Goodid { get; set; } = 0;
		/// <summary>
		/// Desc: 商品名称
		/// </summary>
		[SugarColumn(ColumnName = "goodname")]
		public string Goodname { get; set; }
		/// <summary>
		/// Desc: 图片
		/// </summary>
		[SugarColumn(ColumnName = "picture")]
		public string Picture { get; set; }
		/// <summary>
		/// Desc: 购买数量
		/// </summary>
        [SugarColumn(ColumnName = "buynumber")]
		public int? Buynumber { get; set; } = 0;
		/// <summary>
		/// Desc: 单价
		/// </summary>
        [SugarColumn(ColumnName = "price")]
		public double? Price { get; set; }
		/// <summary>
		/// Desc: 折扣价
		/// </summary>
        [SugarColumn(ColumnName = "discountprice")]
		public double? Discountprice { get; set; }
		/// <summary>
		/// Desc: 总价
		/// </summary>
        [SugarColumn(ColumnName = "total")]
		public double? Total { get; set; }
		/// <summary>
		/// Desc: 折扣总价格
		/// </summary>
        [SugarColumn(ColumnName = "discounttotal")]
		public double? Discounttotal { get; set; }
		/// <summary>
		/// Desc: 支付类型
		/// </summary>
		[SugarColumn(ColumnName = "type")]
		public string Type { get; set; }
		/// <summary>
		/// Desc: 订单状态
		/// </summary>
		[SugarColumn(ColumnName = "status")]
		public string Status { get; set; }
		/// <summary>
		/// Desc: 地址
		/// </summary>
		[SugarColumn(ColumnName = "address")]
		public string Address { get; set; }
		/// <summary>
		/// Desc: 电话
		/// </summary>
		[SugarColumn(ColumnName = "tel")]
		public string Tel { get; set; }
		/// <summary>
		/// Desc: 收货人
		/// </summary>
		[SugarColumn(ColumnName = "consignee")]
		public string Consignee { get; set; }
		/// <summary>
		/// Desc: 备注
		/// </summary>
		[SugarColumn(ColumnName = "remark")]
		public string Remark { get; set; }
		/// <summary>
		/// Desc: 物流
		/// </summary>
		[SugarColumn(ColumnName = "logistics")]
		public string Logistics { get; set; }
		/// <summary>
		/// Desc: 用户id
		/// </summary>
        [SugarColumn(ColumnName = "userid")]
		public long? Userid { get; set; } = 0;
		/// <summary>
		/// Desc: 商户名称
		/// </summary>
		[SugarColumn(ColumnName = "shangjiazhanghao")]
		public string Shangjiazhanghao { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
