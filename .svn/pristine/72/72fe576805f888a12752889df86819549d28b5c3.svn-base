ActionPage["Report"] = function (mainbody, param) {
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
            { type: "calendar", dateFormat: "%Y-%m-%d", weekStart: 7, readonly: true, name: "datetime", label: "查询时间:", inputWidth: '125' },
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
                grid.send("Action/RepotAction.ashx?fun=report&proc=" + param, w1form);
                return;
        }
    });
    grid.send("Action/RepotAction.ashx?fun=report&proc=" + param, w1form);
}