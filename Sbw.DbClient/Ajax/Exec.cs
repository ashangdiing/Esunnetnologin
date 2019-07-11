using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sbw.Ajax
{
    public abstract class Exec : Ajax
    {
        public override void Default()
        {
            Response.ContentType = "application/x-javascript";
            Response.Write(@"
var Exec = {
	success: function(data, fun, form) { form.reset(); alert(data.value ? '执行成功!' : '执行失败'); },
	before: function(fun, form) { return true; },
	ajax: function(data, type, fun, form) {
		if (!Exec.before()) return;
		$.ajax({
			url: '" + Request.Url.AbsolutePath + @"',
			data: data,
			type: type,
			success: function(data) { Exec.success(data, fun, form); },
			dataType: 'json'
		});
	},
	get: function(fun,param) {
		var form = $(window.event == null ? arguments.callee.caller.arguments[0].target : window.event.srcElement).parents('form:first');
		var data = param || {};
		data.fun = fun;
		Exec.ajax(data, 'GET', fun, form[0]);
	},
	post: function(fun,param) {
		var form = $(window.event == null ? arguments.callee.caller.arguments[0].target : window.event.srcElement).parents('form:first');
		var data = param || {};
		data.fun = fun;
		Exec.ajax(data, 'POST', fun, form[0]);
	}
}
            ");
        }
    }
}