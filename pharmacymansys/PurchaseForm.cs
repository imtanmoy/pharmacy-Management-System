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
    public partial class PurchaseForm : Form
    {
        public PurchaseForm()
        {
            InitializeComponent();
            timer1.Start();
            mgffill();
            pinvoiceNo();
            
            
        }


        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);

        string mfgid;

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            this.textBox2.Text= time.ToString("dd/MM/yyyy");
            
        }


        void mgffill()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                    
               
                string sqlquery = "SELECT MGF_NAME FROM MFG_INFO ORDER BY MGF_NAME";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
            
                oda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "MGF_NAME";
                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = -1;  
                    }
                        
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


        private void getData(AutoCompleteStringCollection dataCollection)
        {
            OracleCommand command;
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataSet ds = new DataSet();

            string sql = "SELECT MGF_NAME FROM MFG_INFO ORDER BY MGF_NAME";
            try
            {
            

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                    

                command = new OracleCommand(sql, conn);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
               conn.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }



        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            getData(combData);
            comboBox1.AutoCompleteCustomSource = combData;
        }

        


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {


                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
               
                string sqlquery = "SELECT * FROM MFG_INFO WHERE MGF_NAME='" + comboBox1.Text + "'";
                OracleCommand cmdd = new OracleCommand(sqlquery, conn);
                OracleDataReader rv = cmdd.ExecuteReader();
            
                while (rv.Read()) 
                {
                    mfgid = rv.GetValue(0).ToString();
                    string mobile = rv.GetValue(3).ToString();
                    string add = rv.GetValue(2).ToString();
                    textBox3.Text = mobile;
                    richTextBox1.Text = add;
                }
                
                rv.Dispose();
                conn.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
         
            AutocompleteText();
        }


        void AutocompleteText()
        {
            textBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }


                string sqluery = "SELECT * FROM MED_INFO WHERE MED_MGF='" + comboBox1.Text + "' ORDER BY MED_NAME";
                OracleCommand cd = new OracleCommand(sqluery, conn);
                OracleDataReader r;
                r = cd.ExecuteReader();
                while(r.Read())
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


            textBox5.AutoCompleteCustomSource = coll;

        }

       

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string sqlquery = "SELECT MED_ID FROM MED_INFO WHERE MED_NAME='" + textBox5.Text + "'";
                    OracleCommand cmd = new OracleCommand(sqlquery, conn);
                    OracleDataReader r = cmd.ExecuteReader();
                    r.Read();
                    textBox4.Text = r.GetValue(0).ToString();
                    r.Dispose();
                    cmd.Dispose();

                    sqlquery = "SELECT MED_QNTY FROM MED_STORE WHERE MED_ID='" + textBox4.Text + "'";
                    cmd = new OracleCommand(sqlquery,conn);
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


        string Genname()
        {
            string gn=null;
            try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string sqlquery = "SELECT MED_GROUP FROM MED_INFO WHERE MED_NAME='" + textBox5.Text + "'";
                    OracleCommand cmd = new OracleCommand(sqlquery, conn);
                    OracleDataReader r = cmd.ExecuteReader();
                    r.Read();
                    gn = r.GetValue(0).ToString();
                    r.Dispose();
                    cmd.Dispose();
                    conn.Close();
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message);
                }
            return gn;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = n+1;
            dataGridView1.Rows[n].Cells[1].Value = textBox5.Text;
            dataGridView1.Rows[n].Cells[2].Value = Genname();
            dataGridView1.Rows[n].Cells[3].Value = textBox7.Text;
            dataGridView1.Rows[n].Cells[4].Value = textBox8.Text;
            dataGridView1.Rows[n].Cells[5].Value = textBox9.Text;

       

        try 
            {
                decimal sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                 

                    sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value);
                }
                textBox10.Text = sum.ToString();
              
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            

           

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            decimal pp=0;
            int qty=0 ;

            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                pp = 0;
            }
            else if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
            
            }
            else
            {
                pp = decimal.Parse(textBox7.Text);
                qty = int.Parse(textBox8.Text);
            }
            decimal rst = pp * qty;
            string tt = rst.ToString("0.##");
            textBox9.Text = tt;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            decimal pp = 0;
            int qty=0;
            textBox9.Clear();
            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                qty = 0;
            }
            else if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
            
            }
            else
            {
                qty = int.Parse(textBox8.Text);
                pp = decimal.Parse(textBox7.Text);
            }
            
            decimal rst = pp * qty;
            string tt = rst.ToString("0.##");
            textBox9.Text = tt;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            textBox11.Clear();
            //textBox12.Clear();
            textBox13.Clear();
           // decimal gt = 0;
            decimal tax = 0;
            decimal tt= decimal.Parse(textBox10.Text);
            tax = (tt*15)/100+tt;
            textBox11.Text = tax.ToString("0.##");
            textBox13.Text = tax.ToString("0.##");

            



        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            textBox13.Clear();
            decimal gt = 0;
             decimal dis;
            decimal tax = decimal.Parse(textBox11.Text);
            if (string.IsNullOrWhiteSpace(textBox12.Text))
            {
                dis = 0;
            }
            else
            {
                dis = decimal.Parse(textBox12.Text);
            }
            
            gt = tax - dis;
            textBox13.Text = gt.ToString("0.##");
        }

        private void button2_Click(object sender, EventArgs e)
        {

                        
            
            
           // ReportPreview rp = new ReportPreview();
          //  rp.Show();
        }
        string tdate;

        void pinvoiceNo() 
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
                string sqlquery = "SELECT * FROM PINVICE WHERE PDATE=TO_DATE('" + tdate + "', 'yyyymmdd')";
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


            string pdate = stime.ToString("ddMMyy");
            string bug = pdate + ReturnId;
            this.textBox1.Text = bug;
            
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string inv = this.textBox1.Text;
            string amount = this.textBox10.Text;
            string tax = this.textBox11.Text;
            string dis = this.textBox12.Text;
            if (string.IsNullOrWhiteSpace(textBox12.Text))
            {
                dis = "0";
            }
            string gt = this.textBox13.Text;

            /// pinvoice database insert

            try
            {
                conn.Open();
                string sqlquery = "INSERT INTO PINVICE(PIV_ID,MFG_ID,PDATE,AMOUNT,TAX,DISCOUNT,GTOTAL) VALUES('" + inv + "','" + mfgid + "',TO_DATE('" + tdate + "', 'yyyymmdd'),'"+amount+"','"+tax+"','"+dis+"','"+gt+"')";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    cmd.Dispose();
                    MessageBox.Show("Done");
                    
                   
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }



            // purchase_detail insert

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
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    StrQuery = "INSERT INTO PURCHASE_DETAIL(PIV_ID,MED_NAME,BPRICE,QTY,TOTAL) VALUES ('"
                        + this.textBox1.Text + "','"
                        + dataGridView1.Rows[i].Cells[1].Value + "','"
                        + dataGridView1.Rows[i].Cells[3].Value + "','"
                        + dataGridView1.Rows[i].Cells[4].Value + "','"
                        + dataGridView1.Rows[i].Cells[5].Value + "')";
                    cmd.CommandText = StrQuery;
                    cmd.ExecuteNonQuery();
                }
                
             //int i = cmd.ExecuteNonQuery();
               
                    cmd.Dispose();
                    MessageBox.Show("Done");
                    this.Hide();
                   

               
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }



            //////////////////////////////////////////////////////////////////////////////////////////////

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


                       int qty = int.Parse(r.GetValue(2).ToString()) + int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                   
                       
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














           // PurchaseView rp = new PurchaseView(textBox1.Text);
           // rp.Show();
       

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            this.textBox7.Clear();
            this.textBox8.Clear();
            this.textBox9.Clear();
        }

        
        
        
    }
}
