<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Esunnet.Record.Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>用户登录</title>
    <script type="text/javascript" src="js/jquery-1.8.2.min.js"></script>
	<script type="text/javascript">
	    $(function () {
	        $("#error").ajaxError(function (event, request, settings) {
	            $(this).html("登陆失败请联系管理员!");
	        });
	    });
	    function doSubimt() {
	        if ($("#txt_UserId").val() == "" || $("#txt_Password").val() == "") {
	            $("#error").html("请输入帐号和密码！"); return false;
	        }
	        $.get("Action/UserAction.ashx", { fun: "login", isTouch: !!("createTouch" in document), userId: $("#txt_UserId").val(), userPassword: $("#txt_Password").val(), t: new Date().getTime() }, function (data) {
	            switch (data) {
	                case -2: $("#error").html("用户不存在！"); break;
	                case -1: $("#error").html("密码错误！"); break;
	                case 0: $("#error").html("请输入帐号和密码！"); break;
	                default: window.location.replace('Default.aspx?' + new Date().getTime()); break;
	            }
	        }, "json");
	        return false;
	    }
	</script>
	<style type="text/css">
		body,html
		{
			overflow:hidden;
			height:100%;
            width: 100%;
		}
		.outer 
		{
			top:0px;
			left:0px;
			bottom:0px;
			right:0px;
			overflow:hidden;
			position:absolute;
			height:100%;
		}
		.middle 
		{
			width:509px;
			height:352px;
			left:50%;
			top:50%;
			position:relative;
		}
		.inner {
			width:509px;
			height:352px;
			top: -50%;
			left:-50%;
			position:relative;
			background-image:url(imgs/Login/login.jpg);
			text-align:center;
			padding:180px 0px 0px 0px;
			background-repeat:no-repeat;
			font-size:14px;
		}
		#txt_UserId
		{
			width:140px;
			margin:2px;
			vertical-align:middle;
		}
		#txt_Password
		{
			width:140px;
			margin:2px;
			vertical-align:middle;
		}
		.button
		{
			background-image:url(imgs/Login/btn.jpg);
			width:70px;
			height:20px;
			border:0px;
			margin:2px;
			padding:0px;
		}
		#error
		{
			color:Red;
			padding:8px;
		}
	</style>
</head>
<body>
	<div class="outer">
		<div class="middle">
			<div class="inner">
				<form action="Login.aspx" onsubmit="return doSubimt()">
					帐号:<input type="text" name="txt_UserId" id="txt_UserId" /><br />
					密码:<input type="password" name="txt_Password" id="txt_Password" /><br />
					<input type="submit" value="登录" class="button" />
					<div id="error"></div>
				</form>
			</div>
		</div>
	</div>
</body>
</html>