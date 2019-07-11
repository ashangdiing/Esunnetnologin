using Esunnet.Record.Bll.WebService;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Esunnet.Record
{
    public partial class Record : System.Web.UI.Page
    {
        private IRecord ir = Esunnet.Tool.Setting.Biz.GetWebServiceBean<IRecord>();
        public string[] GetRecordByCallId(string callid)
        {
            return ir.GetRecordByCallId(callid);
        }
        public string GetRecordByTsId(string tsid)
        {
            return ir.GetRecordByTsId(tsid);
        }
        protected void DownloadFile(string filename)
        {
            if (string.IsNullOrEmpty(filename)) { return; }
            FileStream MyFileStream;
            long FileSize;
            //filename = path + filename;
            MyFileStream = new FileStream(filename, FileMode.Open);
            FileSize = MyFileStream.Length;
            byte[] Buffer = new byte[(int)FileSize];
            MyFileStream.Read(Buffer, 0, (int)FileSize);
            MyFileStream.Close();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename.Substring(filename.LastIndexOf("\\") + 1));
            Response.ContentType = "audio/wav";
            Response.BinaryWrite(Buffer);
            Response.Flush();
            Response.Close();
            Response.End();
        }
        protected void DownloadFile(string[] filename)
        {
            ZipFile zip = new ZipFile();
            string u = "";// path;
            for (int i = 0; i < filename.Length; i++)
            {
                if (File.Exists(u + filename[i]))
                {
                    try
                    {
                        zip.AddFile(u + filename[i]);
                    }
                    catch { }
                }
            }
            Response.AddHeader("Content-Disposition", "attachment;filename=Record.zip");
            Response.ContentType = "application/zip";
            zip.Save(Response.OutputStream);
            Response.End();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["callid"]))
            {
                string[] s = GetRecordByCallId(Request["callid"]);
                if (s != null && s.Length > 0)
                {
                    Response.Redirect(s[0]);
                }
            }
            else if (!string.IsNullOrEmpty(Request["tsid"]))
            {
                Response.Redirect(GetRecordByTsId(Request["tsid"]));
            }
            else if (!string.IsNullOrEmpty(Request["dc"]))
            {
                //Response.Redirect(GetRecordByTsId(Request["tsid"]));
            }
            else if (!string.IsNullOrEmpty(Request["dt"]))
            {
                //Response.Redirect(GetRecordByTsId(Request["tsid"]));
            }
        }
    }
}