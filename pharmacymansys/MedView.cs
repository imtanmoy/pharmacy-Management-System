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
    public partial class MedView : Form
    {
        public MedView()
        {
            InitializeComponent();
            MedLoad();
        }

        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);
        DataTable dt;
        void MedLoad()
        {
            try
            {
                conn.Open();
                string sqlquery = "SELECT MED_ID,MED_NAME,MED_STG,MED_MGF,MED_BATCH,MED_GROUP,MED_TYPE,COST_PRICE,SELL_PRICE,NOTES FROM MED_INFO";
                //string sqlquery = "SELECT * FROM MED_INFO";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                dt = new DataTable();
           //     dt.Columns["MED_NAME"].ColumnName = "Name";
              
                oda.Fill(dt);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.DataSource = bsource;
                oda.Update(dt);
                dt.Columns[0].ColumnName = "ID";
                dt.Columns[1].ColumnName = "Name";
                dt.Columns[2].ColumnName = "Strenght";
                dt.Columns[3].ColumnName = "Manufacturer";
                dt.Columns[4].ColumnName = "Batch No";
                dt.Columns[5].ColumnName = "Group";
                dt.Columns[6].ColumnName = "Type";
                dt.Columns[7].ColumnName = "Cost Price";
                dt.Columns[8].ColumnName = "Sell Price";
                dt.Columns[9].ColumnName = "Note";
                dt.AcceptChanges();
                
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MedLoad();
            this.dataGridView1.Update();
            this.dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string pid=null;
            if(e.RowIndex>=0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                pid = row.Cells["ID"].Value.ToString();
            }
            MedInfoView miv = new MedInfoView(pid);
            miv.ShowDialog();
        }

        


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("Name LIKE '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv;
            
        }
        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("Group LIKE '%{0}%'", textBox2.Text);
            dataGridView1.DataSource = dv;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("Manufacturer LIKE '%{0}%'", textBox3.Text);
            dataGridView1.DataSource = dv;
        }

        private void MedView_FormClosing(object sender, FormClosingEventArgs e)
        {
            //adminControls ads = new adminControls();
            this.Hide();
           // ads.Show();
        }
    }
}
