using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using Sbw.Json;

namespace Sbw.DbClinet
{
	internal delegate T Execute<T>();
	public class DbClinet : IDisposable
	{
		#region 私有属性
		/// <summary>
		/// 数据库连接池
		/// </summary>
		private DbProviderFactory objFactory = null;

		/// <summary>
		/// 指向Config文件中连接字符串的键
		/// </summary>
		private static readonly string WebConfigKey = "Default";

		/// <summary>
		/// 数据库连接对象
		/// </summary>
		private DbConnection con;

		/// <summary>
		/// 数据库操作对象
		/// </summary>
		private DbCommand cmd;

		/// <summary>
		/// 是否打开事物
		/// </summary>
		private bool isOpenTran = false;
		#endregion
		#region 共有属性
		/// <summary>
		/// 处理异常的类
		/// </summary>
		public DboException Exception { get; set; }
		#endregion
		#region 类初始化
		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="con">连接字符串</param>
		/// <param name="provider">数据库类型</param>
		public DbClinet(string conString, Providers provider)
		{
			Exception = new DboException();
			switch (provider)
			{
				case Providers.SqlServer: objFactory = SqlClientFactory.Instance; break;
				case Providers.ODBC: objFactory = OdbcFactory.Instance; break;
				case Providers.OleDb: objFactory = OleDbFactory.Instance; break;
				case Providers.Oracle: objFactory = OracleClientFactory.Instance; break;
			}
			if (objFactory == null) Exception.Exception("未初始化数据连接池");
			this.con = objFactory.CreateConnection();
			this.con.ConnectionString = conString;
			this.cmd = objFactory.CreateCommand();
			this.cmd.Connection = con;
		}
        public DbClinet()
		{
			Exception = new DboException();
            switch (ConfigurationManager.ConnectionStrings[DbClinet.WebConfigKey].ProviderName)
			{
				case "System.Data.SqlClient": objFactory = SqlClientFactory.Instance;
					break;
				case "System.Data.OleDb": objFactory = OleDbFactory.Instance;
					break;
				case "System.Data.OracleClient": objFactory = OracleClientFactory.Instance;
					break;
				case "System.Data.Odbc": objFactory = OdbcFactory.Instance;
					break;
			}
			if (objFactory == null) Exception.Exception("未初始化数据连接池");
			this.con = objFactory.CreateConnection();
            this.con.ConnectionString = ConfigurationManager.ConnectionStrings[DbClinet.WebConfigKey].ConnectionString;
			this.cmd = objFactory.CreateCommand();
			this.cmd.Connection = con;
		}
		/// <summary>
		/// 初始化一个数据库操作类
		/// </summary>
		/// <param name="conString">连接字符串</param>
		/// <param name="provider">数据库类型</param>
		/// <returns>数据库操作类</returns>
        public static DbClinet Init(string conString, Providers provider) { return new DbClinet(conString, provider); }
		/// <summary>
		/// 使用 web.Config 文件中的 数据库连接 结点进行数据库连接
		/// </summary>
		/// <param name="webConfigKey">节点的键</param>
		/// <returns>数据库操作类</returns>
        public static DbClinet Init(string webConfigKey)
		{
			Providers provider = Providers.SqlServer;
			switch (ConfigurationManager.ConnectionStrings[webConfigKey].ProviderName)
			{
				case "System.Data.SqlClient": provider = Providers.SqlServer;
					break;
				case "System.Data.OleDb": provider = Providers.OleDb;
					break;
				case "System.Data.OracleClient": provider = Providers.Oracle;
					break;
				case "System.Data.Odbc": provider = Providers.ODBC;
					break;
			}
            return new DbClinet(ConfigurationManager.ConnectionStrings[webConfigKey].ConnectionString, provider);
		}
		/// <summary>
		/// 使用 web.Config 文件中的 数据库连接 结点进行数据库连接 采用类默认的 键
		/// </summary>
		/// <returns>数据库操作类</returns>
        public static DbClinet Init() { return Init(WebConfigKey); }
		#endregion

