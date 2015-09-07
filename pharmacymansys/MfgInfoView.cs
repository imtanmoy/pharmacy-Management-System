using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pharmacymansys
{
    public partial class MfgInfoView : Form
    {
        public MfgInfoView()
        {
            InitializeComponent();
        }
        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);
        void dataView(string sid)
        {

            try
            {
                conn.Open();
                string sqlquery = "SELECT * FROM MED_INFO WHERE MED_ID='" + sid + "'";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataReader r = cmd.ExecuteReader();
                r.Read();
                textBox1.Text = r.GetValue(1).ToString();
                textBox2.Text = r.GetValue(8).ToString();
                richTextBox1.Text = r.GetValue(9).ToString();
                r.Dispose();
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
    }
}
