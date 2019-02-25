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
    public partial class About : Form
    {
        string teacher;
        Thread t = new Thread(Formstart);
        bool imageSelected = false;

        Database db = new Database();
        MySqlConnection dbConnection;

        public About(string teacher)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;

            if (teacher == "Head Teacher")
            {
                editButton.Show();
            }

            String selectQuery = "SELECT * FROM schoolinfo WHERE schoolid = 1";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter mda = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            mda.Fill(table);
            if (table.Rows.Count > 0)
            {
                schoolName.Text = table.Rows[0][1].ToString();
                description.Text = table.Rows[0][3].ToString();
                estd.Text = ": " + table.Rows[0][4].ToString();
                area.Text = ": " + table.Rows[0][5].ToString();

                byte[] img = (byte[])table.Rows[0][2];
                MemoryStream ms = new MemoryStream(img);
                pictureBox2.Image = Image.FromStream(ms);
            }
            mda.Dispose();

            selectQuery = "SELECT * FROM teachers";
            command = new MySqlCommand(selectQuery, dbConnection);
            mda = new MySqlDataAdapter(command);
            table = new DataTable();
            mda.Fill(table);
            teachers.Text = ": " + table.Rows.Count.ToString() + " (Now)";
            mda.Dispose();

            int year = 0;
            selectQuery = "SELECT * FROM years ORDER BY year DESC";
            command = new MySqlCommand(selectQuery, dbConnection);
            mda = new MySqlDataAdapter(command);
            table = new DataTable();
            mda.Fill(table);
            if (table.Rows.Count > 0)
                year = Convert.ToInt32(table.Rows[0][0].ToString());
            mda.Dispose();

            selectQuery = "SELECT * FROM students WHERE year = " + year;
            command = new MySqlCommand(selectQuery, dbConnection);
            mda = new MySqlDataAdapter(command);
            table = new DataTable();
            mda.Fill(table);
            students.Text = ": " + table.Rows.Count.ToString() + " (" + year.ToString() + ")";
            mda.Dispose();
        }
        public About(string teacher, bool edit)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;
            
            viewBox.Hide();
            editBox.Show();
            saveButton.Show();

            String selectQuery = "SELECT * FROM schoolinfo WHERE schoolid = 1";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter mda = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            mda.Fill(table);
            if (table.Rows.Count > 0)
            {
                txtSchoolName.Text = table.Rows[0][1].ToString();
                txtDescription.Text = table.Rows[0][3].ToString();
                txtESTD.Text = table.Rows[0][4].ToString();
                txtArea.Text = table.Rows[0][5].ToString();

                byte[] img = (byte[])table.Rows[0][2];
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);
                imageSelected = true;
            }
            mda.Dispose();
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

        private void EditButton_Click(object sender, EventArgs e)
        {
            About aFobj = new About(teacher, true);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                aFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(txtSchoolName.Text == "")
            {
                MessageBox.Show("Please insert a name");
                txtSchoolName.Focus();
            }
            else if(!imageSelected)
            {
                MessageBox.Show("Please select an image");
                browseButton.Focus();
            }
            else if(txtESTD.Text == "")
            {
                MessageBox.Show("Please enter the establishment year");
                txtESTD.Focus();
            }
            else if (txtArea.Text == "")
            {
                MessageBox.Show("Please enter the area");
                txtArea.Focus();
            }
            else if (txtDescription.Text == "")
            {
                MessageBox.Show("Please write a short description");
                txtDescription.Focus();
            }
            else
            {
                String selectQuery = "SELECT * FROM schoolinfo WHERE schoolid = 1";
                MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
                MySqlDataAdapter mda = new MySqlDataAdapter(command);
                DataTable table = new DataTable();

                mda.Fill(table);
                if (table.Rows.Count < 1)
                {
                    InsertData();
                }
                else
                {
                    UpdateData();
                }
                mda.Dispose();

                About aFobj = new About(teacher);
                t.Start();
                while (t.IsAlive)
                {
                    this.Hide();
                    aFobj.ShowDialog();
                }
                t.Abort();
                this.Close();
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose image (*.JPG;*.PNG;*.GIF|*.jpg;*.png;*.gif)";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                imageSelected = true;
            }
        }

        private void InsertData()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            MySqlCommand command = new MySqlCommand("INSERT INTO schoolinfo (schoolid, schoolname, image, description, estd, area)" +
                                                    " VALUES (@schoolid, @schoolname, @image, @description, @estd, @area)", dbConnection);

            command.Parameters.Add("@schoolid", MySqlDbType.Int32).Value = 1;
            command.Parameters.Add("@schoolname", MySqlDbType.VarChar).Value = txtSchoolName.Text;
            command.Parameters.Add("@description", MySqlDbType.VarChar).Value = txtDescription.Text;
            command.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;
            command.Parameters.Add("@estd", MySqlDbType.Int32).Value = txtESTD.Text;
            command.Parameters.Add("@area", MySqlDbType.VarChar).Value = txtArea.Text;

            dbConnection.Open();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Successfully Saved");
            }
            else
            {
                MessageBox.Show("Unable to Save");
            }

            dbConnection.Close();
        }

        private void UpdateData()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            MySqlCommand command = new MySqlCommand("UPDATE schoolinfo SET schoolname = @schoolname, image = @image, description = @description, estd = @estd, area = @area" +
                                                    " WHERE schoolid = 1", dbConnection);

            command.Parameters.Add("@schoolname", MySqlDbType.VarChar).Value = txtSchoolName.Text;
            command.Parameters.Add("@description", MySqlDbType.VarChar).Value = txtDescription.Text;
            command.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;
            command.Parameters.Add("@estd", MySqlDbType.Int32).Value = txtESTD.Text;
            command.Parameters.Add("@area", MySqlDbType.VarChar).Value = txtArea.Text;

            dbConnection.Open();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Successfully Saved");
            }
            else
            {
                MessageBox.Show("Unable to Save");
            }

            dbConnection.Close();
        }      
    }
}