		#region 添加参数
		/// <summary>
		/// 添加该参数。
		/// </summary>
		/// <param name="name">名字。</param>
		/// <param name="value">值。</param>
		/// <returns></returns>
		public DbParameter AddParameter(string name, object value)
		{
			DbParameter p = objFactory.CreateParameter();
			p.ParameterName = name;
			p.Value = value;
			cmd.Parameters.Add(p);
			return p;
		}
		/// <summary>
		/// 添加参数
		/// </summary>
		/// <param name="p">参数</param>
		/// <returns></returns>
		public DbParameter AddParameter(DbParameter p)
		{
			cmd.Parameters.Add(p);
			return p;
		}
		/// <summary>
		/// 添加输入参数
		/// </summary>
		/// <param name="name">名字</param>
		/// <param name="type">类型</param>
		/// <returns></returns>
		public DbParameter AddParameter(string name, DbType type)
		{
			DbParameter p = objFactory.CreateParameter();
			p.ParameterName = name;
			p.DbType = type;
			p.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(p);
			return p;
		}
		/// <summary>
		/// 添加参数
		/// </summary>
		/// <param name="name">名字</param>
		/// <param name="type">类型</param>
		/// <param name="pd">是 输出还是 输入参数</param>
		/// <returns></returns>
		public DbParameter AddParameter(string name, DbType type, ParameterDirection pd)
		{
			DbParameter p = objFactory.CreateParameter();
			p.ParameterName = name;
			p.DbType = type;
			p.Direction = pd;
			cmd.Parameters.Add(p);
			return p;
		}
		/// <summary>
		/// 添加输出参数
		/// </summary>
		/// <param name="name">名字</param>
		/// <param name="type">类型</param>
		/// <param name="size">大小</param>
		/// <returns></returns>
		public DbParameter AddParameter(string name, DbType type, int size)
		{
			DbParameter p = objFactory.CreateParameter();
			p.ParameterName = name;
			p.DbType = type;
			p.Size = size;
			p.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(p);
			return p;
		}
		/// <summary>
		/// 添加参数
		/// </summary>
		/// <param name="name">名字</param>
		/// <param name="type">类型</param>
		/// <param name="size">大小</param>
		/// <param name="pd">是 输出还是 输入参数</param>
		/// <returns></returns>
		public DbParameter AddParameter(string name, DbType type, int size, ParameterDirection pd)
		{
			DbParameter p = objFactory.CreateParameter();
			p.ParameterName = name;
			p.DbType = type;
			p.Size = size;
			p.Direction = pd;
			cmd.Parameters.Add(p);
			return p;
		}
		/// <summary>
		/// 当连接是打开的时候关闭连接
		/// </summary>
		public void Close()
		{
			if (con.State == ConnectionState.Open)
			{
				con.Close();
			}
		}
		/// <summary>
		/// 添加参数
		/// </summary>
		/// <param name="name">名字</param>
		/// <param name="vaule">值</param>
		/// <param name="size">大小</param>
		/// <param name="pd">是 输出还是 输入参数</param>
		/// <returns></returns>
		public DbParameter AddParameter(string name, object value, int size, ParameterDirection pd)
		{
			DbParameter p = objFactory.CreateParameter();
			p.ParameterName = name;
			p.Value = value;
			p.Size = size;
			p.Direction = pd;
			cmd.Parameters.Add(p);
			return p;
		}
		/// <summary>
		/// 添加返回参数
		/// 名字为 @ReturnValue
		/// </summary>
		/// <returns></returns>
		public DbParameter AddParameter()
		{
			DbParameter p = objFactory.CreateParameter();
			p.ParameterName = "@ReturnValue";
			p.Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add(p);
			return p;
		}
        public void ClearParamenter()
        {
            cmd.Parameters.Clear();
        }
		#endregion

