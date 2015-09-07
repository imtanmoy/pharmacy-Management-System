using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
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
    public partial class AddMedicineInfo : Form
    {
        public AddMedicineInfo()
        {
            InitializeComponent();
            mgffill();
            groupfill();
        }

        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);


        void mgffill() {
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
            catch(Exception exe){
                MessageBox.Show(exe.Message);
            }
            
        }
        void groupfill() {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MedicineInfo_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mgfnm = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string grp = comboBox2.GetItemText(this.comboBox2.SelectedItem);
            string mtype = comboBox3.GetItemText(this.comboBox3.SelectedItem);
            string nid = MedIDgen.IdGenrator(mgfnm);
            string mnm = textBox1.Text;
            string mbt = textBox3.Text;
            string mcp = textBox4.Text;
            string msp = textBox2.Text;
            string mnt = richTextBox1.Text;
            string stg = textBox6.Text;
            conn.Open();
            try 
            {
                
                OracleCommand cmd = new OracleCommand("INSERT INTO MED_INFO(MED_ID,MED_NAME,MED_STG,MED_MGF,MED_BATCH,MED_GROUP,MED_TYPE,COST_PRICE,SELL_PRICE,NOTES) VALUES('" + nid + "','" + mnm + "','"+stg+"','" + mgfnm + "','" + mbt + "','" + grp + "','" + mtype + "','" + mcp + "','" + msp + "','" + mnt + "')", conn);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("DONE");
                    
                    cmd.Dispose();
                    this.Hide();
                }
            }
            catch(Exception exe){
                MessageBox.Show(exe.Message);
            }


            try
            {

                OracleCommand cmd = new OracleCommand("INSERT INTO MED_STORE(MED_ID,MED_NAME,MED_QNTY,DAM_QNTY,RE_LEVEL) VALUES('" + nid + "','" + mnm + "','" + 0 + "','" + 0 + "','" + 0 + "')", conn);
                int j = cmd.ExecuteNonQuery();
                if (j > 0)
                {
                    cmd.Dispose();
                    this.Hide();


                    adminControls adm = new adminControls();
                    adm.Show();
                    MedInfoView miv = new MedInfoView(nid);
                    miv.Show();
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
            
            conn.Close();
            
        }

        private void MedicineInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            adminControls adm = new adminControls();
            adm.Show();
            this.Hide();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Utilities.ResetAllControls(this);
        }
    }
}
