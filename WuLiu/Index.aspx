<%@ Page Title="Table Info" Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WuLiu.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/Bootstrap/css/bootstrap.min.css" />
    <title></title>
    <style type="text/css">
        #addnewuliu td, tr
        {
            width: 100px;
        }
    </style>
</head>
<body>
    <div class="container">
        <!-- Modal -->
        <div class="modal fade" id="wuliutable" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <form action="forrequest.ashx?action=addwuliu" method="post">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title" id="myModalLabel">新增运单</h4>
                        </div>
                        <div class="modal-body">
                            <table class='table table-bordered' id="addnewuliu">
                                <thead>
                                    <tr>
                                        <th>收件人</th>
                                        <th>发货人</th>
                                        <th>起点</th>
                                        <th>终点</th>
                                        <th>货物</th>
                                        <th>运费(￥)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <input class="form-control" type="text" name="sender" required="" />
                                        </td>
                                        <td>
                                            <input class="form-control" type="text" name="receiver" required="" /></td>
                                        <td id="sroad"></td>
                                        <td id="eroad"></td>
                                        <td>
                                            <input class="form-control" type="text" name="things" required="" /></td>
                                        <td>
                                            <input class="form-control" name="freight" required="required" type="number" min="0" step="0.01" placeholder="0.00" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
                            <button type="submit" class="btn btn-primary">保存</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <div class="modal fade" id="roadtable" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true">
            <div class="modal-dialog">
                <form action="forrequest.ashx?action=addroad" method="post">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title" id="myModalLabel1">新增路线</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table-bordered table">
                                <thead>
                                    <tr>
                                        <th>名称</th>
                                        <th>类型</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <input class="form-control" type="text" name="roadname" required="required" />
                                        </td>
                                        <td>
                                            <select class="form-control" name="roadchoose" required="required">
                                                <option value="sroad">起点</option>
                                                <option value="eroad">终点</option>
                                            </select>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
                            <button type="submit" class="btn btn-primary">保存</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>

        <!--tools-->
        <div style="margin: 10px 0; height: 35px;">
            <div class="pull-left">
                <input id="wuliucodeid" type="text" class="form-control" style="width: 100px; float: left; margin: 0 5px;" placeholder="运单编号" required="required" />
                <button class="btn btn-default btn-search" type="button" onclick="searchbyid()"><span class="glyphicon glyphicon-search"></span>查询</button>
            </div>
            <div class="pull-right">
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#wuliutable">新增运单</button>
                <span></span>
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#roadtable">新增路线</button>
            </div>
        </div>

        <!--table-->
        <div class="panel-default panel" style="margin-top: 15px;">
            <div class="panel-heading" style="height: 50px;">运单表</div>
            <div class="panel-body" id="pbody">
            </div>
            <div class="panel-footer" id="footpage">
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/Bootstrap/js/jquery-2.2.1.min.js"></script>
    <script type="text/javascript" src="/Bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $.post("ForRequest.ashx", { action: "pagego", pagenum: 1 }, function (txt) {
            pagehelp(txt);
        });

        //路线
        $.post("ForRequest.ashx", { action: "addsroad" }, function (txt) {
            var list = JSON.parse(txt);
            var s = "<select name='sroad' class='form-control'>";
            for (var i = 0; i < list.length; i++) {
                var temp = list[i];
                s += "<option value='" + temp.Id + "'>" + temp.StartPoint + "</option>"
            }
            s += "</select>";
            $("#sroad").append(s);
        });

        $.post("ForRequest.ashx", { action: "adderoad" }, function (txt) {
            var list = JSON.parse(txt);
            var s = "<select name='eroad' class='form-control'>";
            for (var i = 0; i < list.length; i++) {
                var temp = list[i];
                s += "<option value='" + temp.Id + "'>" + temp.EndPoint + "</option>"
            }
            s += "</select>";
            $("#eroad").append(s);
        });


        //分页
        $.post("ForRequest.ashx", { action: "getpagenum" }, function (m) {
            var s = "<ul class='pagination'><li class='disabled' onclick='dispagefoot(this)'><a href='#' onclick=pagego(1)>1</a></li>";
            for (var i = 2; i <= m; i++) {
                s += "<li onclick='dispagefoot(this)'><a href='#' onclick=pagego('" + i + "') >" + i + "</a></li>";
            }
            s += "</ul>";
            $("#footpage").append(s);
        });

        function dispagefoot(obj) {
            $(obj).addClass("disabled");
        }

        function pagego(num) {
            $(".pagination li").removeClass("disabled");
            $(this).parent().addClass("disabled");
            $.post("forrequest.ashx", { action: "pagego", pagenum: num }, function (txt) {
                pagehelp(txt);
            });
        }

        function pagehelp(txt) {
            var list = JSON.parse(txt);
            var sum = 0;
            var s = "<table class='table table-bordered'><thead><tr><th>编号</th><th>收件人</th><th>发货人</th><th>起点</th><th>终点</th><th>货物</th><th>运费(￥)</th><th>操作</th></tr></thead><tbody>";
            for (var i = 0; i < list.length; i++) {
                var temp = list[i];
                sum += temp.Freight;
                s += "<tr><td>" + temp.Id + "</td><td>" + temp.Sender + "</td><td>" + temp.Receiver + "</td><td>" + temp.StartPoint + "</td><td>" + temp.EndPoint + "</td><td>" + temp.Things + "</td><td>" + temp.Freight + "</td><td><a href=# onclick=delbyid('" + temp.Id + "')>删除</a></td></tr>"
            }
            s += "<tr><td class='text-center' colspan='6'>总计:</td><td>" + sum.toFixed(2) + "</td><td></td></tr></tbody></table>";  //小数点位数 (Math.round(sum*100)/100)
            $("#pbody").find("table").remove();
            $("#pbody").append(s);
        }

        function delbyid(id) {
            if (confirm('确定删除？')) {
                window.location.href = "forrequest.ashx?action=delbyid&id=" + id;
            }
        }

        function searchbyid() {
            $(".pagination li").removeClass("disabled");
            var id = $("#wuliucodeid").val();
            $.post("forrequest.ashx", { wlid: id, action: "searchbyid" }, function (txt) {
                pagehelp(txt);
            });
        }
    </script>
</body>
</html>
