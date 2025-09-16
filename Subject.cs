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
    public partial class Subject : Form
    {
        public Subject()
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

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Subtbl (SubjectId, SubjectName, SubjectCode, TeacherId, SectionName) VALUES (@SubjectId, @SubjectName, @SubjectCode, @TeacherId, @SectionName)", conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectId", int.Parse(txtSubId.Text));
                        cmd.Parameters.AddWithValue("@SubjectName", txtSubjectName.Text);
                        cmd.Parameters.AddWithValue("@SubjectCode", txtSubjectCode.Text);
                        cmd.Parameters.AddWithValue("@TeacherId", int.Parse(cmbTeacherId.SelectedValue.ToString()));
                        cmd.Parameters.AddWithValue("@SectionName", cmbSectionName.Text); 

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Subject Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
           clearFields();
        }

       
        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE Subtbl SET SubjectName = @SubjectName, SubjectCode = @SubjectCode, TeacherId = @TeacherId, SectionName = @SectionName WHERE SubjectId = @SubjectId", conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectName", txtSubjectName.Text);
                        cmd.Parameters.AddWithValue("@SubjectCode", txtSubjectCode.Text);
                        cmd.Parameters.AddWithValue("@TeacherId", int.Parse(cmbTeacherId.SelectedValue.ToString()));
                        cmd.Parameters.AddWithValue("@SectionName", cmbSectionName.Text); 
                        cmd.Parameters.AddWithValue("@SubjectId", int.Parse(txtSubId.Text));

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Subject Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE Subtbl WHERE SubjectId = @SubjectId", conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectId", int.Parse(txtSubId.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Subject Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void Display()
        {
            dataGridViewSub.DataSource = null;
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Subtbl", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            conn.Close();

            if (table.Rows.Count > 0)
            {
                dataGridViewSub.DataSource = table;
            }
            else
            {
                MessageBox.Show("No records found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        bool isLoading = true;
        private void Subject_Load(object sender, EventArgs e)
        {

            isLoading = true;

            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TeacherId FROM teachertbl", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbTeacherId.DataSource = dt;
                cmbTeacherId.DisplayMember = "TeacherId";
                cmbTeacherId.ValueMember = "TeacherId";

                SqlCommand cmd2 = new SqlCommand("SELECT SectionId, Section FROM sectiontbl", conn);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                cmbSectionName.DataSource = dt2;
                cmbSectionName.DisplayMember = "Section"; 
                cmbSectionName.ValueMember = "Section";  
            }
            cmbTeacherId.SelectedIndex = -1;
            cmbSectionName.SelectedIndex = -1;
            lblTeacherName.Text = "";

            isLoading = false;
        }
        private void clearFields()
        {
            txtSubId.Clear();
            txtSubjectCode.Clear();
            txtSubjectName.Clear();
            cmbTeacherId.SelectedIndex = -1;
            lblTeacherName.Text = "";
            cmbSectionName.SelectedIndex = -1;
        }

        private void cmbTeacherId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading || cmbTeacherId.SelectedValue == null)
                return;

            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TeacherName FROM teachertbl WHERE TeacherId = @id", conn);
                cmd.Parameters.AddWithValue("@id", cmbTeacherId.SelectedValue);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblTeacherName.Text = reader["TeacherName"].ToString();
                }
                else
                {
                    lblTeacherName.Text = "Not Found";
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            mainForm.Show();
            this.Hide();
        }

        private void Subject_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txtSubId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
