using Esunnet.Model.Frame.Dhtmlx;
using Esunnet.Record.Bll;
using Sbw.DbClinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esunnet.Record.Biz
{
    public abstract class AjaxBiz<T, M> : Sbw.Ajax.Ajax, IAjax<T, M>
        where T : IBll
        where M : IView, new()
    {
        public T Biz
        {
            get { return Esunnet.Tool.Setting.Biz.GetBean<T>(); }
        }
        public Model.Frame.IParameter<M> Parameter
        {
            get { return new Parameter<M>(); }
        }
    }
}
