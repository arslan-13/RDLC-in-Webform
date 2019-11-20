using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using Microsoft.ReportingServices.Interfaces;

namespace RDLC
{
    public partial class RDLC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            DataTable dt = GetData();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/HIMS/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetForReport", dt));


        }

        private DataTable GetData()
        {
            DataTable Result = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Source_ManagementConnectionString"].ConnectionString))
            {
                try
                {
                    //string Reportquery = "SELECT Requisition_Id, Release_Number, Client_Id, Module_Id, Requester_Id, Authorizer_Id, Reference, Request_Date, Priority_Id, Developer_id, Receive_Date, Description_Of_Change FROM Requisition WHERE(Requisition_Id = @RID)";

                    SqlCommand cmd = new SqlCommand("spreport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RID", TextBox1.Text);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(Result);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
            }
            return Result;
        }
    }
}