		#region 执行方法
		/// <summary>
		/// 执行Command设置好所有参数
		/// </summary>
		/// <typeparam name="T">返回类型参数</typeparam>
		/// <param name="sql">执行的sql语句</param>
		/// <param name="type">执行的类型</param>
		/// <param name="execute">执行的那个方法</param>
		/// <returns>返回指定参数</returns>
		private T ExecuteCmd<T>(string sql, CommandType type, Execute<T> execute)
		{
			try
            {
                Sbw.DbClinet.Log.Debug("Sql:{0},type:{1}", sql, type);
				cmd.CommandText = sql;
				cmd.CommandType = type;
				if (con.State == System.Data.ConnectionState.Closed)
				{
					con.Open();
				}
				return execute();
			}
			catch (DbException e)
			{
				Exception.Log(e, sql, cmd.Parameters);
				return default(T);
			}
			finally
			{
				cmd.Parameters.Clear();
				if (!isOpenTran && !typeof(T).IsAssignableFrom(typeof(DbDataReader)))
				{
					Close();
				}
			}
		}
		/// <summary>
		/// 执行Command设置好所有参数
		/// </summary>
		/// <typeparam name="T">返回类型参数</typeparam>
		/// <param name="proc">存储过程</param>
		/// <param name="execute">执行的那个方法</param>
		/// <returns>返回指定参数</returns>
		private T ExecuteCmd<T>(Proc proc, Execute<T> execute)
		{
			try
			{
				cmd.CommandText = proc.ProcName;
				cmd.CommandType = CommandType.StoredProcedure;
				if (con.State == System.Data.ConnectionState.Closed)
				{
					con.Open();
				}
				cmd.Parameters.AddRange(proc.InitParameters(cmd.CreateParameter));
				return execute();
			}
			catch (DbException e)
			{
				Exception.Log(e, proc.ProcName, cmd.Parameters);
				return default(T);
			}
			finally
			{
				proc.InitOutput();
				cmd.Parameters.Clear();
				if (!isOpenTran && !typeof(T).IsAssignableFrom(typeof(DbDataReader)))
				{
					Close();
				}
			}
		}

		#region int ExecuteNonQuery() 函数调用
		/// <summary>
		/// 执行sql语句返回影响行数
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <param name="type">执行的类型</param>
		/// <returns>返回影响行数</returns>
		public int ExecuteNonQuery(string sql, CommandType type)
		{
			return ExecuteCmd<int>(sql, type, cmd.ExecuteNonQuery);
		}
		/// <summary>
		/// 执行sql语句返回影响行数
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <returns>返回影响行数</returns>
		public int ExecuteNonQuery(string sql)
		{
			return ExecuteNonQuery(sql, CommandType.Text);
		}
		/// <summary>
		/// 执行存储过程返回影响行数
		/// </summary>
		/// <param name="proc">需要执行的存储过程</param>
		/// <returns>返回影响行数</returns>
		public int ExecuteNonQuery(Proc proc)
		{
			return ExecuteCmd<int>(proc, cmd.ExecuteNonQuery);
		}
		#endregion

		#region object ExecuteScalar() 函数调用
		/// <summary>
		/// 执行sql语句返回第一行第一列的值
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <param name="type">执行的类型</param>
		/// <returns>返回第一行第一列的值</returns>
		public object ExecuteScalar(string sql, CommandType type)
		{
			return ExecuteCmd<object>(sql, type, cmd.ExecuteScalar);
		}
		/// <summary>
		/// 执行sql语句返回第一行第一列的值
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <returns>返回第一行第一列的值</returns>
		public object ExecuteScalar(string sql)
		{
			return ExecuteScalar(sql, CommandType.Text);
		}
		/// <summary>
		/// 执行存储过程返回第一行第一列的值
		/// </summary>
		/// <param name="proc">存储过程</param>
		/// <returns>返回第一行第一列的值</returns>
		public object ExecuteScalar(Proc proc)
		{
			return ExecuteCmd<object>(proc, cmd.ExecuteScalar);
		}
		#endregion

		#region DbDataReader ExecuteReader() 函数调用
		/// <summary>
		/// 执行sql返回DataReader
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <param name="type">执行的类型</param>
		/// <returns>返回DataReader</returns>
		public DbDataReader ExecuteReader(string sql, CommandType type)
		{
			return ExecuteCmd<DbDataReader>(sql, type, cmd.ExecuteReader);
		}
		/// <summary>
		/// 执行sql返回DataReader
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <returns></returns>
		public DbDataReader ExecuteReader(string sql)
		{
			return ExecuteReader(sql, CommandType.Text);
		}
		/// <summary>
		/// 执行存储过程返回DataReader
		/// </summary>
		/// <param name="proc">存储过程</param>
		/// <returns>返回DataReader</returns>
		public DbDataReader ExecuteReader(Proc proc)
		{
			return ExecuteCmd<DbDataReader>(proc, cmd.ExecuteReader);
		}
		#endregion

