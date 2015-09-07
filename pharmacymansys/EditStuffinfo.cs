using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pharmacymansys
{
    public partial class EditStuffinfo : Form
    {
        public EditStuffinfo(string mid)
        {
            InitializeComponent();
            nid = mid;
            dataView(mid);
        }

        string nid;

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
                //textBox7.Text = r.GetValue(4).ToString();
                //textBox7.Text = r.GetOracleDate(4).ToString();
                // textBox7.Text = dtt.ToString();
                string date = r.GetOracleDate(4).ToString();
                DateTime dt = Convert.ToDateTime(date, System.Globalization.CultureInfo.CreateSpecificCulture("en-us").DateTimeFormat);
                dateTimePicker1.Value = dt;
              //  MessageBox.Show(dt.ToString());
              //  textBox8.Text = r.GetValue(5).ToString();
                comboBox2.Text = r.GetValue(5).ToString();
                textBox4.Text = r.GetValue(6).ToString();
                textBox5.Text = r.GetValue(7).ToString();
               // textBox9.Text = r.GetValue(8).ToString();
                comboBox3.Text = r.GetValue(8).ToString();
                textBox6.Text = r.GetValue(9).ToString();
              //  textBox10.Text = r.GetValue(12).ToString();
                string jdate = r.GetOracleDate(12).ToString();
                DateTime jdt = Convert.ToDateTime(jdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-us").DateTimeFormat);
                dateTimePicker2.Value = jdt;
                richTextBox1.Text = r.GetValue(10).ToString();
                richTextBox2.Text = r.GetValue(11).ToString();
                string pos = r.GetValue(14).ToString();

                if (pos == "Manager")
                {
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    comboBox1.SelectedIndex = 1;
                }

                
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




    }
}
