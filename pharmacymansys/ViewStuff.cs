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
    public partial class ViewStuff : Form
    {
        public ViewStuff()
        {
            InitializeComponent();
            stuffload();
        }

        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);
        DataTable dt;

        void stuffload()
        {
            try
            {
                conn.Open();
                string sqlquery = "SELECT EMP_ID,EMP_NAME,MOBILE,JOIN_DATE,POSITION,SALARY FROM EMPLOYE_INFO";
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
                dt.Columns[2].ColumnName = "Mobile";
                dt.Columns[3].ColumnName = "Joining Date";
                dt.Columns[4].ColumnName = "Position";
                dt.Columns[4].ColumnName = "Salary";

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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string pid = null;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                pid = row.Cells["ID"].Value.ToString();
            }
            StuffInfo si = new StuffInfo(pid);
            si.ShowDialog();
        }


    }
}
