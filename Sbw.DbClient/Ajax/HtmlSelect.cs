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
    public abstract class HtmlSelect : Ajax
    {
        public override void Init()
        {
            fun.Add("Select", Select);
        }
        public override void Default()
        {
            Response.ContentType = "application/x-javascript";
            Response.Write("var HemlSelect = { after: false, params: function(el) { return el.value; }, get: function(el, into, fun) { $.get('" + Request.Url.AbsolutePath + "', { value: HemlSelect.params(el), fun: fun || 'Select' }, function(data) { $('#' + into).html(data); if (HemlSelect.after) HemlSelect.after(el,into,data,fun); }); } }");
        }
        public abstract void Select();
    }
}
