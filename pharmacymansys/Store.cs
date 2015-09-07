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
    public partial class Store : Form
    {
        public Store()
        {
            InitializeComponent();
            MedStoreLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PurchaseForm pf = new PurchaseForm();
            pf.Show();
        }



        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);
        DataTable dt;
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
            ManageStock ms = new ManageStock();
            ms.Show();
        }






    }
}
