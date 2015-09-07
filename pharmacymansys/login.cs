using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace pharmacymansys
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
           
           
        }

       


        static string connectionString = "data source=XE;user id=PHARMACY;password=1234";
        OracleConnection conn = new OracleConnection(connectionString);
  

        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.facebook.com/imtanmoybanik");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text) || string.IsNullOrWhiteSpace(this.textBox2.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                if (string.IsNullOrWhiteSpace(this.textBox1.Text)) 
                {
                    MessageBox.Show("Enter User Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.textBox1.Focus();
                }
               else if (string.IsNullOrWhiteSpace(this.textBox2.Text))
                {
                    MessageBox.Show("Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.textBox2.Focus();
                }
                else 
                {
                    MessageBox.Show("Select your Status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                ///////////start of admin log in//////////////////////


                if (comboBox1.SelectedIndex == 0)
                {
                    adminControls adct = new adminControls();
                    try 
                    {
                        conn.Open();
                     
                 
                        String admin_username = textBox1.Text;
                        String pass_word = textBox2.Text;

                        OracleCommand cmd = new OracleCommand("SELECT * FROM ADMIN WHERE ADMIN_USERNAME='" + textBox1.Text + "'", conn);
                     

                        OracleDataReader r = cmd.ExecuteReader();
                        if (r.HasRows)
                        {
                            
                           
                            cmd = new OracleCommand("SELECT PASSWORD FROM ADMIN WHERE ADMIN_USERNAME='" + textBox1.Text + "'", conn);
                            r = cmd.ExecuteReader();
                            r.Read();
                         
                            if (r.GetValue(0).ToString() == pass_word)
                            {
                                LoginName.name = textBox1.Text;
                                adct.Show();
                                this.Hide();
                            
                            }
                            

                        }
                        else
                        {
                            MessageBox.Show("Invalid User Name Or Password", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox2.Focus();
                           
                        }
                        r.Close();
                        conn.Close();
                        
                    }

                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                   
                    
                }

                ///////////End of admin log in//////////////////////



                ///////////start of manager log in//////////////////////


                if (comboBox1.SelectedIndex == 1)
                {
                    managerControls mnct = new managerControls();

                    try
                    {
                        conn.Open();
                       // String admin_username = textBox1.Text;
                       // String pass_word = textBox2.Text;

                        OracleCommand cmd = new OracleCommand("SELECT * FROM MANAGER_LOGIN WHERE U_NAME='" + textBox1.Text + "'", conn);
                        OracleDataReader r = cmd.ExecuteReader();
                        if (r.HasRows)
                        {

                            // MessageBox.Show();
                            cmd = new OracleCommand("SELECT PASSWORD FROM MANAGER_LOGIN WHERE U_NAME='" + textBox1.Text + "'", conn);
                            r = cmd.ExecuteReader();
                            r.Read();
                             // MessageBox.Show(r.GetString(0));
                            if (r.GetValue(0).ToString() == textBox2.Text)
                            {
                                
                                //  MessageBox.Show("wELCOM");
                                LoginName.name = textBox1.Text;
                                mnct.Show();
                                this.Hide();

                            }

                        }
                        else
                        {
                            MessageBox.Show("Invalid User Name Or Password", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        r.Close();
                        conn.Close();

                    }

                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                    
                    
                }

                ///////////End of manager log in//////////////////////

                //////////start of stuff log in/////////////////////

                if (comboBox1.SelectedIndex == 2)
                {
                    stuffControls stct = new stuffControls();

                    try
                    {
                        conn.Open();
                        // String admin_username = textBox1.Text;
                        // String pass_word = textBox2.Text;

                        OracleCommand cmd = new OracleCommand("SELECT * FROM STUFF_LOGIN WHERE U_NAME='" + textBox1.Text + "'", conn);
                        OracleDataReader r = cmd.ExecuteReader();
                        if (r.HasRows)
                        {

                            // MessageBox.Show();
                            cmd = new OracleCommand("SELECT PASSWORD FROM STUFF_LOGIN WHERE U_NAME='" + textBox1.Text + "'", conn);
                            r = cmd.ExecuteReader();
                            r.Read();
                            // MessageBox.Show(r.GetString(0));
                            if (r.GetValue(0).ToString() == textBox2.Text)
                            {
                                
                                //  MessageBox.Show("wELCOM");
                                LoginName.name = textBox1.Text;
                                stct.Show();
                                this.Hide();

                            }

                        }
                        else
                        {
                            MessageBox.Show("Invalid User Name Or Password", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        r.Close();
                        conn.Close();

                    }

                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }


                    
                }
                
                ///////////End of stuff log in//////////////////////
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utilities.ResetAllControls(this);
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
