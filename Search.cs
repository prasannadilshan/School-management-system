using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace School_management_system
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            
            cmbSelectType.Items.AddRange(new string[] { "Student", "Teacher" ,"Section"});

            
            cmbIdName.Items.AddRange(new string[] { "ID", "Name" });

            
            cmbIdName.SelectedIndex = 0;
            cmbSelectType.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string type = cmbSelectType.SelectedItem.ToString();
            string idName = cmbIdName.SelectedItem.ToString();

            string searchValue = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a value to search.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }else if(type == "Student" && idName == "ID")
            {
                string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT StudentId, StudentName, DoB, Gender, Phone, Email FROM studenttbl WHERE StudentId = @StudentId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentId", int.Parse(searchValue)); 

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) 
                    {
                        lbl1.Text = "Student ID: " + reader["StudentId"].ToString();
                        lbl2.Text = "Name: " + reader["StudentName"].ToString();
                        lbl3.Text = "Date of Birth: " + Convert.ToDateTime(reader["DoB"]).ToShortDateString();
                        lbl4.Text = "Gender: " + reader["Gender"].ToString();
                        lbl5.Text = "Phone: " + reader["Phone"].ToString();
                        lbl6.Text = "Email: " + reader["Email"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No student found with ID: " + searchValue);
                    }
                }

            }
            else if (type == "Student" && idName == "Name")
            {
                string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT StudentId, StudentName, DoB, Gender, Phone, Email FROM studenttbl WHERE StudentName = @StudentName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentName", searchValue); 

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbl1.Text = "Student ID: " + reader["StudentId"].ToString();
                        lbl2.Text = "Name: " + reader["StudentName"].ToString();
                        lbl3.Text = "Date of Birth: " + Convert.ToDateTime(reader["DoB"]).ToShortDateString();
                        lbl4.Text = "Gender: " + reader["Gender"].ToString();
                        lbl5.Text = "Phone: " + reader["Phone"].ToString();
                        lbl6.Text = "Email: " + reader["Email"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No student found with name: " + searchValue);
                    }
                }

            }
            else if (type == "Teacher" && idName == "ID")
            {
                string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT TeacherId, TeacherName, Gender, Phone FROM teachertbl WHERE TeacherId = @TeacherId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeacherId", int.Parse(searchValue));

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbl1.Text = "Teacher ID: " + reader["TeacherId"].ToString();
                        lbl2.Text = "Name: " + reader["TeacherName"].ToString();
                        lbl3.Text = "Gender: " + reader["Gender"].ToString();
                        lbl4.Text = "Phone: " + reader["Phone"].ToString();
                        lbl5.Text = "";
                        lbl6.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("No teacher found with ID: " + searchValue);
                    }
                }
            }
            else if (type == "Teacher" && idName == "Name")
            {
                string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT TeacherId, TeacherName, Gender, Phone FROM teachertbl WHERE TeacherName = @TeacherName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeacherName", searchValue); 

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbl1.Text = "Teacher ID: " + reader["TeacherId"].ToString();
                        lbl2.Text = "Name: " + reader["TeacherName"].ToString();
                        lbl3.Text = "Gender: " + reader["Gender"].ToString();
                        lbl4.Text = "Phone: " + reader["Phone"].ToString();
                        lbl5.Text = "";
                        lbl6.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("No teacher found with name: " + searchValue);
                    }
                }
            }
            else if (type == "Section" && idName == "ID")
            {
                string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT SectionId, Section, TeacherId, RoomNumber, Capacity FROM Sectiontbl WHERE SectionId = @SectionId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SectionId", int.Parse(searchValue));  

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbl1.Text = "Section ID: " + reader["SectionId"].ToString();
                        lbl2.Text = "Section: " + reader["Section"].ToString();
                        lbl3.Text = "Teacher ID: " + reader["TeacherId"].ToString();
                        lbl4.Text = "Room Number: " + reader["RoomNumber"].ToString();
                        lbl5.Text = "Capacity: " + reader["Capacity"].ToString();
                        lbl6.Text = ""; 
                    }
                    else
                    {
                        MessageBox.Show("No section found with ID: " + searchValue);
                    }
                }

            }
            else if (type == "Section" && idName == "Name")
            {
                string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT SectionId, Section, TeacherId, RoomNumber, Capacity FROM Sectiontbl WHERE Section = @Section";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Section", searchValue); 

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbl1.Text = "Section ID: " + reader["SectionId"].ToString();
                        lbl2.Text = "Section: " + reader["Section"].ToString();
                        lbl3.Text = "Teacher ID: " + reader["TeacherId"].ToString();
                        lbl4.Text = "Room Number: " + reader["RoomNumber"].ToString();
                        lbl5.Text = "Capacity: " + reader["Capacity"].ToString();
                        lbl6.Text = ""; // not used
                    }
                    else
                    {
                        MessageBox.Show("No section found with name: " + searchValue);
                    }
                }

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            mainForm.Show();
            this.Hide(); 
        }

        private void Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
