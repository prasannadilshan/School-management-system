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
    public partial class Enrollment : Form
    {
        public Enrollment()
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

                    SqlCommand cmd = new SqlCommand("INSERT INTO Enrollmenttbl (StudentId, Section, EDate) VALUES (@StudentId, @Section, @EDate)", conn);

                    cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(cmbStudentId.SelectedValue));
                    cmd.Parameters.AddWithValue("@Section", cmbSectionName.Text);
                    cmd.Parameters.AddWithValue("@EDate", DateTime.Now); 

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Enrollment Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch
            {
                MessageBox.Show("An error occurred while adding the enrollment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    SqlCommand cmd = new SqlCommand("UPDATE Enrollmenttbl SET StudentId = @StudentId, Section = @Section, EDate = @EDate WHERE StudentId = @StudentId", conn);

                    cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(cmbStudentId.SelectedValue));
                    cmd.Parameters.AddWithValue("@Section", cmbSectionName.Text);
                    cmd.Parameters.AddWithValue("@EDate", DateTime.Now); 

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Enrollment Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Display();
                    }
                    else
                    {
                        MessageBox.Show("No record found to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("An error occurred while updating the enrollment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClearFields();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE Enrollmenttbl WHERE StudentId = @StudentId", conn);
                    cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(cmbStudentId.SelectedValue));
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Section Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                Display();
            }
            catch
            {
                MessageBox.Show("An error occurred while deleting the enrollment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           
            ClearFields();
        }
        void ClearFields()
        {
            cmbStudentId.SelectedIndex = -1;
            cmbSectionName.SelectedIndex = -1;
            lblStudentName.Text = "";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {   
           Display();
        }
        private void Display()
        {
            dataGridViewEnroll.DataSource = null;
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Enrollmenttbl", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            conn.Close();

            if (table.Rows.Count > 0)
            {
                dataGridViewEnroll.DataSource = table;
            }
            else
            {
                MessageBox.Show("No records found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        bool isLoading = true;
        private void Enrollment_Load(object sender, EventArgs e)
        {

            isLoading = true;

            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT StudentId,StudentName FROM studenttbl", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbStudentId.DataSource = dt;
                cmbStudentId.DisplayMember = "StudentId";
                cmbStudentId.ValueMember = "StudentId";

                SqlCommand cmd2 = new SqlCommand("SELECT SectionId, Section FROM sectiontbl", conn);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                cmbSectionName.DataSource = dt2;
                cmbSectionName.DisplayMember = "Section";
                cmbSectionName.ValueMember = "Section";
            }
            cmbStudentId.SelectedIndex = -1;

            isLoading = false;
        }

        private void cmbStudentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading || cmbStudentId.SelectedValue == null)
                return;

            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT StudentName FROM studenttbl WHERE StudentId = @id", conn);
                cmd.Parameters.AddWithValue("@id", cmbStudentId.SelectedValue);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblStudentName.Text = reader["StudentName"].ToString();
                }
                else
                {
                    lblStudentName.Text = "Not Found";
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            mainForm.Show();
            this.Hide(); // Hide the current form
        }

        private void Enrollment_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
