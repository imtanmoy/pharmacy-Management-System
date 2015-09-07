using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pharmacymansys
{
    class MedIDgen
    {
        public static string IdGenrator(string mgf) {


            string connectionString = "data source=XE;user id=PHARMACY;password=1234";
            OracleConnection conn = new OracleConnection(connectionString);
          //  string mgfnm = mgf;
            string ReturnId = null;
            int rowno = 0;
            try
            {
                conn.Open();
                string sqlquery = "SELECT * FROM MED_INFO WHERE MED_MGF='"+mgf+"'";
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
                

            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
          //  string mid = rowno.ToString();
            
            if(ReturnId.Length==1)
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
            string newid = mgf.Substring(0, 3)+ReturnId;

            return newid;
        }
    }
}
