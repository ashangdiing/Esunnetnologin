UpdatePassWord = function () {
    if (dhxWins.window("updatePassWord") == null) {

        var w1 = dhxWins.create("updatePassWord", 250, 160);
       // var dhxLayout = dhxWins.window("updatePassWord").attachLayout("1c");
       // dhxLayout.items[0].hideHeader();
        // myForm = dhxLayout.cells("a").attachForm();
        var fom = new Array();
        fom.push("<span style='font-size:14px;float:left;text-align: right;'><form id='update'>");
        fom.push("旧密码：<input id='oldPassWord' type='text' maxlength=16/></br>");
        fom.push("新密码:<input id='newPassWord' type='text' maxlength=16/></br>");
        fom.push("确认新密码:<input id='newPassWord1' type='text' maxlength=16/></br>");
        fom.push("<div id='error' style='font-size:10px;text-align: center;color:#F00;'></div>");
        fom.push("</form></span>");
        fom.push("<center><input name='update' type='button' value='确认' onclick='updatePassWordSubmit()'/></center>");
        // alert(fom.join(""));
        w1.attachHTMLString(fom.join(""));
      //  w1.attachHTMLString("<div>heheh</div>");
        w1.denyResize();
        w1.setText("<span style='font-size:14px'>修改密码</span>");
    }
}
function updatePassWordSubmit() {
   
    if ($("#newPassWord1").val().trim().length == 0 || $("#newPassWord").val().trim().length == 0 || $("#newPassWord1").val().trim().length == 0) {
        $("#error").html("长度不能为0或为空！");
        return;
    }
    if ($("#oldPassWord").val() == $("#newPassWord").val()) {
        $("#error").html("旧密码与新密码相同！");
        return;
    }
    if ($("#newPassWord1").val() != $("#newPassWord").val()) {
        $("#error").html("2次密码不相同！");
        return;
    }
   
    $.get("Action/UserAction.ashx", { fun: "UpdatePassword", isTouch: !!("createTouch" in document), oldPassWord: $("#oldPassWord").val(), newPassWord: $("#newPassWord").val(), t: new Date().getTime() }, function (data) {
        switch (data) {
            case -2: $("#error").html("旧密码不对！"); break;
            case -1: $("#error").html("您没登陆！"); break;
            case 0: $("#error").html("请输入帐号和密码！"); break;
            case 1: $("#error").html("密码修改成功！"); break;
            default: $("#error").html("密码修改失败！"); break;

        }
    }, "json");
}