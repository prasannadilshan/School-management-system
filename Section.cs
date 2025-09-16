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
    public partial class Section : Form
    {
        public Section()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Sectiontbl (SectionId, Section, TeacherId, RoomNumber, Capacity) VALUES (@SectionId, @Section, @TeacherId, @RoomNumber, @Capacity)", conn);

                cmd.Parameters.AddWithValue("@SectionId", int.Parse(txtSectionId.Text));
                cmd.Parameters.AddWithValue("@Section", txtSection.Text);
                cmd.Parameters.AddWithValue("@TeacherId", int.Parse(cmbTeacherId.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@RoomNumber", txtRoomNumber.Text);
                cmd.Parameters.AddWithValue("@Capacity", int.Parse(txtCapacity.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Section Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                Display();
            }
            catch
            {
                MessageBox.Show("An error occurred while adding the section. Please check your input and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Sectiontbl 
                         SET Section = @Section, 
                             TeacherId = @TeacherId, 
                             RoomNumber = @RoomNumber, 
                             Capacity = @Capacity 
                         WHERE SectionId = @SectionId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@SectionId", int.Parse(txtSectionId.Text));
                cmd.Parameters.AddWithValue("@Section", txtSection.Text);
                cmd.Parameters.AddWithValue("@TeacherId", int.Parse(cmbTeacherId.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@RoomNumber", txtRoomNumber.Text);
                cmd.Parameters.AddWithValue("@Capacity", int.Parse(txtCapacity.Text));

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Section updated successfully!");
                        Display();
                    }
                    else
                    {
                        MessageBox.Show("No record found with the given SectionId.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            clearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE Sectiontbl WHERE SectionId = @SectionId", conn);
                cmd.Parameters.AddWithValue("@SectionId", int.Parse(txtSectionId.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Section Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                Display();
            }
            catch
            {
                MessageBox.Show("An error occurred while deleting the section. Please check your input and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }
        public void clearFields()
        {
            txtSection.Clear();
            txtSectionId.Clear();
            txtCapacity.Clear();
            txtRoomNumber.Clear();
            cmbTeacherId.SelectedIndex = -1; 
            lblTeacherName.Text = ""; 
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
           Display();
        }
        private void Display()
        {
            clearFields();
            dataGridViewSection.DataSource = null;

            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sectiontbl", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            conn.Close();

            if (table.Rows.Count > 0)
            {
                dataGridViewSection.DataSource = table;
            }
            else
            {
                MessageBox.Show("No records found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtStudentName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtSection_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSectionId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
        bool isLoading = true;

        private void Section_Load(object sender, EventArgs e)
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
            }
            cmbTeacherId.SelectedIndex = -1;

            isLoading = false;
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

        private void Section_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridViewSection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

