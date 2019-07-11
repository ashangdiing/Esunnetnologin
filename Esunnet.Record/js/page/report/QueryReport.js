ActionPage["QueryReport"] = function (mainbody, param) {
    var dhxLayout = mainbody.attachLayout("2E");
    var bar = dhxLayout.items[0].attachToolbar();
    var grid = dhxLayout.items[1].attachGrid();
    dhxLayout.setSkin("dhx_skyblue");
    dhxLayout.items[0].setHeight(80);
    dhxLayout.items[0].hideHeader();
    dhxLayout.items[0].fixSize(true, true);
    dhxLayout.items[1].hideHeader();
    var formdata = [
			{ type: "settings", position: "label-left", labelWidth: 55, inputWidth: 80 },
            { type: "calendar", dateFormat: "%Y-%m-%d %H:%i:%s", enableTime: true, weekStart: 7, readonly: false, name: "begin", label: "起始时间:", inputWidth: '125' },
            { type: "newcolumn" },
            { type: "calendar", dateFormat: "%Y-%m-%d %H:%i:%s", enableTime: true, weekStart: 7, readonly: false, name: "end", label: "结束时间:", inputWidth: '125' },
            { type: "newcolumn" },
			{
			    type: "label",
			    labelWidth: "auto",
			    inputWidth: "auto",
			    position: "label-right",
			    list: [
                    { type: "label", labelWidth: "42", label: "类型：" },
                    { type: "newcolumn" },
                    { type: "radio", name: "type", value: "3", labelWidth: "15", position: "label-right", label: "年" },
                    { type: "newcolumn" },
                    { type: "radio", name: "type", value: "2", labelWidth: "15", position: "label-right", label: "月", checked: true },
                    { type: "newcolumn" },
                    { type: "radio", name: "type", value: "1", labelWidth: "15", position: "label-right", label: "日" }
			    ]
			}];
    var w1form = dhxLayout.items[0].attachForm(formdata);
    bar.setSkin("dhx_skyblue");
    bar.setIconsPath("imgs/button/");
    bar.addButton("select", 1, "查询", "search.gif");
    bar.attachEvent("onClick", function (id) {
        var v = grid.getCheckValue();
        switch (id) {
            case "select":
                grid.send("Action/RepotAction.ashx?fun=queryReport&proc=" + param, w1form);
                return;
        }
    });
    grid.send("Action/RepotAction.ashx?fun=queryReport&proc=" + param, w1form);
    //grid.enablePaging(true, 20, 6, dhxLayout.items[1].attachStatusBar());
    //grid.setPagingSkin("toolbar", "dhx_skyblue");
    // grid.enableAutoWidth(true);
    //grid.send("Action/RecordAction.ashx?fun=find", w1form);
}