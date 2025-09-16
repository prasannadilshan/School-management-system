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
    public partial class Student : Form
    {
        private DataGridView dataGridView1; 

        public Student()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO studenttbl VALUES (@StudentID,@StudentName,  @Gender, @Phone,@Email,@DoB)", conn);
           
            if (string.IsNullOrWhiteSpace(txtStuId.Text) ||
                string.IsNullOrWhiteSpace(txtStudName.Text) ||
                (!rbFemale.Checked && !rbMale.Checked) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cmd.Parameters.AddWithValue("@StudentID", int.Parse(txtStuId.Text));
            cmd.Parameters.AddWithValue("@StudentName", txtStudName.Text);
            cmd.Parameters.Add("@DoB", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
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
            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Student Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtStuId.Clear();
            txtStudName.Clear();
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            rbFemale.Checked = false;
            rbMale.Checked = false;
            txtPhone.Clear();
            txtEmail.Clear();
            Display();
        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                dateTimePicker1.CustomFormat = " ";
            }
            else if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            }

        }



        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE studenttbl SET StudentName = @StudentName, DoB = @DoB, Gender = @Gender, Phone = @Phone, Email = @Email WHERE StudentID = @StudentID", conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", int.Parse(txtStuId.Text));
                        cmd.Parameters.AddWithValue("@StudentName", txtStudName.Text);
                        cmd.Parameters.AddWithValue("@DoB", dateTimePicker1.Value);
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
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        if (string.IsNullOrWhiteSpace(txtStuId.Text) ||
                            string.IsNullOrWhiteSpace(txtStudName.Text) ||
                            (!rbFemale.Checked && !rbMale.Checked) ||
                            string.IsNullOrWhiteSpace(txtPhone.Text) ||
                            string.IsNullOrWhiteSpace(txtEmail.Text))
                        {
                            MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Student Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Display();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid Student ID format. Please enter a valid number.\n" + ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A database error occurred:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    using (SqlCommand cmd = new SqlCommand("DELETE studenttbl WHERE StudentID = @StudentID", conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", int.Parse(txtStuId.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Student Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStuId.Clear();
                Display();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid Student ID format. Please enter a valid number.\n" + ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A database error occurred:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtStuId.Clear();
            txtStudName.Clear();
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            rbFemale.Checked = false;
            rbMale.Checked = false;
            txtPhone.Clear();
            txtEmail.Clear();
            dataGridViewStu.DataSource = null; 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Display();
        }

        public void Display()
        {

            dataGridViewStu.DataSource = null; 
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM studenttbl", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            conn.Close();

            if (table.Rows.Count > 0)
            {
                dataGridViewStu.DataSource = table;
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

        private void Student_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
