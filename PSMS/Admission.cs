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
    public partial class Admission : Form
    {
        int roll;
        string teacher, selectedID = "None", newID;
        Thread t = new Thread(Formstart);

        Database db = new Database();
        MySqlConnection dbConnection;

        bool imageSelected = false;
        string gender = "";

        public Admission(string teacher)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;

            String selectQuery = "Select * FROM years ORDER BY year DESC";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter mda = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            mda.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox2.Items.Add(table.Rows[i][0].ToString());
            }
        }

        public Admission(string teacher, string selectedID)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;
            this.selectedID = selectedID;

            String selectQuery = "SELECT * FROM students WHERE id = '" + selectedID + "'";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            textBox1.Text = table.Rows[0][1].ToString();
            textBox2.Text = table.Rows[0][2].ToString();
            textBox3.Text = table.Rows[0][3].ToString();
            textBox4.Text = table.Rows[0][5].ToString();
            dateTimePicker1.Text = table.Rows[0][6].ToString();
            textBox5.Text = table.Rows[0][4].ToString();
            gender = table.Rows[0][7].ToString();

            if (gender == "Male")
                radioButton1.Checked = true;
            else if (gender == "Female")
                radioButton2.Checked = true;
            else if (gender == "Others")
                radioButton3.Checked = true;

            byte[] img = (byte[])table.Rows[0][8];
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
            imageSelected = true;

            insertButton.Text = "Update";
            groupBox1.Hide();
        }

        static void Formstart()
        {

        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose image (*.JPG;*.PNG;*.GIF|*.jpg;*.png;*.gif)";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                imageSelected = true;
            }
        }

        private void InsertUpdateButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please input a name");
                textBox1.Focus();
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please input father's name");
                textBox2.Focus();
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Please input mother's name");
                textBox3.Focus();
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Please input an address");
                textBox4.Focus();
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Please input a phone no");
                textBox5.Focus();
            }
            else if (dateTimePicker1.Text == "")
            {
                MessageBox.Show("Please input the date of birth");
                dateTimePicker1.Focus();
            }
            else if ((!radioButton1.Checked) && (!radioButton2.Checked) && (!radioButton3.Checked))
            {
                MessageBox.Show("Please select the gender");
            }
            else if (!imageSelected)
            {
                MessageBox.Show("Please select an image");
                chooseButton.Focus();
            }
            else if (comboBox1.Text == "" && selectedID == "None")
            {
                MessageBox.Show("Please select a class");
                comboBox1.Focus();
            }
            else if (comboBox2.Text == "" && selectedID == "None")
            {
                MessageBox.Show("Please input a year");
                comboBox2.Focus();
            }
            else
            {
                if (selectedID == "None")
                    Insert_Click();
                else
                    Update_Click();
            }            
        }

        private void Insert_Click()
        {
            DateTime dateTime = DateTime.Now;
            newID = dateTime.ToString();

            dbConnection.Open();
            String selectQuery0 = "SELECT * FROM students WHERE id = '" + newID + "'";
            MySqlCommand command0 = new MySqlCommand(selectQuery0, dbConnection);
            MySqlDataReader mdr0 = command0.ExecuteReader();

            if (mdr0.Read())
            {
                MessageBox.Show("Please try again...");
                dbConnection.Close();
            }
            else
            {
                dbConnection.Close();

                dbConnection.Open();
                String selectQuery = "SELECT * FROM students WHERE class = '" + comboBox1.Text + "' && year = " + comboBox2.Text + " ORDER BY roll DESC";
                MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
                MySqlDataReader mdr = command.ExecuteReader();

                if (mdr.Read())
                {
                    int x = mdr.GetInt32("roll");
                    roll = x + 1;
                }
                else
                {
                    roll = 1;
                }
                dbConnection.Close();
                Insert();
            }
        }

        private void Insert()
        {
            String newClassFormated = "Error";
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            MySqlCommand command = new MySqlCommand("INSERT INTO students(id, name, fname, mname, phoneNo, address, birthDate, gender, image, class, year, roll)" +
                                                    " VALUES (@id, @name, @fname, @mname, @phoneNo, @address, @birthDate, @gender, @image, @class, @year, @roll)", dbConnection);

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = newID;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@mname", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@phoneNo", MySqlDbType.VarChar).Value = textBox5.Text;
            command.Parameters.Add("@address", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@birthDate", MySqlDbType.VarChar).Value = dateTimePicker1.Text;
            command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;
            command.Parameters.Add("@class", MySqlDbType.VarChar).Value = comboBox1.Text;
            command.Parameters.Add("@year", MySqlDbType.Int32).Value = comboBox2.Text;
            command.Parameters.Add("@roll", MySqlDbType.Int32).Value = roll;

            if (comboBox1.Text == "Class 0")
            {
                newClassFormated = "class0";
            }
            else if (comboBox1.Text == "Class 1")
            {
                newClassFormated = "class1";
            }
            else if (comboBox1.Text == "Class 2")
            {
                newClassFormated = "class2";
            }
            else if (comboBox1.Text == "Class 3")
            {
                newClassFormated = "class3";
            }
            else if (comboBox1.Text == "Class 4")
            {
                newClassFormated = "class4";
            }
            else if (comboBox1.Text == "Class 5")
            {
                newClassFormated = "class5";
            }
            String selectQuery2 = "INSERT INTO classes(id, " + newClassFormated + ") VALUES ('" + newID + "', " + comboBox2.Text + ")";
            MySqlCommand command2 = new MySqlCommand(selectQuery2, dbConnection);

            MySqlCommand command3 = new MySqlCommand("INSERT INTO firstterm(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
            command3.Parameters.Add("@roll", MySqlDbType.Int32).Value = roll;
            command3.Parameters.Add("@id", MySqlDbType.VarChar).Value = newID;
            command3.Parameters.Add("@class", MySqlDbType.VarChar).Value = comboBox1.Text;
            command3.Parameters.Add("@year", MySqlDbType.Int32).Value = comboBox2.Text;

            MySqlCommand command4 = new MySqlCommand("INSERT INTO secondterm(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
            command4.Parameters.Add("@roll", MySqlDbType.Int32).Value = roll;
            command4.Parameters.Add("@id", MySqlDbType.VarChar).Value = newID;
            command4.Parameters.Add("@class", MySqlDbType.VarChar).Value = comboBox1.Text;
            command4.Parameters.Add("@year", MySqlDbType.Int32).Value = comboBox2.Text;

            MySqlCommand command5 = new MySqlCommand("INSERT INTO final(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
            command5.Parameters.Add("@roll", MySqlDbType.Int32).Value = roll;
            command5.Parameters.Add("@id", MySqlDbType.VarChar).Value = newID;
            command5.Parameters.Add("@class", MySqlDbType.VarChar).Value = comboBox1.Text;
            command5.Parameters.Add("@year", MySqlDbType.Int32).Value = comboBox2.Text;

            dbConnection.Open();
            String selectQuery6 = "Select * FROM years WHERE year = " + comboBox2.Text;
            MySqlCommand command6 = new MySqlCommand(selectQuery6, dbConnection);
            MySqlDataReader mdr = command6.ExecuteReader();

            if(mdr.Read())
            {
                dbConnection.Close();
                dbConnection.Open();
                if (command.ExecuteNonQuery() == 1 && command2.ExecuteNonQuery() == 1 && command3.ExecuteNonQuery() == 1 && command4.ExecuteNonQuery() == 1 && command5.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Admission successful");
                }
                else
                {
                    MessageBox.Show("Failed");
                }
            }
            else
            {
                MySqlCommand command7 = new MySqlCommand("INSERT INTO years(year) VALUES (@year)", dbConnection);
                command7.Parameters.Add("@year", MySqlDbType.Int32).Value = comboBox2.Text;

                dbConnection.Close();
                dbConnection.Open();
                if (command.ExecuteNonQuery() == 1 && command2.ExecuteNonQuery() == 1 && command3.ExecuteNonQuery() == 1 && command4.ExecuteNonQuery() == 1 && command5.ExecuteNonQuery() == 1 && command7.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Admission successful");
                    comboBox2.Items.Add(comboBox2.Text.ToString());
                }
                else
                {
                    MessageBox.Show("Failed");
                }
            }           
            dbConnection.Close();            
        }

        private void Update_Click()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            String selectQuery = "UPDATE students SET name = '" + textBox1.Text + "', fname = '" + textBox2.Text + "', mname = '" + textBox3.Text + "', phoneNo = '" + textBox5.Text + "', address = '" + textBox4.Text + "', birthDate = '" + dateTimePicker1.Text + "', gender = '" + gender + "', image = @image" +
                                " WHERE id = '" + selectedID + "'";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            command.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;

            dbConnection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Profile Updated Successfully");
                ViewProfile();
            }
            dbConnection.Close();
        }

        private void ViewProfile()
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

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Others";
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
    }
}
