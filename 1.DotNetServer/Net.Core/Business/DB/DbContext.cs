using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net.Core.Common.Helpers;
using Net.Core.Models.ConfModel;

namespace Net.Core.Business.DB
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext()
        {
            DbConfig dbConfig = ConfigHelper.GetConfig<DbConfig>("DbConfig");

            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = dbConfig.ConnectionString,
                DbType = dbConfig.DbType.ToLower() == "mysql" ? DbType.MySql : DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,

            });

            Db.Aop.OnLogExecuting = (sql, pars) => {};
        }

        public SqlSugarClient Db;
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }

        public dynamic Common(string tableName, string columnName, string columnValue = "", int type = 0, string sign = "option", int remindStart = 0, int remindEnd = 0, string mywhere = "")
        {
            sign = sign.ToLower();
            string sql = "";
            dynamic res = null;
            switch (sign)
            {
                case "option":
                    string where = " WHERE 1 = 1 ";
                    if (type == 1)
                    {
                        where = " WHERE level = " + type;
                    }

                    if (type > 1)
                    {
                        where = " WHERE level = " + type + " AND parent = " + columnValue;
                    }
                    sql = "SELECT " + columnName + " FROM " + tableName + where + mywhere;
                    res = Db.Ado.SqlQuery<string>(sql).ToList();
                    break;

                case "follow":
                    sql = "SELECT * FROM " + tableName +" WHERE " + columnName + " = '" + columnValue + "'";
                    res = Db.Ado.SqlQuerySingle<dynamic>(sql);
                    break;

                case "sh":
                    columnValue = columnValue == "是" ? "否" : "是";
                    sql = "UPDATE " + tableName + " SET Sfsh = '" + columnValue + "' WHERE Id = " + Convert.ToInt32(columnName);
                    res = Db.Ado.ExecuteCommand(sql);
                    break;

                case "remind":
                    res = 0;
                    if (type == 1)
                    {
                        if (remindStart > 0)
                        {
                            sql = "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnName + " >= " + remindStart + mywhere;
                        }

                        if (remindEnd > 0)
                        {
                            sql = "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnName + " <= " + remindEnd + mywhere;
                        }

                        if (remindStart > 0 && remindEnd > 0)
                        {
                            sql = "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnName + " >= " + remindStart + " AND " + columnName + " <= " + remindEnd + mywhere;
                        }

                        res = Db.Ado.SqlQuerySingle<int>(sql);
                    }

                    if (type == 2)
                    {
                        DateTime dt = DateTime.Now;
                        DateTime dtStart = dt.AddDays(remindStart);
                        DateTime dtEnd = dt.AddDays(remindEnd);

                        String dtStartStr = "";
                        String dtEndStr = "";
                        if(ConfigHelper.GetConfig<DbConfig>("DbConfig").DbType.ToLower() == "mysql")
                        {
                            dtStartStr = dtStart.ToString("yyyy-MM-dd hh:mm:ss");
                            dtEndStr = dtEnd.ToString("yyyy-MM-dd hh:mm:ss");
                        }
                        else
                        {
                            dtStartStr = dtStart.ToString("yyyy/MM/dd hh:mm:ss");
                            dtEndStr = dtEnd.ToString("yyyy/MM/dd hh:mm:ss");
                        }

                        if (remindStart >= 0 && remindEnd >= 0)
                        {
                            sql = "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnName + " BETWEEN '" + dtStartStr + "' AND '" + dtEndStr + "'" + mywhere;
                        }
                        else if (remindStart >= 0)
                        {
                            sql = "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnName + " >= '" + dtStartStr + "'" + mywhere;
                        }
                        else if (remindEnd >= 0)
                        {
                            sql = "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnName + " <= '" + dtEndStr + "'" + mywhere;
                        }
                        else if (remindStart < 0)
                        {
                            sql = "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnName + " >= '" + dtStartStr + "'" + mywhere;
                        }

                        res = Db.Ado.SqlQuerySingle<int>(sql);
                    }
                    break;

                case "cal":
                    sql = "SELECT SUM(" + columnName + ") AS sum, MAX(" + columnName + ") AS max, MIN(" + columnName + ") AS min, AVG(" + columnName + ") AS avg FROM " + tableName;
                    res = Db.Ado.SqlQuerySingle<dynamic>(sql);
                    break;

                case "group":
                    // sql = "SELECT COUNT(*) AS total, " + columnName + " FROM " + tableName + " GROUP BY " + columnName;
                    sql = "SELECT COUNT(*) AS total, " + columnName + " FROM " + tableName + mywhere + " GROUP BY " + columnName;
                    res = Db.Ado.SqlQuery<dynamic>(sql);
                    break;

                case "value":
                    // sql = "SELECT " + columnName + ", " + columnValue + " AS total FROM " + tableName + " GROUP BY " + columnName + ", " + columnValue;
                    sql = "SELECT " + columnName + ", SUM(" + columnValue + ") AS total FROM " + tableName + mywhere + " GROUP BY " + columnName;
                    res = Db.Ado.SqlQuery<dynamic>(sql);
                    break;
            }

            return res;
        }

        public dynamic StatDate(string tableName, string xColumnName, string yColumnName, string timeStatType, string where = "")
        {
            DbConfig dbConfig = ConfigHelper.GetConfig<DbConfig>("DbConfig");
            string sql = "";
            if (dbConfig.DbType.ToLower() == "mysql")
            {
                if (timeStatType == "日")
                    sql = "SELECT DATE_FORMAT(" + xColumnName + ", '%Y-%m-%d') " + xColumnName + ", sum(" + yColumnName + ") total FROM " + tableName + where + " GROUP BY DATE_FORMAT(" + xColumnName + ", '%Y-%m-%d')";
                if (timeStatType == "月")
                    sql = "SELECT DATE_FORMAT(" + xColumnName + ", '%Y-%m') " + xColumnName + ", sum(" + yColumnName + ") total FROM " + tableName + where + " GROUP BY DATE_FORMAT(" + xColumnName + ", '%Y-%m')";
                if (timeStatType == "年")
                    sql = "SELECT DATE_FORMAT(" + xColumnName + ", '%Y') " + xColumnName + ", sum(" + yColumnName + ") total FROM " + tableName + where + " GROUP BY DATE_FORMAT(" + xColumnName + ", '%Y')";
            }
            else
            {
                if (timeStatType == "日")
                    sql = "SELECT CONVERT(VARCHAR(10)," + xColumnName + ", 120) " + xColumnName + ", sum(" + yColumnName + ") total FROM " + tableName + where + " GROUP BY CONVERT(VARCHAR(10)," + xColumnName + ", 120)";
                if (timeStatType == "月")
                    sql = "SELECT CONVERT(VARCHAR(7)," + xColumnName + ", 120) " + xColumnName + ", sum(" + yColumnName + ") total FROM " + tableName + where + " GROUP BY CONVERT(VARCHAR(7)," + xColumnName + ", 120)";
                if (timeStatType == "年")
                    sql = "SELECT CONVERT(VARCHAR(4)," + xColumnName + ", 120) " + xColumnName + ", sum(" + yColumnName + ") total FROM " + tableName + where + " GROUP BY CONVERT(VARCHAR(4)," + xColumnName + ", 120)";
            }

            return Db.Ado.SqlQuery<dynamic>(sql);
        }
    }
}
