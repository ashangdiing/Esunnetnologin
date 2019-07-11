
using Esunnet.Record.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esunnet.Record.Action
{
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    public class UserAction : Esunnet.Record.Biz.AjaxBiz<IUser, Model.Table.User>, System.Web.SessionState.IRequiresSessionState
    {
        public override void Init()
        {
            fun.Add("login", Login);
            fun.Add("UpdatePassword", UpdatePassword);
        }
        private void Login()
        {
            if (!string.IsNullOrEmpty(Parameter.t.userId) && !string.IsNullOrEmpty(Parameter.t.userPassword))
            {
                Model.Table.User u = Parameter.t;
                u.lastLoginIp = Request.UserHostAddress;
                u.userPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Parameter.t.userPassword, "MD5");
                u = Biz.Login(u);
                if (u.State > 0)
                {
                    Session["User"] = u;
                }
                /*get
                Response.Write(us.State);
                switch (us.State)
                {
                    //case -2: EsunLog.Log("登陆", "用户不存在！", "信息", ip); break;
                   // case -1: EsunLog.Log("登陆", "密码错误！", "信息", ip); break;
                    //case 0: $("#error").html("请输入帐号和密码！"); break;
                    // default: new EsunLog("登陆", "登陆成功！", "信息", ip); break;
                }
                if (us.State > 0)
                {
                    Session["User"] = us;
                  //  EsunLog.Log("登陆", "登陆成功！", "信息", ip);
                }*/
                Response.Write(u.State);
                return;
            }
            Response.Write(0);
        }
        private void UpdatePassword()
        {
            Model.Table.User us = Session["User"] as Model.Table.User;
            string oldPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Request["oldPassword"], "MD5");
            string newPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Request["newPassword"], "MD5");
            if (us == null)
            {
                Response.Write(-1);
                return;
            }
            if (!us.userPassword.Equals(oldPassword))
            {
                Response.Write(-2);
                return;
            }
            us.userPassword = newPassword;
            bool UPState = Biz.UpdatePassword(us);
            if (!UPState)
            {
                us.userPassword = oldPassword;
                Session["User"] = us;
                Response.Write(-3);
            }
            else Response.Write(1);
        }
    }
}