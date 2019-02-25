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
    public partial class View_Result : Form
    {
        string teacher, tbname, id, classes, year;

        Thread t = new Thread(Formstart);

        Database db = new Database();
        MySqlConnection dbConnection;
        String selectQuery;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;

        public View_Result(string teacher, string tbname, string id, string classes, string year)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;
            this.tbname = tbname;
            this.id = id;
            this.classes = classes;
            this.year = year;
            gradeRadio.Checked = true;

            String selectQuery = "SELECT * FROM schoolinfo WHERE schoolid = 1";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter mda = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            mda.Fill(table);
            if (table.Rows.Count > 0)
            {
                schoolName.Text = table.Rows[0][1].ToString();
            }
            mda.Dispose();

            Setup();
            ShowResult();          
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

        private void Setup()
        {
            selectQuery = "SELECT * FROM students WHERE id = '" + id + "' ORDER BY roll ASC";
            command = new MySqlCommand(selectQuery, dbConnection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            if(table.Rows.Count>0)
            {
                nameLabel2.Text = table.Rows[0][1].ToString();
                fnameLabel2.Text = table.Rows[0][2].ToString();
                mnameLabel2.Text = table.Rows[0][3].ToString();
                dobLabel2.Text = table.Rows[0][6].ToString();

                byte[] img = (byte[])table.Rows[0][8];
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);
            }
            adapter.Dispose();
        }

        private void ShowResult()
        {
            double points = 0, totalPoints = 0, gpa = 0;
            string grade = "F", tempClass = classes;

            if(tbname == "firstterm")
            {
                examLabel2.Text = "1st Term";
            }
            else if (tbname == "secondterm")
            {
                examLabel2.Text = "2nd Term";
            }
            else if (tbname == "final")
            {
                examLabel2.Text = "Final";
            }

            if (classes == "Graduated")
                tempClass = "Class 5";

            selectQuery = "SELECT * FROM " + tbname + " WHERE id = '" + id + "' and class = '" + tempClass + "' and year = " + year;
            command = new MySqlCommand(selectQuery, dbConnection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);

            classLabel2.Text = table.Rows[0][2].ToString();
            if (classes == "Graduated")
                classLabel2.Text = "Graduated";

            yearLabel2.Text = table.Rows[0][3].ToString();
            rollLabel2.Text = table.Rows[0][0].ToString();

            for (int i = 4; i < 10; i++)
            {
                int marks = 0;
                marks = Convert.ToInt32(table.Rows[0][i].ToString());
                points = GetPoints(marks);
                totalPoints += points;

                if(gradeRadio.Checked)
                {
                    grade = GetGrade(points);

                    switch (i)
                    {
                        case 4:
                            banglaLabel2.Text = grade;
                            break;
                        case 5:
                            englishLabel2.Text = grade;
                            break;
                        case 6:
                            mathLabel2.Text = grade;
                            break;
                        case 7:
                            gsLabel2.Text = grade;
                            break;
                        case 8:
                            ssLabel2.Text = grade;
                            break;
                        case 9:
                            relLabel2.Text = grade;
                            break;
                    }
                }
                else if(marksRadio.Checked)
                {
                    switch (i)
                    {
                        case 4:
                            banglaLabel2.Text = marks.ToString();
                            break;
                        case 5:
                            englishLabel2.Text = marks.ToString();
                            break;
                        case 6:
                            mathLabel2.Text = marks.ToString();
                            break;
                        case 7:
                            gsLabel2.Text = marks.ToString();
                            break;
                        case 8:
                            ssLabel2.Text = marks.ToString();
                            break;
                        case 9:
                            relLabel2.Text = marks.ToString();
                            break;
                    }
                }                          
            }

            if (table.Rows[0][2].ToString() == "Class 0" || table.Rows[0][2].ToString() == "Class 1" || table.Rows[0][2].ToString() == "Class 2")
            {
                gpa = totalPoints / 3;
                groupBox2.Hide();
            }
            else
            {
                gpa = totalPoints / 6;
            }

            gpaLabel2.Text = gpa.ToString(".00");
            adapter.Dispose();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if(printDialog1.ShowDialog() == DialogResult.OK)
            {                
                printDocument1.Print();
            }
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {           
            string basic = nameLabel.Text + "\t\t: " + nameLabel2.Text + "\n";
            basic += fnameLabel.Text + "\t: " + fnameLabel2.Text + "\n";
            basic += mnameLabel.Text + "\t: " + mnameLabel2.Text + "\n";
            basic += dobLabel.Text + "\t: " + dobLabel2.Text + "\n";
            basic += classLabel.Text + "\t\t: " + classLabel2.Text + "\t\t\t";
            basic += yearLabel.Text + "\t: " + yearLabel2.Text + "\n";
            basic += rollLabel.Text + "\t\t: " + rollLabel2.Text + "\t\t\t";
            basic += examLabel.Text + "\t: " + examLabel2.Text + "\n";
            basic += gpaLabel.Text + "\t\t: " + gpaLabel2.Text;

            string heading = "\t\tSubject Name\t: ";
            if(gradeRadio.Checked)
                heading += "Grade";
            else if(marksRadio.Checked)
                heading += "Marks";
            
            string result = "\t\t" + banglaLabel.Text + "\t\t: " + banglaLabel2.Text + "\n";
            result += "\t\t" + englishLabel.Text + "\t\t: " + englishLabel2.Text + "\n";
            result += "\t\t" + mathLabel.Text + "\t: " + mathLabel2.Text + "\n";

            if (!(classLabel2.Text == "Class 0" || classLabel2.Text == "Class 1" || classLabel2.Text == "Class 2"))
            {
                result += "\t\t" + gsLabel.Text + "\t: " + gsLabel2.Text + "\n";
                result += "\t\t" + ssLabel.Text + "\t: " + ssLabel2.Text + "\n";
                result += "\t\t" + relLabel.Text + "\t: " + relLabel2.Text;
            }

            e.Graphics.DrawString(schoolName.Text, new Font("Monotype Corsiva", 24, FontStyle.Bold), Brushes.Black, 100, 100);
            e.Graphics.DrawString(basic, new Font("Monotype Corsiva", 16, FontStyle.Italic), Brushes.Black, 100, 200);
            e.Graphics.DrawString(heading, new Font("Monotype Corsiva", 16, FontStyle.Bold), Brushes.Black, 100, 420);
            e.Graphics.DrawString(result, new Font("Monotype Corsiva", 16, FontStyle.Italic), Brushes.Black, 100, 450);
            e.Graphics.DrawImage(pictureBox1.Image, 630, 200, 100, 100);
            e.Graphics.DrawLine(new Pen(Color.Black), 630, 995, 740, 995);
            e.Graphics.DrawString("Signature", new Font("Monotype Corsiva", 10, FontStyle.Italic), Brushes.Black, 660, 1000);
        }

        private void GradeRadio_CheckedChanged(object sender, EventArgs e)
        {
            ShowResult();
        }

        private void MarksRadio_CheckedChanged(object sender, EventArgs e)
        {
            ShowResult();
        }

        private double GetPoints(int marks)
        {
            if (marks >= 80)
            {
                return 5.0;
            }
            else if (marks >= 70)
            {
                return 4.5;
            }
            else if (marks >= 60)
            {
                return 4.0;
            }
            else if (marks >= 50)
            {
                return 3.0;
            }
            else if (marks >= 40)
            {
                return 2.0;
            }
            else if (marks >= 33)
            {
                return 1.0;
            }
            else
            {
                return 0.0;
            }
        }

        private string GetGrade(double points)
        {
            if (points >= 5.0)
            {
                return "A+";
            }
            else if (points >= 4.5)
            {
                return "A";
            }
            else if (points >= 4.0)
            {
                return "A-";
            }
            else if (points >= 3.0)
            {
                return "B";
            }
            else if (points >= 2.0)
            {
                return "C";
            }
            else if (points >= 1.0)
            {
                return "D";
            }
            else
            {
                return "F";
            }
        }
    }
}
