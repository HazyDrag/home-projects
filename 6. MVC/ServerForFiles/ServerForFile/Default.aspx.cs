using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace ServerForFile
{
    public partial class Default : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputFile File1;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private void Page_Load(object sender, System.EventArgs e)
        {
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void Submit1_ServerClick(object sender, System.EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                string fm = System.IO.Path.GetExtension(File1.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Files");
                try
                {
                    if (fm == ".jpg" || fm == ".png")
                        File1.PostedFile.SaveAs(SaveLocation + "\\Images\\" + fn);
                    else if (fm == ".doc" || fm == ".docx" || fm == ".txt" || fm == ".xlsx" || fm == ".xls" || fm == ".pptx" || fm == ".ppt")
                        File1.PostedFile.SaveAs(SaveLocation + "\\Documents\\" + fn);
                    else if (fm == ".rar" || fm == ".zip" )
                        File1.PostedFile.SaveAs(SaveLocation + "\\Archives\\" + fn);
                    else
                        File1.PostedFile.SaveAs(SaveLocation + "\\Other\\" + fn);

                    Response.Write("The file: " + fn + " has been uploaded.");
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
            else
            {
                Response.Write("Please select a file to upload.");
            }
        }

        protected void Repeater1_PreRender(object sender, EventArgs e)
        {
            DirectoryInfo uploads = new DirectoryInfo(Server.MapPath("Files") + "\\Images");
            Repeater1.DataSource = uploads.GetFiles();
            Repeater1.DataBind();
        }

        protected void Repeater2_PreRender(object sender, EventArgs e)
        {
            DirectoryInfo uploads2 = new DirectoryInfo(Server.MapPath("Files") + "\\Documents");
            Repeater2.DataSource = uploads2.GetFiles();
            Repeater2.DataBind();
        }

        protected void Repeater3_PreRender(object sender, EventArgs e)
        {
            DirectoryInfo uploads3 = new DirectoryInfo(Server.MapPath("Files") + "\\Archives");
            Repeater3.DataSource = uploads3.GetFiles();
            Repeater3.DataBind();
        }

        protected void Repeater4_PreRender(object sender, EventArgs e)
        {
            DirectoryInfo uploads4 = new DirectoryInfo(Server.MapPath("Files") + "\\Other");
            Repeater4.DataSource = uploads4.GetFiles();
            Repeater4.DataBind();
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}