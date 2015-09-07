using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace pharmacymansys
{
    public partial class StuffInfo : Form
    {
        public StuffInfo(string mid)
        {
            InitializeComponent();
            dataView(mid);
            ni = mid;
        }
        string ni;

        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);

        void dataView(string sid)
        {

            try
            {
                conn.Open();
                string sqlquery = "SELECT * FROM EMPLOYE_INFO WHERE EMP_ID='" + sid + "'";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataReader r = cmd.ExecuteReader();
                r.Read();
                textBox1.Text = r.GetValue(1).ToString();
                textBox2.Text = r.GetValue(2).ToString();
                textBox3.Text = r.GetValue(3).ToString();
             //  string dt = r.GetValue(4).ToString();
              // DateTime dtt = DateTime.ParseExact(r.GetValue(4).ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                textBox7.Text = r.GetOracleDate(4).ToString();
              // textBox7.Text = dtt.ToString();
                textBox8.Text = r.GetValue(5).ToString();
                textBox4.Text = r.GetValue(6).ToString();
                textBox5.Text = r.GetValue(7).ToString();
                textBox9.Text = r.GetValue(8).ToString();
                textBox6.Text = r.GetValue(9).ToString();
                textBox10.Text = r.GetOracleDate(12).ToString();
                richTextBox1.Text = r.GetValue(10).ToString();
                richTextBox2.Text = r.GetValue(11).ToString();
                textBox11.Text = r.GetValue(14).ToString();
                textBox12.Text = r.GetValue(15).ToString();

                byte[] igg = (byte[])(r["IMAGE"]);
                if (igg == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    MemoryStream mstream = new MemoryStream(igg);
                    pictureBox1.Image = System.Drawing.Image.FromStream(mstream);
                }
                     



                r.Dispose();
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditStuffinfo esi = new EditStuffinfo(ni);
            esi.Show();
        }




    }
}
