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

namespace PSMS
{
    public partial class Teachers : Form
    {
        string teacher, selectedTeacher;
        Thread t = new Thread(Formstart);

        Database db = new Database();
        MySqlConnection dbConnection;
        String selectQuery;
        MySqlCommand command;
        MySqlDataAdapter mda;
        DataTable table;

        public Teachers(string teacher)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;           
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

        private void HeadTeacherButton_Click(object sender, EventArgs e)
        {
            SetData(-1);
        }

        private void Teacher1Button_Click(object sender, EventArgs e)
        {
            SetData(0);
        }

        private void Teacher2Button_Click(object sender, EventArgs e)
        {
            SetData(1);
        }

        private void Teacher3Button_Click(object sender, EventArgs e)
        {
            SetData(2);
        }

        private void Teacher4Button_Click(object sender, EventArgs e)
        {
            SetData(3);
        }

        private void Teacher5Button_Click(object sender, EventArgs e)
        {
            SetData(4);
        }

        private void Teacher6Button_Click(object sender, EventArgs e)
        {
            SetData(5);
        }

        private void SetData(int i)
        {
            if(i == -1)
            {
                selectQuery = "SELECT * FROM teachers WHERE designation = 'Head Teacher'";
                i = 0;
            }
            else
                selectQuery = "SELECT * FROM teachers WHERE designation = 'Teacher' ORDER BY priorityValue ASC";

            command = new MySqlCommand(selectQuery, dbConnection);
            mda = new MySqlDataAdapter(command);
            table = new DataTable();

            mda.Fill(table);
            if (table.Rows.Count <= i)
            {
                MessageBox.Show("Not Assained");
            }
            else
            {
                label2.Text = ": " + table.Rows[i][0].ToString();
                label4.Text = ": " + table.Rows[i][8].ToString();
                label6.Text = ": " + table.Rows[i][7].ToString();
                label8.Text = ": " + table.Rows[i][9].ToString();
                label10.Text = ": " + table.Rows[i][4].ToString();
                label12.Text = ": " + table.Rows[i][10].ToString();
                label14.Text = ": " + table.Rows[i][1].ToString();
                label16.Text = ": " + table.Rows[i][2].ToString();
                label18.Text = ": " + table.Rows[i][3].ToString();
                label20.Text = ": " + table.Rows[i][5].ToString();
                label22.Text = ": " + table.Rows[i][6].ToString();

                byte[] img = (byte[])table.Rows[i][13];
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);

                editButton.Hide();
                deleteButton.Hide();
                if (teacher == "Head Teacher")
                {
                    deleteButton.Show();
                    selectedTeacher = table.Rows[i][11].ToString();
                    if (teacher == table.Rows[i][0].ToString())
                    {
                        editButton.Show();
                    }
                }
                else if (teacher == table.Rows[i][11].ToString())
                {
                    editButton.Show();
                }
            }
            mda.Dispose();
        }

        private void Teachers_Load(object sender, EventArgs e)
        {
            SetData(-1);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Registration rFobj = new Registration(teacher, true);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                rFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command2 = new MySqlCommand("DELETE FROM teachers WHERE username = '" + selectedTeacher + "'", dbConnection);

            dbConnection.Open();
            if (command2.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Profile deleted successfully");
            }
            else
            {
                MessageBox.Show("Unable to delete");
            }
            dbConnection.Close();
        }
    }
}
