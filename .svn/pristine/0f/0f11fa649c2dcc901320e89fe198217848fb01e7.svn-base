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
    public abstract class Pager : Ajax
    {
        protected string Info = "{0}/{3}页 共{2} 每页{1}行";
        protected string First = "<a href='javascript:Pager.get(\\\"first\\\",\\\"{0}\\\")'> 首页 </a>";
        protected string Prev = "<a href='javascript:Pager.get(\\\"prev\\\",\\\"{0}\\\")'> 上一页 </a>";
        protected string Index = "<a href='javascript:Pager.get({0},\\\"{1}\\\");void(0);'> [{0}] </a>";
        protected string Next = "<a href='javascript:Pager.get(\\\"next\\\",\\\"{0}\\\")'> 下一页 </a>";
        protected string Last = "<a href=javascript:Pager.get(\\\"last\\\",\\\"{0}\\\") > 末页 </a>";
        protected string TextFiled = "<input type='text' id='Pager_{0}Text' size='3' /><input type='button' value='OK' onclick=Pager.get($('#Pager_{0}Text').val(),\\\"{0}\\\") />";
        protected int IndexCount = 5;
        protected int MaxCount = 0;
        protected int PageSize = 0;
        protected int PageIndex { get { return Convert.ToInt32(Request["PageIndex"] ?? "1"); } }
        protected int Begin { get {return  (PageIndex-1)*PageSize+1;} }
        protected int End { get { return Begin + PageSize - 1; } }
        public override void Default()
        {
            if (string.IsNullOrEmpty(Request["into"]))
            {
                Response.Write(string.Format(@"
					String.prototype.format = function() {{ var args = arguments; return this.replace(/{{([0-9]*)}}/g, function(e, v) {{ return args[e[1] || v]; }}); }}
					var Pager =
						{{
							IndexCount: 5,
							Info: ""{0}"",
							First: ""{1}"",
							Prev: ""{2}"",
							Index: ""{3}"",
							Next: ""{4}"",
							Last: ""{5}"",
							TextFiled: ""{6}"",
							printIndex: function(index, into, PageSize, MaxCount) {{
								$('#Pager_' + into).empty();
								if (MaxCount > 0) {{
									var MaxPage = Math.ceil(MaxCount / PageSize);
									$('#Pager_' + into).data('PageIndex',index);
									$('#Pager_' + into).data('Last',MaxPage);
									$('#Pager_' + into).append(Pager.Info.format(index, PageSize, MaxCount, MaxPage, into));
									$('#Pager_' + into).append(Pager.First.format(into));
									$('#Pager_' + into).append(Pager.Prev.format(into));
									$('#Pager_' + into).append(""<span id='Pager_{{0}}Index'></span>"".format(into));
									var ii = index - Math.floor(Pager.IndexCount / 2);
									if (ii + Pager.IndexCount > MaxPage) ii = MaxPage - Pager.IndexCount + 1;
									if (ii < 1) ii = 1;
									for (var i = ii; i < ii + Pager.IndexCount && i <= MaxPage; i++) {{
										$('#Pager_{{0}}Index'.format(into)).append(Pager.Index.format(i, into));
									}}
									$('#Pager_' + into).append(Pager.Next.format(into));
									$('#Pager_' + into).append(Pager.Last.format(into));
									$('#Pager_' + into).append(Pager.TextFiled.format(into));
								}}
							}},
							param: function(into) {{ return ''; }},
							after: false,
							get: function(index, into) {{
		        		        if (isNaN(index)) {{ 
                                    switch (index) {{ 
										case 'first': index = 1; break; 
										case 'prev': index = $('#Pager_' + into).data('PageIndex') - 1; break; 
										case 'next': index = $('#Pager_' + into).data('PageIndex') + 1; break; 
										case 'last': index = $('#Pager_' + into).data('Last'); break; 
										default: return false;
									}} 
								}}
								if (index <= 0) return false;
								if (index > ($('#Pager_' + into).data('Last')||1)) return false;
								index = parseInt(index,10);
								$.get('{7}', {{ param: Pager.param(into), fun: 'Page_'+into, PageIndex: index }}, function(data) {{
									$('#' + into).html(data.Data);
									Pager.printIndex(index, into, data.PageSize, data.MaxCount);
									$('#Pager_{{0}}Text'.format(into)).val(index);
									$('#Pager_{{0}}Index>*:contains({{1}})'.format(into, index)).addClass('Pager_Select');
									if (Pager.after) Pager.after(index, data);
								}}, 'json');
								return false;
							}}
						}}
            ", Info, First, Prev, Index, Next, Last, TextFiled, Request.Url.AbsolutePath));
            }
            else
            {
                PringDOM("<div id='Pager_{0}'></div>", Request["into"]);
                if (Request["auto"] == "true")
                    Response.Write("Pager.get(1,'" + Request["into"] + "');\r\n");
            }
            //PringDOM(Info);
            //PringDOM(First);
            //PringDOM(Prev);
            ////string s = string.Format(Index, 1, "class='Pager_Select'");
            ////for (int i = 2; i <= IndexCount; i++)
            ////{
            ////    s += string.Format(Index, i);
            ////}
            //PringDOM("<span id='Pager_Index'></span>");
            //PringDOM(Next);
            //PringDOM(Last);
            //PringDOM(TextFiled);
        }
        private void PringDOM(string message, params object[] orgs)
        {
            Response.Write("document.write(\"" + string.Format(message, orgs) + "\");\r\n");
        }
        protected void PrinData(object data)
        {
            //data = data.ToString().Replace("\"", "\\\"");
            Response.Write(string.Format(@"{{""Data"":""{0}"",""MaxCount"":""{1}"",""PageSize"":""{2}""}}", data, MaxCount, PageSize));
        }
    }
}
