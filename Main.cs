using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_management_system
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dashboard dashboardForm = new Dashboard();
            dashboardForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student studentForm = new Student();
            studentForm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Subject subjectForm = new Subject();
            subjectForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Teacher teacherForm = new Teacher();
            teacherForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Section sectionForm = new Section();
            sectionForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Enrollment enrollmentForm = new Enrollment();
            enrollmentForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Search searchForm = new Search();
            searchForm.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Hide();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
