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
    public partial class EditMedInfo : Form
    {
        public EditMedInfo(string mid)
        {
            InitializeComponent();
            mgffill();
            groupfill();
            dataView(mid);
            nid = mid;
        }
        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);
        String nid;

        void mgffill()
        {
            try
            {
                conn.Open();
                string sqlquery = "SELECT MGF_NAME FROM MFG_INFO ORDER BY MGF_NAME";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "MGF_NAME";
                    //comboBox1.ValueMember = "Cust_Name";
                }
                oda.Dispose();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }

        }


        void groupfill()
        {

            try
            {
                conn.Open();
                string sqlquery = "SELECT GNAME FROM MED_GROUP ORDER BY GNAME";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                oda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "GNAME";
                    //comboBox1.ValueMember = "Cust_Name";
                }
                oda.Dispose();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }




        void dataView(string sid)
        {


            try
            {
                conn.Open();
                string sqlquery = "SELECT * FROM MED_INFO WHERE MED_ID='" + sid + "'";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataReader r = cmd.ExecuteReader();
                r.Read();
                textBox5.Text = r.GetValue(0).ToString();
                textBox1.Text = r.GetValue(1).ToString();
                textBox6.Text = r.GetValue(2).ToString();
               // comboBox1.DisplayMember = r.GetValue(3).ToString();
                comboBox1.SelectedIndex = comboBox1.FindStringExact(r.GetValue(3).ToString());
                textBox3.Text = r.GetValue(4).ToString();
                comboBox2.SelectedIndex = comboBox2.FindStringExact(r.GetValue(5).ToString());
             //   comboBox2.DisplayMember = r.GetValue(5).ToString();
                comboBox3.SelectedIndex = comboBox3.FindStringExact(r.GetValue(6).ToString());
               // textBox8.Text = r.GetValue(6).ToString();
                textBox4.Text = r.GetValue(7).ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text) || string.IsNullOrWhiteSpace(this.textBox2.Text) || string.IsNullOrWhiteSpace(this.textBox3.Text) || string.IsNullOrWhiteSpace(this.textBox5.Text) || string.IsNullOrWhiteSpace(this.textBox5.Text) || string.IsNullOrWhiteSpace(this.textBox6.Text) || string.IsNullOrWhiteSpace(this.richTextBox1.Text))
            {
                MessageBox.Show("Fill all the requierd field correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                try
                {
                    string mgf = comboBox1.GetItemText(this.comboBox1.SelectedItem);
                    string grp = comboBox1.GetItemText(this.comboBox1.SelectedItem);
                    string mtype = comboBox3.GetItemText(this.comboBox3.SelectedItem);
                    conn.Open();
                    string sqlq = "UPDATE MED_INFO SET MED_NAME='" + textBox1.Text + "',MED_STG='" + textBox6.Text + "',MED_MGF='" + mgf + "',MED_BATCH='" + textBox3.Text + "',MED_GROUP='" + grp + "',MED_TYPE='" + mtype + "',COST_PRICE='" + textBox4.Text + "',SELL_PRICE='" + textBox2.Text + "',NOTES='" + richTextBox1.Text + "' WHERE MED_ID='" + nid + "'";
                    OracleCommand cmd = new OracleCommand(sqlq, conn);
                    int i= cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        //MessageBox.Show("Information Uploaded...");

                        cmd.Dispose();
                        conn.Close();
                        this.Hide();
                        MedInfoView miv = new MedInfoView(nid);
                        miv.Show();
                    }

                    
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       


    }
}