		#region DataSet ExecuteDataSet() 函数调用
		/// <summary>
		/// 执行sql返回DataSet
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <param name="type">执行的类型</param>
		/// <returns>返回DataSet</returns>
		public DataSet ExecuteDataSet(string sql, CommandType type)
		{
			try
			{
				cmd.CommandType = type;
				cmd.CommandText = sql;
				if (con.State == System.Data.ConnectionState.Closed)
				{
					con.Open();
				}
				DbDataAdapter da = objFactory.CreateDataAdapter();
				da.SelectCommand = cmd;
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
			catch (DbException e)
			{
				Exception.Log(e, sql, cmd.Parameters);
				return null;
			}
			finally
			{
				cmd.Parameters.Clear();
				if (!isOpenTran)
				{
					Close();
				}
			}
		}
		/// <summary>
		/// 执行sql返回DataSet
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <returns>返回DataSet</returns>
		public DataSet ExecuteDataSet(string sql)
		{
			return ExecuteDataSet(sql, CommandType.Text);
		}
		/// <summary>
		/// 执行存储过程返回DataSet
		/// </summary>
		/// <param name="proc">存储过程</param>
		/// <returns>返回DataSet</returns>
		public DataSet ExecuteDataSet(Proc proc)
		{
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = proc.ProcName;
				if (con.State == System.Data.ConnectionState.Closed)
				{
					con.Open();
				}
				cmd.Parameters.AddRange(proc.InitParameters(cmd.CreateParameter));
				DbDataAdapter da = objFactory.CreateDataAdapter();
				da.SelectCommand = cmd;
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
			catch (DbException e)
			{
				Exception.Log(e, proc.ProcName, cmd.Parameters);
				return null;
			}
			finally
			{
				proc.InitOutput();
				cmd.Parameters.Clear();
				if (!isOpenTran)
				{
					Close();
				}
			}
		}
		#endregion

		#region DataTable ExecuteDataTable() 函数调用
		/// <summary>
		/// 执行sql返回DataTable
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <param name="type">执行的类型</param>
		/// <returns>返回DataTable</returns>
		public DataTable ExecuteDataTable(string sql, CommandType type)
		{
			try
			{
				cmd.CommandType = type;
				cmd.CommandText = sql;
				if (con.State == System.Data.ConnectionState.Closed)
				{
					con.Open();
				}
				DbDataAdapter da = objFactory.CreateDataAdapter();
				da.SelectCommand = cmd;
				DataTable dt = new DataTable();
				da.Fill(dt);
				return dt;
			}
			catch (DbException e)
			{
				Exception.Log(e, sql, cmd.Parameters);
				return null;
			}
			finally
			{
				cmd.Parameters.Clear();
				if (!isOpenTran)
				{
					Close();
				}
			}
		}
		/// <summary>
		/// 执行sql返回DataTable
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <returns>返回DataTable</returns>
		public DataTable ExecuteDataTable(string sql)
		{
			return ExecuteDataTable(sql, CommandType.Text);
		}
		/// <summary>
		/// 执行存储过程返回DataTable
		/// </summary>
		/// <param name="proc">存储过程</param>
		/// <returns>返回DataTable</returns>
		public DataTable ExecuteDataTable(Proc proc)
		{
			try
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = proc.ProcName;
				if (con.State == System.Data.ConnectionState.Closed)
				{
					con.Open();
				}
				cmd.Parameters.AddRange(proc.InitParameters(cmd.CreateParameter));
				DbDataAdapter da = objFactory.CreateDataAdapter();
				da.SelectCommand = cmd;
				DataTable dt = new DataTable();
				da.Fill(dt);
				return dt;
			}
			catch (DbException e)
			{
				Exception.Log(e, proc.ProcName, cmd.Parameters);
				return null;
			}
			finally
			{
				proc.InitOutput();
				cmd.Parameters.Clear();
				if (!isOpenTran)
				{
					Close();
				}
			}
		}
		#endregion

