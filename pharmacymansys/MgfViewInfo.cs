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
    public partial class MgfViewInfo : Form
    {
        public MgfViewInfo(string mid)
        {
            InitializeComponent();
            dataView(mid);
            nid = mid;
        }
        string nid;
        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);

        void dataView(string sid)
        {

            try
            {
                conn.Open();
                string sqlquery = "SELECT * FROM MFG_INFO WHERE MFG_ID='" + sid + "'";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataReader r = cmd.ExecuteReader();
                r.Read();
                textBox1.Text = r.GetValue(1).ToString();
                richTextBox1.Text = r.GetValue(2).ToString();
                textBox2.Text = r.GetValue(3).ToString();
                r.Dispose();
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string sqlquery = "DELETE FROM MFG_INFO WHERE MFG_ID='" + nid + "'";
                    OracleCommand cmd = new OracleCommand(sqlquery, conn);
                    int i = cmd.ExecuteNonQuery();

                    conn.Close();
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message);
                }
                this.Hide();
            }
            else if (dialogResult == DialogResult.No)
            {
                ///
            }
        }
    }
}
