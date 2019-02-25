using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace PSMS
{
    public partial class Students : Form
    {
        string teacher, selectedID = "None";
        Database db = new Database();
        MySqlConnection dbConnection;
        Thread t = new Thread(Formstart);

        public Students(string teacher)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;

            String selectQuery = "Select * FROM years ORDER BY year DESC";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter mda = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            mda.Fill(table);

            for (int i=0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i][0].ToString());
            }
        }

        static void Formstart()
        {

        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            Home hFobj = new Home(teacher);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                hFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void AdmissionButton_Click(object sender, EventArgs e)
        {
            Admission rFobj = new Admission(teacher);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                rFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void ResultButton_Click(object sender, EventArgs e)
        {
            Result_Sheet result_Sheet = new Result_Sheet(teacher);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                result_Sheet.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void Class0Button_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox1.Focus();
            }
            else
            {
                ShowStudents("Class 0");
            }
        }
        
        private void Class1Button_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox1.Focus();
            }
            else
            {
                ShowStudents("Class 1");
            }
        }

        private void Class2Button_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox1.Focus();
            }
            else
            {
                ShowStudents("Class 2");
            }
        }

        private void Class3Button_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox1.Focus();
            }
            else
            {
                ShowStudents("Class 3");
            }
        }

        private void Class4Button_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox1.Focus();
            }
            else
            {
                ShowStudents("Class 4");
            }
        }

        private void Class5Button_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox1.Focus();
            }
            else
            {
                ShowStudents("Class 5");
            }
        }

        private void GraduatedButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox1.Focus();
            }
            else
            {
                ShowStudents("Graduated");
            }
        }

        private void ShowStudents(String s)
        {
            String selectQuery = "SELECT * FROM students WHERE class = '" + s + "' and year = " + comboBox1.Text + " ORDER BY roll ASC";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            DataGridViewImageColumn imgcol = new DataGridViewImageColumn();
            imgcol = (DataGridViewImageColumn)dataGridView1.Columns[8];
            imgcol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            StudentProfile spFobj = new StudentProfile(teacher, selectedID);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                spFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void DataGridView1_Click(object sender, EventArgs e)
        {
            selectedID = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (selectedID != "None")
                viewButton.Show();
            else
                viewButton.Hide();
        }
    }
}