		#region Select<T>
		/// <summary>
		/// 查询返回Model
		/// </summary>
		/// <typeparam name="T">Model 必须继承 View </typeparam>
		/// <param name="sql">执行的sql语句</param>
		/// <param name="type">执行类型</param>
		/// <returns>返回Model集合</returns>
		public List<T> Select<T>(string sql, CommandType type) where T : IView, new()
		{
			List<T> list = new List<T>();
			using (DbDataReader dr = ExecuteReader(sql, type))
			{
				while (dr.Read())
				{
					T t = new T();
					for (int i = 0; i < dr.FieldCount; i++)
					{
						t[dr.GetName(i)] = dr.GetValue(i);
					}
					list.Add(t);
				}
			}
			Close();
			return list;
		}
		/// <summary>
		/// 查询返回Model
		/// </summary>
		/// <typeparam name="T">Model 必须继承 View </typeparam>
		/// <param name="sql">执行的sql语句</param>
		/// <returns>返回Model集合</returns>
		public List<T> Select<T>(string sql) where T : IView, new()
		{
			return Select<T>(sql, CommandType.Text);
		}
		/// <summary>
		/// 查询返回Model
		/// </summary>
		/// <typeparam name="T">Model 必须继承 View </typeparam>
		/// <param name="proc">存储过程</param>
		/// <returns>返回Model集合</returns>
		public List<T> Select<T>(Proc proc) where T : IView, new()
		{
			List<T> list = new List<T>();
			using (DbDataReader dr = ExecuteReader(proc))
			{
				while (null != dr && dr.Read())
				{
					T t = new T();
					for (int i = 0; i < dr.FieldCount; i++)
					{
						t[dr.GetName(i)] = dr.GetValue(i);
					}
					list.Add(t);
				}
			}
			Close();
			return list;
		}
		/// <summary>
		/// 查询返回Model
		/// </summary>
		/// <param name="sql">执行的sql语句</param>
		/// <param name="type">执行类型</param>
		/// <returns>返回Model集合</returns>
		public List<Dictionary<string, object>> Select(string sql, CommandType type)
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			using (DbDataReader dr = ExecuteReader(sql, type))
			{
				while (null != dr && dr.Read())
				{
					Dictionary<string, object> dic = new Dictionary<string, object>();
					for (int i = 0; i < dr.FieldCount; i++)
					{
						dic[dr.GetName(i)] = dr.GetValue(i);
					}
					list.Add(dic);
				}
			}
			Close();
			return list;
		}
        
