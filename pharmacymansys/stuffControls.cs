using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacymansys
{
    public partial class stuffControls : Form
    {
        public stuffControls()
        {
            InitializeComponent();
            MedStoreLoad();
        }

        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);
        DataTable dt;




        private void button1_Click(object sender, EventArgs e)
        {
            SellForm sf = new SellForm();
            sf.Show();
        }


        void MedStoreLoad()
        {
            try
            {
                conn.Open();
                string sqlquery = "SELECT * FROM MED_STORE";
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
                dt.Columns[2].ColumnName = "Quantity";
                dt.Columns[3].ColumnName = "Damage Qty";
                dt.Columns[4].ColumnName = "Reorder Level";

                dt.AcceptChanges();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("Name LIKE '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MedView md = new MedView();
            md.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewMgf vm = new ViewMgf();
            vm.Show();
        }

        private void stuffControls_FormClosing(object sender, FormClosingEventArgs e)
        {
            login lg = new login();
            lg.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MedStoreLoad();
            this.dataGridView1.Update();
            this.dataGridView1.Refresh();

            
        }


    }
}
