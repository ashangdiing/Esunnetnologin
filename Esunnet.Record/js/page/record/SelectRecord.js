ActionPage["SelectRecord"] = function (mainbody, param) {
    var dhxLayout = mainbody.attachLayout("2E");
    var bar = dhxLayout.items[0].attachToolbar();
    var grid = dhxLayout.items[1].attachGrid();
    dhxLayout.setSkin("dhx_skyblue");
    dhxLayout.items[0].setHeight(130);
    dhxLayout.items[0].hideHeader();
    dhxLayout.items[0].fixSize(true, true);
    dhxLayout.items[1].hideHeader();
    var formdata = [
			{ type: "settings", position: "label-left", labelWidth: 55, inputWidth: 80 },
			{
			    type: "label",
			    labelWidth: "auto",
			    inputWidth: "auto",
			    position: "label-left",
			    list: [
						{ type: "calendar", dateFormat: "%Y-%m-%d %H:%i:%s", weekStart: 7, enableTime: true, readonly: true, name: "reclis_starttime", label: "开始时间:", inputWidth: '125' },
						{ type: "newcolumn" },
						{ type: "calendar", dateFormat: "%Y-%m-%d %H:%i:%s", weekStart: 7, enableTime: true, readonly: true, name: "reclis_endtime", label: "结束时间:", inputWidth: '125' },
						{ type: "newcolumn" },
						{ type: "input", name: "reclis_agentid", label: "坐席号码:", value: "" },
						{ type: "newcolumn" },
						{ type: "input", name: "reclis_ext", label: "分机号码:", value: "" },
						{ type: "newcolumn" },
						{ type: "input", name: "reclis_calling", label: "呼 叫 方:", value: "" },
						{ type: "newcolumn" },
						{ type: "input", name: "reclis_called", label: "被 叫 方:", value: "" },
						{ type: "newcolumn" },
						{
						    type: "fieldset",
						    labelWidth: "auto",
						    label: "呼叫类型",
						    inputWidth: "auto",
						    list: [{ type: "settings", position: "label-right" }, { type: "radio", name: "reclis_calltype", value: "1", label: "呼入" }, { type: "newcolumn" },
									{ type: "radio", name: "reclis_calltype", value: "2", label: "呼出" }, { type: "newcolumn" },
									{ type: "radio", name: "reclis_calltype", value: "3", label: "全部", checked: true }]
						},
						{ type: "newcolumn" },
						{
						    type: "fieldset",
						    labelWidth: "auto",
						    label: "时间",
						    inputWidth: "auto",
						    list: [{ type: "settings", position: "label-right" }, { type: "radio", name: "reclis_time", value: "day", label: "当天" }, { type: "newcolumn" },
									{ type: "radio", name: "reclis_time", value: "week", label: "当周" }, { type: "newcolumn" }, { type: "radio", name: "reclis_time", value: "month", label: "当月" },
									{ type: "newcolumn" }, { type: "radio", name: "reclis_time", value: "all", label: "全部", checked: true }]
						}]
			}];
    var w1form = dhxLayout.items[0].attachForm(formdata);
    w1form.attachEvent("onChange", function (name, value, is_checked) {
        if (name == "reclis_time") {
            var b = new Date();
            var e = new Date();
            switch (value) {
                case "day":
                    b.setHours(0, 0, 0, 0);
                    e.setHours(23, 59, 59, 0);
                    w1form.setValues({ reclis_starttime: b, reclis_endtime: e });
                    break;
                case "week":
                    var w = b.getDay();
                    var d = b.getDate();
                    b.setHours(0, 0, 0, 0);
                    b.setDate(d - w + 1);
                    e.setDate(d - w + 7);
                    e.setHours(23, 59, 59, 0);
                    w1form.setValues({ reclis_starttime: b, reclis_endtime: e });
                    break;
                case "month":
                    b.setHours(0, 0, 0, 0);
                    b.setDate(1);
                    var m = e.getMonth();
                    e.setHours(23, 59, 59, 0);
                    e.setMonth(m + 1, 0);
                    w1form.setValues({ reclis_starttime: b, reclis_endtime: e });
                    break;
                case "all":
                    w1form
                    w1form.setValues({ reclis_starttime: new Date(2000, 0, 1), reclis_endtime: new Date(2100, 0, 1) });
                    break;
                default:
                    break;
            }
        }
    });
    bar.setSkin("dhx_skyblue");
    bar.setIconsPath("imgs/button/");
    //bar.addButton("play", 0, "播放", "record.gif");
    bar.addButton("download", 1, "下载", "movedown.gif");
    bar.addButton("reset", 3, "刷新", "refresh.gif");
    bar.addGridState("selectAll", 4, grid);
    //bar.addButtonSelect("d2", 6, '服务器', [["wh", "obj", "武汉"], ["sh", "obj", "上海"]]);
    bar.addButton("select", 9, "查询", "search.gif");
    bar.attachEvent("onClick", function (id) {
        var v = grid.getCheckValue();
        switch (id) {
            case "play":
                var sid = grid.getSelectedId();
                if (sid == null) {
                    alert("请勾选要收听的录音!");
                } else {
                    if (dhxWins.window("win") == null) {
                        var p = locPort(400, 90);
                        var w1 = dhxWins.createWindow("win", p.w, p.h, 400, 90);
                        w1.attachURL("play.jsp?id=" + sid + "&flag=play");
                        w1.setText("<span style='font-size:14px'>播放录音</span>");
                    }
                }
                return;
            case "download":
                if (!document.applets[0]) {
                    //$("body").append('<applet alt="下载控件" code="DownLoad.class" archive="../cfg/mytest.jar" width="0px" height="0px;">');
                }
                if (v.length == 0) {
                    alert("请勾选要下载的录音!");
                } else {
                    $.get("../playextrecord?flag=down&id=" + v.join("','"), function (data) {
                        try {
                            if (!document.applets[0].openWindow(data)) {
                                alert("参数错误!");
                            }
                        } catch (e) {
                            alert("没有安装java请先安装java!");
                        }
                    }, "json");
                }
                return;
            case "delete":
                if (v.length == 0) {
                    alert("请先勾选要删除的录音记录!");
                    return;
                }
                if (confirm("确定要删除勾选的录音记录吗?")) {
                    $.post('../deleterecordservlet', "flag=" + flag + "&ext=" + userExt + "&ids=" + v.join(","), function (data) {
                        alert(data);
                    });
                }
                return;
            case "reset":
            case "select":
                grid.send("Action/RecordAction.ashx?fun=find", w1form);
                return;
        }
    });
    grid.enablePaging(true, 20, 6, dhxLayout.items[1].attachStatusBar());
    grid.setPagingSkin("toolbar", "dhx_skyblue");
   // grid.enableAutoWidth(true);
    grid.send("Action/RecordAction.ashx?fun=find", w1form);
}