		/// <summary>
		/// 查询返回Model
		/// </summary>
		/// <param name="sql">执行的sql语句</param>
		/// <returns>返回Model集合</returns>
		public List<Dictionary<string, object>> Select(string sql)
		{
			return Select(sql, CommandType.Text);
		}
		/// <summary>
		/// 查询返回Model
		/// </summary>
		/// <param name="proc">存储过程</param>
		/// <returns>返回Model集合</returns>
		public List<Dictionary<string, object>> Select(Proc proc)
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			using (DbDataReader dr = ExecuteReader(proc))
			{
				while (null != dr && dr.Read())
				{
					Dictionary<string, object> dic = new Dictionary<string, object>();
					for (int i = 0; i < dr.FieldCount; i++)
					{
						dic[dr.GetName(i)] = dr.GetValue(i);
					}
					list.Add(dic);
				}
			}
			Close();
			return list;
		}
        /// <summary>
        /// 查询返回Model
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="type">执行类型</param>
        /// <returns>返回Model集合</returns>
        public Paging Paging(string sql, CommandType type)
        {
            Paging p = new Sbw.DbClinet.Paging();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            using (DbDataReader dr = ExecuteReader(sql, type))
            {
                while (null != dr && dr.Read())
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        dic[dr.GetName(i)] = dr.GetValue(i);
                    }
                    list.Add(dic);
                }
                if (dr.NextResult() && dr.Read())
                {
                    p.Count = (dr.GetValue(0) as int?).GetValueOrDefault();
                }
            }
            Close();
            p.Data = list;
            return p;
        }
        /// <summary>
        /// 查询返回Model
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="type">执行类型</param>
        /// <returns>返回Model集合</returns>
        public Paging Paging(string sql)
        {
            return Paging(sql,CommandType.Text);
        }
        /// <summary>
        /// 查询返回Model
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="type">执行类型</param>
        /// <returns>返回Model集合</returns>
        public Paging Paging(Proc proc)
        {
            Paging p = new Sbw.DbClinet.Paging();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            using (DbDataReader dr = ExecuteReader(proc))
            {
                while (null != dr && dr.Read())
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        dic[dr.GetName(i)] = dr.GetValue(i);
                    }
                    list.Add(dic);
                }
                if (dr.NextResult() && dr.Read())
                {
                    object o = dr.GetValue(0);
                    p.Count = (o as int?).GetValueOrDefault();
                }
            }
            Close();
            p.Data = list;
            return p;
        }
        public List<object> Array(string sql, CommandType type)
        {
            List<object> list = new List<object>();
            using (DbDataReader dr = ExecuteReader(sql, type))
            {
                while (null != dr && dr.Read())
                {
                    list.Add(dr.GetValue(0));
                }
            }
            Close();
            return list;
        }
        /// <summary>
        /// 查询返回Model
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="type">执行类型</param>
        /// <returns>返回Model集合</returns>
        public List<object> Array(string sql)
        {
            return Array(sql, CommandType.Text);
        }
        /// <summary>
        /// 查询返回Model
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="type">执行类型</param>
        /// <returns>返回Model集合</returns>
        public List<object> Array(Proc proc)
        {

            List<object> list = new List<object>();
            using (DbDataReader dr = ExecuteReader(proc))
            {
                while (null != dr && dr.Read())
                {
                    list.Add(dr.GetValue(0));
                }
            }
            Close();
            return list;
        }
		#endregion

		#region Json
		/// <summary>
		/// 执行sql返回json
		/// </summary>
		/// <param name="sql">执行的sql语句</param>
		/// <param name="type">执行类型</param>
		/// <returns>返回json</returns>
		public string Json(string sql, CommandType type)
		{
			string s = string.Empty;
			using (DbDataReader dr = ExecuteReader(sql, type))
			{
				s = ToJson.DataReaderToJson(dr);
			}
			Close();
			return s;
		}
		/// <summary>
		/// 执行sql返回json
		/// </summary>
		/// <param name="sql">执行的sql语句</param>
		/// <returns>返回json</returns>
		public string Json(string sql)
		{
			return Json(sql, CommandType.Text);
		}
		/// <summary>
		/// 执行存储过程返回json
		/// </summary>
		/// <param name="proc">存储过程</param>
		/// <returns>返回json</returns>
		public string Json(Proc proc)
		{
			string s = string.Empty;
			using (DbDataReader dr = ExecuteReader(proc))
			{
				s = ToJson.DataReaderToJson(dr);
			}
			Close();
			return s;
		}
		#endregion

		#endregion

		#region 控制方法
		/// <summary>
		/// 开始事务。
		/// </summary>
		public void BeginTransaction()
		{
			if (con.State == System.Data.ConnectionState.Closed)
			{
				con.Open();
			}
			cmd.Transaction = con.BeginTransaction();
			isOpenTran = true;
		}
		/// <summary>
		/// 提交事务。
		/// </summary>
		public void CommitTransaction()
		{
			if (isOpenTran)
			{
				cmd.Transaction.Commit();
				Close();
			}
			isOpenTran = false;
		}
		/// <summary>
		/// 回滚事务。
		/// </summary>
		public void RollbackTransaction()
		{
			cmd.Transaction.Rollback();
			Close();
			isOpenTran = false;
		}
		#endregion

		#region IDisposable 成员

		public void Dispose()
		{
			cmd.Dispose();
			con.Dispose();
		}

		#endregion
	}
}