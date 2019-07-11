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
    public abstract class Form : Ajax
    {
        public override void Default()
        {
            Response.ContentType = "application/x-javascript";
            Response.Write(@"
var Form = {
	success: function(data, fun, form) { form.reset(); alert(data.value ? '执行成功!' : '执行失败'); },
	before: function(fun, form) { return true; },
	ajax: function(data, type, fun, form) {
		if (!Form.before()) return;
		$.ajax({
			url: '" + Request.Url.AbsolutePath + @"',
			data: data,
			type: type,
			success: function(data) { Form.success(data, fun, form); },
			dataType: 'json'
		});
	},
	get: function(fun) {
		var form = $(window.event == null ? arguments.callee.caller.arguments[0].target : window.event.srcElement).parents('form:first');
		var data = form.serializeArray();
		data.push({ name: 'fun', value: fun });
		Form.ajax(data, 'GET', fun, form[0]);
	},
	post: function(fun) {
		var form = $(window.event == null ? arguments.callee.caller.arguments[0].target : window.event.srcElement).parents('form:first');
		var data = form.serializeArray();
		data.push({ name: 'fun', value: fun });
		Form.ajax(data, 'POST', fun, form[0]);
	}
}
            ");
        }
    }
}
