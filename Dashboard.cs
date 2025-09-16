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
using Microsoft.Reporting.WinForms;


namespace School_management_system
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            List<DashboardChartModel> chartData = new List<DashboardChartModel>();

            string connectionString = @"Data Source=PRASANNA\MSSQLSERVER01;Initial Catalog=schooldb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd;

                // Students
                cmd = new SqlCommand("SELECT COUNT(*) FROM studenttbl", conn);
                int countStudents = (int)cmd.ExecuteScalar();
                lblStudents.Text = countStudents.ToString();
                chartData.Add(new DashboardChartModel { Category = "Students", Count = countStudents });

                // Subjects
                cmd = new SqlCommand("SELECT COUNT(*) FROM Subtbl", conn);
                int countSubjects = (int)cmd.ExecuteScalar();
                lblSubjects.Text = countSubjects.ToString();
                chartData.Add(new DashboardChartModel { Category = "Subjects", Count = countSubjects });

                cmd = new SqlCommand("SELECT COUNT(*) FROM Enrollmenttbl", conn);
                int countEnrolment = (int)cmd.ExecuteScalar();
                lblEnrollment.Text = countEnrolment.ToString();



                // Teachers
                cmd = new SqlCommand("SELECT COUNT(*) FROM teachertbl", conn);
                int countTeachers = (int)cmd.ExecuteScalar();
                lblTeachers.Text = countTeachers.ToString();
                chartData.Add(new DashboardChartModel { Category = "Teachers", Count = countTeachers });
            }

           
            ReportDataSource rds = new ReportDataSource("DataSet2", chartData); 


            reportViewer1.LocalReport.ReportEmbeddedResource = "School_management_system.DashboardChart.rdlc"; 

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            mainForm.Show();
            this.Hide();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
