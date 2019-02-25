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
    public partial class StudentProfile : Form
    {
        string teacher, selectedID;
        int year = 0;
        Database db = new Database();
        MySqlConnection dbConnection;
        Thread t = new Thread(Formstart);

        public StudentProfile(string teacher, string selectedID)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;
            this.selectedID = selectedID;

            SetupData();
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

        private void StudentsButton_Click(object sender, EventArgs e)
        {
            Students sFobj = new Students(teacher);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                sFobj.ShowDialog();
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

        private void SetupData()
        {
            String selectQuery = "SELECT * FROM students WHERE id = '" + selectedID + "'";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            label2.Text = ": " + table.Rows[0][0].ToString();
            label4.Text = ": " + table.Rows[0][10].ToString();
            year = Convert.ToInt32(table.Rows[0][10].ToString());
            label6.Text = ": " + table.Rows[0][9].ToString();
            label8.Text = ": " + table.Rows[0][11].ToString();
            label10.Text = ": " + table.Rows[0][1].ToString();
            label12.Text = ": " + table.Rows[0][2].ToString();
            label14.Text = ": " + table.Rows[0][3].ToString();
            label16.Text = ": " + table.Rows[0][5].ToString();
            label17.Text = ": " + table.Rows[0][6].ToString();
            label19.Text = ": " + table.Rows[0][4].ToString();
            label21.Text = ": " + table.Rows[0][7].ToString();

            byte[] img = (byte[])table.Rows[0][8];
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);

            if (table.Rows[0][9].ToString() != "Graduated")
                MarksheetButton.Hide();

            adapter.Dispose();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            String selectQuery = "DELETE FROM students WHERE id = '" + selectedID + "'";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            String selectQuery2 = "DELETE FROM firstterm WHERE id = '" + selectedID + "'";
            MySqlCommand command2 = new MySqlCommand(selectQuery2, dbConnection);
            String selectQuery3 = "DELETE FROM secondterm WHERE id = '" + selectedID + "'";
            MySqlCommand command3 = new MySqlCommand(selectQuery3, dbConnection);
            String selectQuery4 = "DELETE FROM final WHERE id = '" + selectedID + "'";
            MySqlCommand command4 = new MySqlCommand(selectQuery4, dbConnection);
            String selectQuery5 = "DELETE FROM classes WHERE id = '" + selectedID + "'";
            MySqlCommand command5 = new MySqlCommand(selectQuery5, dbConnection);

            dbConnection.Open();
            try
            {
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                command5.ExecuteNonQuery();

                MessageBox.Show("All information of this student is deleted");
                dbConnection.Close();

                String selectQuery6 = "SELECT * FROM students WHERE year = " + year;
                MySqlCommand command6 = new MySqlCommand(selectQuery6, dbConnection);
                MySqlDataAdapter mda6 = new MySqlDataAdapter(command6);
                DataTable table6 = new DataTable();
                mda6.Fill(table6);
                if(table6.Rows.Count < 1)
                {
                    String selectQuery7 = "DELETE FROM years WHERE year = " + year;
                    MySqlCommand command7 = new MySqlCommand(selectQuery7, dbConnection);
                    dbConnection.Open();
                    command7.ExecuteNonQuery();
                    dbConnection.Close();
                }
                mda6.Dispose();
                StudentsButton_Click(sender, e);
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.ToString());
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void MarksheetButton_Click(object sender, EventArgs e)
        {
            View_Result vrFobj = new View_Result(teacher, "final", selectedID, "Graduated", year.ToString());
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                vrFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Admission rFobj = new Admission(teacher, selectedID);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                rFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }
    }
}
