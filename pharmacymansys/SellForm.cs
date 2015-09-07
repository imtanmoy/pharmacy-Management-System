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
    public partial class SellForm : Form
    {
        public SellForm()
        {
            InitializeComponent();
            sellinvoiceNo();
            timer1.Start();
            AutocompleteText();
        }
        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);

        string tdate;

        void sellinvoiceNo()
        {

            //check if the table has any data
            //if no then insert data with the date(today)

            string ReturnId = null;
            int rowno = 0;

            var stime = DateTime.Now;
            //var stime = DateTime.Now.AddDays(1);
            //this.textBox1.Text = stime.ToString("PIVMMyydd");
            tdate = stime.ToString("yyyyMMdd");


            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string sqlquery = "SELECT * FROM SALESINVOICE WHERE SDATE=TO_DATE('" + tdate + "', 'yyyymmdd')";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                rowno = ds.Tables[0].Rows.Count;
                if (rowno == 0)
                {
                    ReturnId = "0001";
                }
                else
                {
                    rowno++;
                    ReturnId = rowno.ToString();
                }

                conn.Close();


            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }


            if (ReturnId.Length == 1)
            {
                ReturnId = "000" + rowno;
            }
            if (ReturnId.Length == 2)
            {
                ReturnId = "00" + rowno;
            }
            if (ReturnId.Length == 3)
            {
                ReturnId = "0" + rowno;
            }


            //if 


            string pdate = stime.ToString("MMddyy");
            string bug = pdate + ReturnId;
            this.textBox1.Text = bug;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            this.textBox2.Text = time.ToString("dd/MM/yyyy");
        }


        void AutocompleteText()
        {
            textBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }


                string sqluery = "SELECT * FROM MED_INFO ORDER BY MED_NAME";
                OracleCommand cd = new OracleCommand(sqluery, conn);
                OracleDataReader r;
                r = cd.ExecuteReader();
                while (r.Read())
                {
                    string sn = r.GetString(1);
                    coll.Add(sn);
                }


                r.Dispose();
                cd.Dispose();
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }


            textBox4.AutoCompleteCustomSource = coll;

        }

       

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            this.textBox7.Clear();
            this.textBox8.Clear();
            this.textBox9.Clear();
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string sqlquery = "SELECT MED_ID,SELL_PRICE FROM MED_INFO WHERE MED_NAME='" + textBox4.Text + "'";
                    OracleCommand cmd = new OracleCommand(sqlquery, conn);
                    OracleDataReader r = cmd.ExecuteReader();
                    r.Read();
                    textBox5.Text = r.GetValue(0).ToString();
                    textBox9.Text = r.GetValue(1).ToString();

                    r.Dispose();
                    cmd.Dispose();

                    sqlquery = "SELECT MED_QNTY FROM MED_STORE WHERE MED_ID='" + textBox5.Text + "'";
                    cmd = new OracleCommand(sqlquery, conn);
                    r = cmd.ExecuteReader();
                    r.Read();
                    textBox6.Text = r.GetValue(0).ToString();
                    r.Dispose();
                    cmd.Dispose();







                    conn.Close();
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message);
                }
            }

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            decimal pp = 0;
            int qty = 0;

            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                pp = 0;
            }
            else if (string.IsNullOrWhiteSpace(textBox9.Text))
            {

            }
            else
            {
                pp = decimal.Parse(textBox9.Text);
                qty = int.Parse(textBox8.Text);
            }
            decimal rst = pp * qty;
            string tt = rst.ToString("0.##");
          //  MessageBox.Show(tt);
            textBox7.Text = tt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = n + 1;
            dataGridView1.Rows[n].Cells[1].Value = textBox4.Text;
            dataGridView1.Rows[n].Cells[2].Value = textBox9.Text;
            dataGridView1.Rows[n].Cells[3].Value = textBox8.Text;
            dataGridView1.Rows[n].Cells[4].Value = textBox7.Text;
           



            try
            {
                decimal sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {


                    sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                }
                textBox12.Text = sum.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        decimal ftax = 0;

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            textBox11.Clear();
            textBox13.Clear();
            // decimal gt = 0;
            decimal tax = 0;
            decimal tt = decimal.Parse(textBox12.Text);
            tax = (tt * 15) / 100;
            ftax = tax + tt;
            textBox11.Text = tax.ToString("0.##");
            textBox13.Text = ftax.ToString("0.##");
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            textBox13.Clear();
            decimal gt = 0;
            decimal dis;
           // decimal tax = decimal.Parse(textBox11.Text);
            if (string.IsNullOrWhiteSpace(textBox12.Text))
            {
                dis = 0;
            }
            else
            {
                dis = decimal.Parse(textBox10.Text);
            }

            gt = ftax - dis;
            textBox13.Text = gt.ToString("0.##");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string inv = this.textBox1.Text;
            string cust = this.textBox3.Text;
            string mob = this.textBox14.Text;
            string add = this.richTextBox1.Text;
            string amount = this.textBox12.Text;
            string tax = this.textBox11.Text;
            string dis = this.textBox10.Text;
            if (string.IsNullOrWhiteSpace(textBox10.Text))
            {
                dis = "0";
            }
            string gt = this.textBox13.Text;

            /// pinvoice database insert

            try
            {
                conn.Open();
                string sqlquery = "INSERT INTO SALESINVOICE(SALE_ID,SDATE,CUST_NAME,CUST_MOBILE,CUST_ADD,AMOUNT,TAX,DISCOUNT,GTOTAL) VALUES('" + inv + "',TO_DATE('" + tdate + "', 'yyyymmdd'),'" + cust + "','" + mob + "','" + add + "','" + amount + "','" + tax + "','" + dis + "','" + gt + "')";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    cmd.Dispose();
                    MessageBox.Show("Done");


                }
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }


            /////////////////////////////////////////




            string StrQuery;
            // int j=0;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    StrQuery = "INSERT INTO SALES_DETAILS(SALE_ID,MED_NAME,UPRICE,QTY,TOTAL) VALUES ('"
                        + this.textBox1.Text + "','"
                        + dataGridView1.Rows[i].Cells[1].Value + "','"
                        + dataGridView1.Rows[i].Cells[2].Value + "','"
                        + dataGridView1.Rows[i].Cells[3].Value + "','"
                        + dataGridView1.Rows[i].Cells[4].Value + "')";
                    cmd.CommandText = StrQuery;
                    cmd.ExecuteNonQuery();
                }

                //int i = cmd.ExecuteNonQuery();

                cmd.Dispose();
                MessageBox.Show("Done");
               // this.Hide();
                conn.Close();


            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }

            ///////////////////////////////////////////////



            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {

                    string sid = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string sqlquery = "SELECT * FROM MED_STORE WHERE MED_NAME='" + sid + "'";
                    OracleCommand cmd = new OracleCommand(sqlquery, conn);
                    OracleDataReader r = cmd.ExecuteReader();
                    r.Read();

                    // textBox5.Text = r.GetValue(3).ToString();

                    //    MessageBox.Show(r.GetValue(2).ToString());


                    int qty = int.Parse(r.GetValue(2).ToString()) - int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());


                    r.Dispose();

                    cmd = new OracleCommand("UPDATE MED_STORE SET MED_QNTY='" + qty + "' WHERE MED_NAME='" + sid + "'", conn);

                    cmd.ExecuteNonQuery();




                }
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }




        }





    }
}
