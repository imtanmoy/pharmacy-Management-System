using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacymansys
{
    public partial class adminControls : Form
    {

        

        public adminControls()
        {
            InitializeComponent();
            timer1.Start();
            var stime = DateTime.Now;
            this.label10.Text = stime.ToString("h:mm:ss tt");
            
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PrintDialog dlg = new PrintDialog();
          //  dlg.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void accountSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminSetting ads = new AdminSetting();

            ads.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
            login lgin = new login();
            lgin.Show();
        }

        private void newStuffRegistratiomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeRegistration emreg = new EmployeRegistration();
            this.Hide();
            emreg.Show();
        }

        private void adminControls_FormClosing(object sender, FormClosingEventArgs e)
        {
          //  Application.Exit();
            login lg = new login();
            lg.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewGrp catl = new NewGrp();
            catl.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMedicineInfo mda = new AddMedicineInfo();
            this.Hide();
            mda.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MedView mdv = new MedView();
           // this.Hide();
            mdv.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MedView mdv = new MedView();
            this.Hide();
            mdv.Show();
        }

        private void addManufactureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddMgf adm = new AddMgf();
            adm.Show();
        }

        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewMgf vmf = new ViewMgf();
            vmf.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            this.label8.Text = time.ToString("h:mm:ss tt");
            this.label9.Text = time.ToLongDateString();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Store str = new Store();
            str.Show();
            this.Hide();
        }

        private void viewEmployesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewStuff vs = new ViewStuff();
            vs.Show();
        }

        

        
    }
}
