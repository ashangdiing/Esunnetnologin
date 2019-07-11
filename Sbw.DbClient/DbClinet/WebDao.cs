using System;
using System.Collections.Generic;
using System.Text;

namespace Sbw.DbClinet
{
	public class WebDao
	{
        public DbClinet Clinet { get { return Singleton<DbClinet>.Instance(); } }
	}
}
