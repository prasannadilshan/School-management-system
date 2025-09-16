using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace School_management_system
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";


        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Username,Password FROM Logintbl WHERE Username = '" + username + "' and Password = '" + password + "'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            

            if(dt.Rows.Count > 0)
            {
                Main main = new Main();
                main.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
            conn.Close();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
