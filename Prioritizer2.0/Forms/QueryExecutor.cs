using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Data.EntityClient;

namespace Prioritizer2._0
{
    public partial class QueryExecutor : Form
    {
        public QueryExecutor()
        {
            InitializeComponent();
        }

        //private void radButton1_Click(object sender, EventArgs e)
        //{
        //    IDbConnection dbConn = NewPrioritizer.repository.Connection;
        //    dbConn.Open();
        //    SqlCommand dc = (SqlCommand)dbConn.CreateCommand();
        //    dc.CommandText = "SELECT ID, userName, domainUserName, email FROM Users";
            
        //    SqlDataReader dr = dc.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    dbConn.Close();
        //    //SqlDataAdapter oDa = new SqlDataAdapter();
        //    //oDa.SelectCommand  = dc;
        //    //DataSet oDs = new DataSet();
        //    //oDa.Fill(oDs, "Results");
            

        //    System.Data.SqlServerCe.SqlCeConnectionStringBuilder sb = new System.Data.SqlServerCe.SqlCeConnectionStringBuilder();
        //    sb.DataSource = "c:\\temp\\prioritizedb.sdf";
        //    sb.Password = "1q2w3e4R";
           
        //    SqlCeConnection sqlConnection = new SqlCeConnection(sb.ToString());
        //    string log;
        //    bool exceptionOccured;
        //    DataSet ds = simpleQueryDataset(sqlConnection, txtSql.Text, out log, out exceptionOccured);
        //    gridResults.DataSource = ds.Tables[0];
        //}


        public static DataSet simpleQueryDataset(SqlCeConnection oCn, string sql, out string log, out bool exceptionOccured)
        {
            try
            {
                DataSet oDs = new DataSet();
                SqlCeDataAdapter oDa = new SqlCeDataAdapter();
                SqlCeCommand oSelCmd = new SqlCeCommand(sql, oCn);
                oSelCmd.CommandType = CommandType.Text;
                oDa.SelectCommand = oSelCmd;
                oDa.Fill(oDs, "Results");
               
                //lblResult.Text = "Log: Sql Statement executed successfully";
                log = "Sql Statement executed successfully";
                //lblResult.ForeColor = Color.Green;
                exceptionOccured = false;
                return oDs;
            }
            catch (Exception ex)
            {
                log = ex.Message;
                exceptionOccured = true;
                //lblResult.Text = ex.Message;
                //lblResult.ForeColor = Color.Red;
            }

            return null;



        }
    }
}
