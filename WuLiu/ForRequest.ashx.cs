using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WuLiu
{
    /// <summary>
    /// ForIndex 的摘要说明
    /// </summary>
    public class ForRequest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Request["action"] == "addsroad")
            {
                context.Response.Write(WuLiuDAL.GetSRoad());
            }
            if (context.Request["action"] == "adderoad")
            {
                context.Response.Write(WuLiuDAL.GetERoad());
            }
            if (context.Request["action"] == "addwuliu")
            {
                var sd = context.Request["sender"];
                var receiver = context.Request["receiver"];
                var sroadid = Int32.Parse(context.Request["sroad"]);
                var eroadid = Int32.Parse(context.Request["eroad"]);
                var things = context.Request["things"];
                var freight = Decimal.Parse(context.Request["freight"]);
                WuLiuDAL.InsertWuliu(sd, receiver, things, sroadid, eroadid, freight);
                context.Response.Redirect("index.aspx");
            }
            if (context.Request["action"] == "addroad")
            {
                var roadchoose = context.Request["roadchoose"];
                var roadname = context.Request["roadname"];
                if (roadchoose == "sroad")
                {
                    WuLiuDAL.InsertRoad(true, roadname);
                }
                if (roadchoose == "eroad")
                {
                    WuLiuDAL.InsertRoad(false, roadname);
                }
                context.Response.Redirect("Index.aspx");
            }
            //分页
            if (context.Request["action"] == "getpagenum")
            {
                context.Response.Write(WuLiuDAL.ReturnPageNum());
            }
            if (context.Request["action"] == "pagego")
            {
                int pageNum = Int32.Parse(context.Request["pagenum"]);
                context.Response.Write(WuLiuDAL.GetAllWuLiuByPage(pageNum));
            }
            //查询
            if (context.Request["action"] == "searchbyid")
            {
                if (!String.IsNullOrWhiteSpace(context.Request["wlid"]))
                {
                    int id = 0;
                    if (Int32.TryParse(context.Request["wlid"], out id))
                    {
                        context.Response.Write(WuLiuDAL.GetWuliuById(id));
                    }
                }
            }
            //删除
            if (context.Request["action"] == "delbyid")
            {
                int id = Int32.Parse(context.Request["id"]);
                WuLiuDAL.DelWuLiuById(id);
                context.Response.Redirect("Index.aspx");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}