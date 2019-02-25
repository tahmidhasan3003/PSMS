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
    public partial class Registration : Form
    {
        int teacherType = 2;
        bool update = false;
        string teacher, uname;
        Thread t = new Thread(Formstart);

        Database db = new Database();
        MySqlConnection dbConnection;

        //String designation = new String("");
        string gender, designation = "Teacher";
        int pv;

        bool imageSelected = false;

        public Registration(string teacher, int teacherType)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;
            this.teacherType = teacherType;
            if (teacherType == 1)
            {
                designation = "Head Teacher";
                designationRadio.Text = "Head Teacher";
            }
        }

        public Registration(string teacher, bool update)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;
            this.update = update;
            if(update)
            {
                importantBox.Hide();
               
                String selectQuery;
                if(teacher == "Head Teacher")
                {
                    selectQuery = "SELECT * FROM teachers WHERE designation = '" + teacher + "'";
                    designationRadio.Text = "Head Teacher";
                }
                else
                {
                    selectQuery = "SELECT * FROM teachers WHERE username = '" + teacher + "'";
                }
                
                MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
                MySqlDataAdapter mda = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                mda.Fill(table);

                textBox1.Text = table.Rows[0][1].ToString();
                textBox2.Text = table.Rows[0][2].ToString();
                textBox3.Text = table.Rows[0][3].ToString();
                textBox4.Text = table.Rows[0][4].ToString();
                textBox5.Text = table.Rows[0][5].ToString();
                textBox6.Text = table.Rows[0][6].ToString();
                textBox7.Text = table.Rows[0][10].ToString();
                dateTimePicker1.Text = table.Rows[0][7].ToString();
                dateTimePicker2.Text = table.Rows[0][8].ToString();
                gender = table.Rows[0][9].ToString();
                uname = table.Rows[0][11].ToString();

                if (gender == "Male")
                {
                    radioButton1.Checked = true;
                }
                else if (gender == "Female")
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton3.Checked = true;
                }

                imageSelected = true;
                byte[] img = (byte[])table.Rows[0][13];
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);
                mda.Dispose();
            }
        }

        static void Formstart()
        {

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Please input your name");
                textBox1.Focus();
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please input your father's name");
                textBox2.Focus();
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Please input your mother's name");
                textBox3.Focus();
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Please input your phone no");
                textBox4.Focus();
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Please input your present address");
                textBox5.Focus();
            }
            else if (textBox6.Text == "")
            {
                MessageBox.Show("Please input your parmanent address");
                textBox6.Focus();
            }
            else if (dateTimePicker1.Text == dateTimePicker2.Text)
            {
                MessageBox.Show("Your birth date or joining date is incorrect");
                dateTimePicker1.Focus();
            }
            else if ((!radioButton1.Checked) && (!radioButton2.Checked) && (!radioButton3.Checked))
            {
                MessageBox.Show("Please select your gender");
            }
            else if (textBox7.Text == "")
            {
                MessageBox.Show("Please input your email address");
                textBox7.Focus();
            }
            else if (textBox8.Text == "" && !update)
            {
                MessageBox.Show("Please input your username");
                textBox8.Focus();
            }
            else if (textBox9.Text == "" && !update)
            {
                MessageBox.Show("Please input your password");
                textBox9.Focus();
            }
            else if (textBox9.Text != textBox10.Text && !update)
            {
                MessageBox.Show("Password not matched");
                textBox10.Focus();
            }
            else if(!imageSelected)
            {
                MessageBox.Show("Select an image");
                chooseImage.Focus();
            }
            else if(!update)
            {
                dbConnection.Open();
                String selectQuery = "SELECT * FROM teachers ORDER BY priorityValue DESC";
                MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
                MySqlDataReader mdr = command.ExecuteReader();
                int x = 0;
                if (mdr.Read())
                {
                    x = mdr.GetInt32("priorityValue");                                                                     
                }
                pv = x + 1;
                dbConnection.Close();
                Insert();
            }
            else if(update)
            {
                UpdateData();
            }
        }

        private void Insert()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            string s = dateTimePicker2.Text;

            MySqlCommand command = new MySqlCommand("INSERT INTO teachers(designation, name, fname, mname, phoneNo, presentAddress, parmanentAddress, birthDate, joiningDate, gender, email, username, password, image, priorityValue)" +
                                                    " VALUES (@designation, @name, @fname, @mname, @phoneNo, @presentAddress, @parmanentAddress, @birthDate, @joiningDate, @gender, @email, @username, @password, @image, @priorityValue)", dbConnection);

            command.Parameters.Add("@designation", MySqlDbType.VarChar).Value = designation;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@mname", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@phoneNo", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@presentAddress", MySqlDbType.VarChar).Value = textBox5.Text;
            command.Parameters.Add("@parmanentAddress", MySqlDbType.VarChar).Value = textBox6.Text;
            command.Parameters.Add("@birthDate", MySqlDbType.VarChar).Value = dateTimePicker1.Text;
            command.Parameters.Add("@joiningDate", MySqlDbType.VarChar).Value = dateTimePicker2.Text;
            command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBox7.Text;
            command.Parameters.Add("@username", MySqlDbType.VarChar).Value = textBox8.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = textBox9.Text;
            command.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;
            command.Parameters.Add("@priorityValue", MySqlDbType.Int32).Value = pv;

            dbConnection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Registration successful");
                if(teacherType == 1)
                {
                    teacher = "Head Teacher";
                }
            }
            else
            {
                MessageBox.Show("Unable");
            }
            dbConnection.Close();
        }

        private void UpdateData()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            string s = dateTimePicker2.Text;

            MySqlCommand command = new MySqlCommand("UPDATE teachers SET name = @name, fname = @fname, mname = @mname, phoneNo = @phoneNo, presentAddress = @presentAddress, parmanentAddress = @parmanentAddress, birthDate = @birthDate, joiningDate = @joiningDate, gender = @gender, email = @email, image = @image" +
                                                    " WHERE username = '" + uname + "'", dbConnection);

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@mname", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@phoneNo", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@presentAddress", MySqlDbType.VarChar).Value = textBox5.Text;
            command.Parameters.Add("@parmanentAddress", MySqlDbType.VarChar).Value = textBox6.Text;
            command.Parameters.Add("@birthDate", MySqlDbType.VarChar).Value = dateTimePicker1.Text;
            command.Parameters.Add("@joiningDate", MySqlDbType.VarChar).Value = dateTimePicker2.Text;
            command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBox7.Text;
            command.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;

            dbConnection.Open();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Successfully Updated");
                dbConnection.Close();
                ViewData();
            }
            else
            {
                MessageBox.Show("Unable to Update");
            }

            dbConnection.Close();
        }

        private void ViewData()
        {
            Teachers tFobj = new Teachers(teacher);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                tFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void ChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose image (*.JPG;*.PNG;*.GIF|*.jpg;*.png;*.gif)";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                imageSelected = true;
            }
        }

        private void Home_Click(object sender, EventArgs e)
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
    }
}
