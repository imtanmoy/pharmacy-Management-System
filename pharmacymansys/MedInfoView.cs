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
    public partial class MedInfoView : Form
    {
        public MedInfoView(string mid)
        {
            InitializeComponent();
            dataView(mid);
            ni = mid;
        }
        string ni;
        
        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);

        void dataView(string sid) {

            try
            {
                conn.Open();
                string sqlquery = "SELECT * FROM MED_INFO WHERE MED_ID='"+sid+"'";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataReader r = cmd.ExecuteReader();
                r.Read();
                textBox1.Text = r.GetValue(1).ToString();
                textBox6.Text = r.GetValue(2).ToString();
                textBox5.Text = r.GetValue(3).ToString();
                textBox3.Text = r.GetValue(4).ToString();
                textBox7.Text = r.GetValue(5).ToString();
                textBox8.Text = r.GetValue(6).ToString();
                textBox4.Text = r.GetValue(7).ToString();
                textBox2.Text = r.GetValue(8).ToString();
                richTextBox1.Text = r.GetValue(9).ToString();
                r.Dispose();
                conn.Close();
            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditMedInfo mdi = new EditMedInfo(ni);
            mdi.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string sqlquery = "DELETE FROM MED_INFO WHERE MED_ID='" + ni + "'";
                    OracleCommand cmd = new OracleCommand(sqlquery, conn);
                   cmd.ExecuteNonQuery();
                   cmd.Dispose();


                   sqlquery = "DELETE FROM MED_STORE WHERE MED_ID='" + ni + "'";
                   cmd = new OracleCommand(sqlquery, conn);
                    cmd.ExecuteNonQuery();
                   cmd.Dispose();

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
