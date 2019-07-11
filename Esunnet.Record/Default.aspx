<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Esunnet.Record.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<title>EUC</title>
<link rel="stylesheet" href="js/dhtmlx/dhtmlx.css" type="text/css" />
<link rel="stylesheet" href="js/dhtmlx/status_toolbar_layout.css" type="text/css" />
<script type="text/javascript" src="Action/Config.ashx"></script>
<script type="text/javascript">
    if (Config.user == null) {
       // window.location.replace('Login.aspx?' + new Date().getTime());
    }
</script>
<script type="text/javascript" src="js/dhtmlx/dhtmlx.js"></script>
<script type="text/javascript" src="js/dhtmlx/ext/dhtmlxgrid_pgn.js"></script>
<script type="text/javascript" src="js/dhtmlx/ext/dhtmlxgrid_json.js"></script>
<script type="text/javascript" src="js/jquery-1.8.2.min.js"></script>                      
<script type="text/javascript" src="js/dhtmlx/expand.js"></script>
    <script src="js/page/system/User.js"></script>
<style type="text/css">
html,body {
	width: 100%;
	height: 100%;
	margin: 0px;
	padding: 0px;
	overflow: hidden;
	font-size: 13px !important;
}
</style>
<script type="text/javascript">
    var dhxMenu, dhxTabbar, dhxWins, dhxStatusBar;
    dhtmlx.image_path = "js/dhtmlx/imgs/";
    //dhtmlx.skin = "dhx_skyblue";
    $(function () {
        dhxWins = new dhtmlXWindows();
        var dhxLayout = new dhtmlXLayoutObject(document.body, "1C");
        dhxLayout.setSkin("dhx_skyblue");
        dhxMenu = dhxLayout.attachMenu();
        dhxMenu.setSkin("dhx_skyblue");
        dhxMenu.loadJson("Action/MenuAction.ashx", { t: new Date().getTime() });
        dhxStatusBar = dhxLayout.attachStatusBar();
        //dhxStatusBar.setText("用户：" + Config.user.userId);
        dhxStatusBar.setText("用户：admin" );
        dhxTabbar = dhxLayout.items[0].attachTabbar();
        dhxMenu.attachEvent("onClick", function (id) {
            var data = dhxMenu.getUserData(id, "data");
            if (data && data.param == "window") {
                ActionPage.add(data.url, data.fun, dhxTabbar.cells("a" + id), data.param);
                return;
            }
            if (id == 88) {
                location.replace("Login.aspx");
            } else if (id == 2) {
                UpdatePassWord();
            }
            else if (!dhxTabbar.cells("a" + id)) {
                dhxTabbar.addTab("a" + id, dhxMenu.getItemText(id), "100px");
                ActionPage.add(data.url, data.fun, dhxTabbar.cells("a" + id), data.param);
            }
            dhxTabbar.setTabActive("a" + id);
        });
        var openDefault = function () {
            dhxTabbar.addTab("a11","录音查询", "100px");
            ActionPage.add("js/page/record/SelectRecord.js", "SelectRecord", dhxTabbar.cells("a11"));
            dhxTabbar.setTabActive("a11");
        }
        openDefault();
    });
</script>
</head>
<body>
</body>
</html>
