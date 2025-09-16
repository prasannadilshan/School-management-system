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
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO teachertbl VALUES (@TeacherId,@TeacherName,@Gender,@Phone)", conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherId", int.Parse(txtTeacherId.Text));
                        cmd.Parameters.AddWithValue("@TeacherName", txtTeacherName.Text);
                        string Gender = "";
                        if (rbFemale.Checked)
                        {
                            Gender = "Female";
                        }
                        else if (rbMale.Checked)
                        {
                            Gender = "Male";
                        }
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@Phone", int.Parse(txtPhone.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Teacher Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
            }
            catch
            {
                MessageBox.Show("An error occurred while adding the teacher. Please check your input and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE teachertbl SET TeacherName = @TeacherName ,Gender=@Gender ,Phone = @Phone WHERE TeacherId = @TeacherId", conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherId", int.Parse(txtTeacherId.Text));
                        cmd.Parameters.AddWithValue("@TeacherName", txtTeacherName.Text);
                        string Gender = "";
                        if (rbFemale.Checked)
                        {
                            Gender = "Female";
                        }
                        else if (rbMale.Checked)
                        {
                            Gender = "Male";
                        }
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@Phone", int.Parse(txtPhone.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Teacher Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                Display();
            }
            catch
            {
                MessageBox.Show("An error occurred while updating the teacher. Please check your input and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE teachertbl WHERE TeacherId = @TeacherId", conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherId", int.Parse(txtTeacherId.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Teacher Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                Display();
            }
            catch
            {
                MessageBox.Show("An error occurred while deleting the teacher. Please check your input and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }
        public void clearFields()
        {
            txtTeacherId.Clear();
            txtTeacherName.Clear();
            rbFemale.Checked = false;
            rbMale.Checked = false;
            txtPhone.Clear();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Display();
        }
        
        private void Display()
        {
            clearFields();
            dataGridViewTeacher.DataSource = null; 
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM teachertbl", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            conn.Close();

            if (table.Rows.Count > 0)
            {
                dataGridViewTeacher.DataSource = table;
            }
            else
            {
                MessageBox.Show("No records found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            mainForm.Show();
            this.Hide(); 
        }

        private void Teacher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
