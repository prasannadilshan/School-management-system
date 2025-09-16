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
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Attendancetbl VALUES (@AId,@StudentName, @Status)", conn);
            cmd.Parameters.AddWithValue("@AId", int.Parse(txtAId.Text));
            cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
            cmd.Parameters.AddWithValue("@Status", txtStatus.Text);

            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Attendance Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


       


        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Attendancetbl SET StudentName = @StudentName ,Status = @Status WHERE AId = @AId", conn);
            cmd.Parameters.AddWithValue("@AId", int.Parse(txtAId.Text));
            cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
            cmd.Parameters.AddWithValue("@Status", txtStatus.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Attendance Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE Attendancetbl WHERE AId = @AId", conn);
            cmd.Parameters.AddWithValue("@AId", int.Parse(txtAId.Text));
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Attendance Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Attendancetbl", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                dataGridViewAttendance.DataSource = table;
            }
            else
            {
                MessageBox.Show("No records found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAId.Clear();
            txtStudentName.Clear();
            txtStatus.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
