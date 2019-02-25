using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using System.IO;

namespace PSMS
{
    public partial class Home : Form
    {
        int teacherType = 2;
        string teacher = "None";
        Thread t = new Thread(Formstart);

        Database db = new Database();
        MySqlConnection dbConnection;
        MySqlCommand command;
        MySqlDataReader mdr;
        String selectQuery;

        public Home()
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);

            try
            {
                dbConnection.Open();
                dbConnection.Close();
                CheckTables();
                Checking();
            }
            catch
            {
                this.Close();
            }
        }

        private void Checking()
        {
            dbConnection.Open();
            selectQuery = "SELECT * FROM teachers WHERE designation = 'Head Teacher'";
            command = new MySqlCommand(selectQuery, dbConnection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                logInBox.Show();
                textBox1.Focus();
                registrationButton.Hide();
                studentsButton.Hide();
                logOutButton.Hide();
                pictureBox1.Hide();
            }
            else
            {
                studentsButton.Hide();
                logOutButton.Hide();
                teacherType = 1;
                logInBox.Hide();
                registrationButton.Show();
                pictureBox1.Show();
            }
            dbConnection.Close();

            String selectQuery2 = "SELECT * FROM schoolinfo WHERE schoolid = 1";
            MySqlCommand command2 = new MySqlCommand(selectQuery2, dbConnection);
            MySqlDataAdapter mda = new MySqlDataAdapter(command2);
            DataTable table = new DataTable();

            mda.Fill(table);
            if (table.Rows.Count > 0)
            {
                schoolName.Text = table.Rows[0][1].ToString();

                byte[] img = (byte[])table.Rows[0][2];
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);
            }
            mda.Dispose();
        }

        public Home(string teacher)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;
            registrationButton.Hide();
            if (teacher == "None")
            {
                Checking();
                /*logInBox.Show();
                studentsButton.Hide();
                logOutButton.Hide();*/
            }
            else if(teacher == "Head Teacher")
            {
                registrationButton.Show();
            }

            String selectQuery2 = "SELECT * FROM schoolinfo WHERE schoolid = 1";
            MySqlCommand command2 = new MySqlCommand(selectQuery2, dbConnection);
            MySqlDataAdapter mda = new MySqlDataAdapter(command2);
            DataTable table = new DataTable();

            mda.Fill(table);
            if (table.Rows.Count > 0)
            {
                schoolName.Text = table.Rows[0][1].ToString();

                byte[] img = (byte[])table.Rows[0][2];
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);
            }
            mda.Dispose();
        }

        static void Formstart()
        {

        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            dbConnection.Open();
            selectQuery = "SELECT * FROM teachers WHERE username = '" + textBox1.Text + "' and password = '" + textBox2.Text + "'";                    
            command = new MySqlCommand(selectQuery, dbConnection);
            mdr = command.ExecuteReader();

            if (mdr.Read())
            {
                registrationButton.Show();
                studentsButton.Show();
                logOutButton.Show();
                logInBox.Hide();
                pictureBox1.Show();

                teacher = mdr.GetString("designation");
                if(teacher == "Teacher")
                {
                    teacher = mdr.GetString("username");
                    registrationButton.Hide();
                }
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
            dbConnection.Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            About aFobj = new About(teacher);
            t.Start();
            while(t.IsAlive)
            {
                this.Hide();
                aFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            Registration rFobj = new Registration(teacher, teacherType);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                rFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void TeachersButton_Click(object sender, EventArgs e)
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

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            /*registrationButton.Hide();
            studentsButton.Hide();
            logOutButton.Hide();
            logInBox.Show();

            teacher = "None";*/

            Home hFobj = new Home();
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                hFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void CommitteeButton_Click(object sender, EventArgs e)
        {
            Committee cFobj = new Committee(teacher);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                cFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        void Test()
        {
            for (int i = 0; i <= 500; i++)
                Thread.Sleep(10);
        }

        private void DetailsButton_Click(object sender, EventArgs e)
        {
            /*
            using (WaitForm wfFobj = new WaitForm(Test))
            {
                wfFobj.ShowDialog(this);
            }
            */
            Thread t2 = new Thread(Formstart);
            Developer dFobj = new Developer();
            t2.Start();
            while (t2.IsAlive)
            {
                //this.Hide();
                dFobj.ShowDialog();
            }
            t2.Abort();
            
        }

        private void CheckTables()
        {
            CheckSchoolinfo();
            CheckTeachers();
            CheckStudents();
            CheckClasses();
            CheckFirstterm();
            CheckSecondterm();
            CheckFinal();
            CheckYears();
            CheckComittee();
        }

        private void CheckSchoolinfo()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `schoolinfo` (" +
                        " `schoolid` int(11) NOT NULL," +
                        " `schoolname` varchar(500) NOT NULL," +
                        " `image` longblob NOT NULL," +
                        " `description` varchar(5000) NOT NULL," +
                        " `estd` int(11) NOT NULL," +
                        " `area` varchar(100) NOT NULL," +
                        " PRIMARY KEY (schoolid)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Schoolinfo' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void CheckClasses()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `classes` (" +
                        " `id` varchar(50) NOT NULL," +
                        " `class0` int(11) NOT NULL DEFAULT '0'," +
                        " `class1` int(11) NOT NULL DEFAULT '0'," +
                        " `class2` int(11) NOT NULL DEFAULT '0'," +
                        " `class3` int(11) NOT NULL DEFAULT '0'," +
                        " `class4` int(11) NOT NULL DEFAULT '0'," +
                        " `class5` int(11) NOT NULL DEFAULT '0'," +
                        " `graduated` int(11) NOT NULL," +
                        " PRIMARY KEY (id)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Classes' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }       

        private void CheckFirstterm()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `firstterm` (" +
                        " `roll` int(11) NOT NULL," +
                        " `id` varchar(50) NOT NULL," +
                        " `class` varchar(20) NOT NULL," +
                        " `year` int (11) NOT NULL," +
                        " `ban` int (11) NOT NULL DEFAULT '0'," +
                        " `eng` int (11) NOT NULL DEFAULT '0'," +
                        " `mat` int (11) NOT NULL DEFAULT '0'," +
                        " `gs` int (11) NOT NULL DEFAULT '0'," +
                        " `ss` int (11) NOT NULL DEFAULT '0'," +
                        " `rel` int (11) NOT NULL DEFAULT '0'," +
                        " PRIMARY KEY (id, class, year)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Firstterm' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void CheckSecondterm()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `secondterm` (" +
                        " `roll` int(11) NOT NULL," +
                        " `id` varchar(50) NOT NULL," +
                        " `class` varchar(20) NOT NULL," +
                        " `year` int (11) NOT NULL," +
                        " `ban` int (11) NOT NULL DEFAULT '0'," +
                        " `eng` int (11) NOT NULL DEFAULT '0'," +
                        " `mat` int (11) NOT NULL DEFAULT '0'," +
                        " `gs` int (11) NOT NULL DEFAULT '0'," +
                        " `ss` int (11) NOT NULL DEFAULT '0'," +
                        " `rel` int (11) NOT NULL DEFAULT '0'," +
                        " PRIMARY KEY (id, class, year)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Secondterm' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void CheckFinal()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `final` (" +
                        " `roll` int(11) NOT NULL," +
                        " `id` varchar(50) NOT NULL," +
                        " `class` varchar(20) NOT NULL," +
                        " `year` int (11) NOT NULL," +
                        " `ban` int (11) NOT NULL DEFAULT '0'," +
                        " `eng` int (11) NOT NULL DEFAULT '0'," +
                        " `mat` int (11) NOT NULL DEFAULT '0'," +
                        " `gs` int (11) NOT NULL DEFAULT '0'," +
                        " `ss` int (11) NOT NULL DEFAULT '0'," +
                        " `rel` int (11) NOT NULL DEFAULT '0'," +
                        " PRIMARY KEY (id, class, year)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Final' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void CheckStudents()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `students` (" +
                        " `id` varchar(50) NOT NULL," +
                        " `name` varchar(200) NOT NULL," +
                        " `fname` varchar(200) NOT NULL," +
                        " `mname` varchar(200) NOT NULL," +
                        " `phoneNo` varchar(20) NOT NULL," +
                        " `address` varchar(500) NOT NULL," +
                        " `birthDate` varchar(100) NOT NULL," +
                        " `gender` varchar(20) NOT NULL," +
                        " `image` longblob NOT NULL," +
                        " `class` varchar(10) NOT NULL," +
                        " `year` int (11) NOT NULL," +
                        " `roll` int (10) NOT NULL," +
                        " PRIMARY KEY (id)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Students' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void CheckTeachers()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `teachers` (" +
                        " `designation` varchar(30) NOT NULL," +
                        " `name` varchar(200) NOT NULL," +
                        " `fname` varchar(200) NOT NULL," +
                        " `mname` varchar(200) NOT NULL," +
                        " `phoneNo` varchar(20) NOT NULL," +
                        " `presentAddress` varchar(500) NOT NULL," +
                        " `parmanentAddress` varchar(500) NOT NULL," +
                        " `birthDate` varchar(100) NOT NULL," +
                        " `joiningDate` varchar(100) NOT NULL," +
                        " `gender` varchar(20) NOT NULL," +
                        " `email` varchar(100) NOT NULL," +
                        " `username` varchar(100) NOT NULL," +
                        " `password` varchar(100) NOT NULL," +
                        " `image` longblob NOT NULL," +
                        " `priorityValue` int(10) NOT NULL," +
                        " PRIMARY KEY (username)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Teachers' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void CheckYears()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `years` (" +
                        " `year` int(11) NOT NULL," +
                        " PRIMARY KEY (year)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Years' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void CheckComittee()
        {
            selectQuery = "CREATE TABLE IF NOT EXISTS `committee` (" +
                        " `id` int(11) NOT NULL AUTO_INCREMENT," +
                        " `name` varchar(100) NOT NULL," +
                        " `designation` varchar(50) NOT NULL," +
                        " `contact` varchar(20) NOT NULL," +
                        " `address` varchar(500) NOT NULL," +
                        " PRIMARY KEY (id)" +
                        ") ENGINE = InnoDB DEFAULT CHARSET = latin1";
            try
            {
                dbConnection.Open();
                command = new MySqlCommand(selectQuery, dbConnection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("'Committee' Table Creation Unsuccessful");
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
