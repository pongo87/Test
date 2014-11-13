using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WuLiu
{
    public static class WuLiuDAL
    {
        /// <summary>
        /// 页容量
        /// </summary>
        public static decimal pageContent = 15;

        public static string GetAllWuLiuByPage(int pageNum)
        {
            List<T_WuLiu> list = new List<T_WuLiu>();
            DataTable table = new DataTable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            table = SqlHelper.ExecuteDataTable(@"select top (@pageContent) tw.*,te.EndPoint,ts.StartPoint from T_WuLiu tw join T_ERoad te on tw.ERoadId=te.Id join T_SRoad ts on tw.SRoadId=ts.Id
                                               where tw.Id not in (select top (@pageNum) Id from T_WuLiu order by id desc) order by id desc",
                        new SqlParameter("@pageContent", (int)pageContent),
                        new SqlParameter("@pageNum", (int)(pageContent * (pageNum - 1))));

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    list.Add(new T_WuLiu
                    {
                        Id = Int32.Parse(row["Id"].ToString()),
                        Sender = row["Sender"].ToString(),
                        Receiver = row["Receiver"].ToString(),
                        SRoadId = Int32.Parse(row["SRoadId"].ToString()),
                        ERoadId = Int32.Parse(row["ERoadId"].ToString()),
                        Things = row["Things"].ToString(),
                        Freight = decimal.Parse(row["Freight"].ToString()),
                        EndPoint = row["EndPoint"].ToString(),
                        StartPoint = row["StartPoint"].ToString()
                    });
                }
            }
            return jss.Serialize(list);
        }

        public static string ReturnPageNum()
        {
            var allcount = SqlHelper.ExecuteScalar("select count(*) from T_Wuliu");
            var pageNum = Math.Ceiling((decimal)((int)allcount / pageContent));
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(pageNum);
        }

        public static string GetSRoad()
        {
            List<T_SRoad> list = new List<T_SRoad>();
            DataTable table = new DataTable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            table = SqlHelper.ExecuteDataTable("select * from T_SRoad");
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    list.Add(new T_SRoad
                    {
                        Id = Int32.Parse(row["Id"].ToString()),
                        StartPoint = row["StartPoint"].ToString(),
                    });
                }
            }
            return jss.Serialize(list);
        }

        public static string GetERoad()
        {
            List<T_ERoad> list = new List<T_ERoad>();
            DataTable table = new DataTable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            table = SqlHelper.ExecuteDataTable("select * from T_ERoad");
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    list.Add(new T_ERoad
                    {
                        Id = Int32.Parse(row["Id"].ToString()),
                        EndPoint = row["EndPoint"].ToString(),
                    });
                }
            }
            return jss.Serialize(list);
        }

        public static void InsertWuliu(string sender, string receiver, string things, int sroadid, int eroadid, decimal freight)
        {
            SqlHelper.ExecuteNonQuery(@"insert into T_WuLiu(Sender,Receiver,SRoadId,ERoadId,Things,Freight) 
                                                    values(@sender,@receiver,@sroadid,@eroadid,@things,@freight)",
                                                    new SqlParameter("@sender", sender),
                                                    new SqlParameter("@receiver", receiver),
                                                    new SqlParameter("@sroadid", sroadid),
                                                    new SqlParameter("@eroadid", eroadid),
                                                    new SqlParameter("@things", things),
                                                    new SqlParameter("@freight", freight));
        }

        public static void InsertRoad(bool flag, string roadname)
        {
            if (flag)
            {
                SqlHelper.ExecuteNonQuery(@"insert into T_SRoad(StartPoint) values(@spoint)", new SqlParameter("@spoint", roadname));
            }
            else
            {
                SqlHelper.ExecuteNonQuery(@"insert into T_ERoad(EndPoint) values(@epoint)", new SqlParameter("@epoint", roadname));
            }
        }

        public static void DelWuLiuById(int id)
        {
            SqlHelper.ExecuteNonQuery("delete from T_WuLiu where Id=@id", new SqlParameter("@id", id));
        }

        public static string GetWuliuById(int id)
        {
            List<T_WuLiu> list = new List<T_WuLiu>();
            DataTable table = new DataTable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            table = SqlHelper.ExecuteDataTable(@"select tw.*,te.EndPoint,ts.StartPoint from T_WuLiu tw join T_ERoad te on tw.ERoadId=te.Id join T_SRoad ts on tw.SRoadId=ts.Id where tw.Id=@id ",
                        new SqlParameter("@id", id));
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    list.Add(new T_WuLiu
                    {
                        Id = Int32.Parse(row["Id"].ToString()),
                        Sender = row["Sender"].ToString(),
                        Receiver = row["Receiver"].ToString(),
                        SRoadId = Int32.Parse(row["SRoadId"].ToString()),
                        ERoadId = Int32.Parse(row["ERoadId"].ToString()),
                        Things = row["Things"].ToString(),
                        Freight = decimal.Parse(row["Freight"].ToString()),
                        EndPoint = row["EndPoint"].ToString(),
                        StartPoint = row["StartPoint"].ToString()
                    });
                }
            }
            return jss.Serialize(list);
        }
    }
}