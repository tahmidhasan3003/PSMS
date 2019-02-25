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
    public partial class Result_Sheet : Form
    {
        string teacher, tbname, id, classes, year;

        string[] arrayID = new string[500];
        int[] arrayMarks = new int[500];
        int ai = 0;
        string[] arrayID2 = new string[500];
        int[] arrayMarks2 = new int[500];
        int ai2 = 0;

        Thread t = new Thread(Formstart);

        Database db = new Database();
        MySqlConnection dbConnection;
        String selectQuery;

        public Result_Sheet(string teacher)
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

        private void ViewButton_Click(object sender, EventArgs e)
        {
            View_Result vrFobj = new View_Result(teacher, tbname, id, classes, year);
            t.Start();
            while (t.IsAlive)
            {
                this.Hide();
                vrFobj.ShowDialog();
            }
            t.Abort();
            this.Close();
        }

        private void Calculate()
        {
            String newClass = "Error", newClassFormated = "Error", oldClassFormated = "Error", c1 = "", c2 = "";
            int newYear = 0;
            if(comboBox1.InvokeRequired)
            {
                comboBox1.Invoke(new MethodInvoker(delegate { c1 = comboBox1.Text; }));
            }
            if (comboBox2.InvokeRequired)
            {
                comboBox2.Invoke(new MethodInvoker(delegate { c2 = comboBox2.Text; }));
            }
            String selectQuery = "SELECT * FROM final WHERE class = '" + c1 + "' and year = " + c2 + " ORDER BY roll ASC";
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                int count = 0, sum = 0, x;
                string s;
                if(c1 == "Class 0" || c1 == "Class 1" || c1 == "Class 2")
                {
                    for (int j = 4; j < 7; j++)
                    {
                        s = table.Rows[i][j].ToString();
                        x = Convert.ToInt32(s);
                        if (x < 33)
                        {
                            count++;
                        }
                        sum += x;
                    }
                }
                else
                {
                    for (int j = 4; j < 10; j++)
                    {
                        s = table.Rows[i][j].ToString();
                        x = Convert.ToInt32(s);
                        if (x < 33)
                        {
                            count++;
                        }
                        sum += x;
                    }
                }
                if (count <= 0)
                {
                    arrayID[ai] = table.Rows[i][1].ToString();
                    arrayMarks[ai] = sum;
                    ai++;
                }
                else
                {
                    arrayID2[ai2] = table.Rows[i][1].ToString();
                    arrayMarks2[ai2] = sum;
                    ai2++;
                }
            }

            SortData1();
            SortData2();

            newYear = Convert.ToInt32(c2) + 1;
            if (c1 == "Class 0")
            {
                newClass = "Class 1";
                newClassFormated = "class1";
                oldClassFormated = "class0";
            }
            else if (c1 == "Class 1")
            {
                newClass = "Class 2";
                newClassFormated = "class2";
                oldClassFormated = "class1";
            }
            else if (c1 == "Class 2")
            {
                newClass = "Class 3";
                newClassFormated = "class3";
                oldClassFormated = "class2";
            }
            else if (c1 == "Class 3")
            {
                newClass = "Class 4";
                newClassFormated = "class4";
                oldClassFormated = "class3";
            }
            else if (c1 == "Class 4")
            {
                newClass = "Class 5";
                newClassFormated = "class5";
                oldClassFormated = "class4";
            }
            else if (c1 == "Class 5")
            {
                newClass = "Graduated";
                newClassFormated = "graduated";
                oldClassFormated = "class5";
                newYear--;
            }

            for (int i = 0; i < ai; i++)
            {
                MySqlCommand command2 = new MySqlCommand("UPDATE students SET class = @class, year = @year, roll = @roll WHERE id = @id", dbConnection);
                command2.Parameters.Add("@roll", MySqlDbType.Int32).Value = i + 1;
                command2.Parameters.Add("@id", MySqlDbType.VarChar).Value = arrayID[i];
                command2.Parameters.Add("@class", MySqlDbType.VarChar).Value = newClass;
                command2.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;

                String selectQuery3 = "UPDATE classes SET " + newClassFormated + " = " + newYear + " WHERE id = '" + arrayID[i] + "'";
                MySqlCommand command3 = new MySqlCommand(selectQuery3, dbConnection);

                MySqlCommand command4 = new MySqlCommand("INSERT INTO firstterm(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
                command4.Parameters.Add("@roll", MySqlDbType.Int32).Value = i + 1;
                command4.Parameters.Add("@id", MySqlDbType.VarChar).Value = arrayID[i];
                command4.Parameters.Add("@class", MySqlDbType.VarChar).Value = newClass;
                command4.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;

                MySqlCommand command5 = new MySqlCommand("INSERT INTO secondterm(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
                command5.Parameters.Add("@roll", MySqlDbType.Int32).Value = i + 1;
                command5.Parameters.Add("@id", MySqlDbType.VarChar).Value = arrayID[i];
                command5.Parameters.Add("@class", MySqlDbType.VarChar).Value = newClass;
                command5.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;

                MySqlCommand command6 = new MySqlCommand("INSERT INTO final(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
                command6.Parameters.Add("@roll", MySqlDbType.Int32).Value = i + 1;
                command6.Parameters.Add("@id", MySqlDbType.VarChar).Value = arrayID[i];
                command6.Parameters.Add("@class", MySqlDbType.VarChar).Value = newClass;
                command6.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;

                MySqlCommand command7 = new MySqlCommand("SELECT * FROM years WHERE year = @year", dbConnection);
                command7.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;
                dbConnection.Open();
                MySqlDataReader mdr = command7.ExecuteReader();
                if (mdr.Read())
                {
                    dbConnection.Close();
                    dbConnection.Open();
                    try
                    {
                        command2.ExecuteNonQuery();
                        command3.ExecuteNonQuery();
                        command4.ExecuteNonQuery();
                        command5.ExecuteNonQuery();
                        command6.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
                else
                {
                    MySqlCommand command8 = new MySqlCommand("INSERT INTO years(year) VALUES (@year)", dbConnection);
                    command8.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;
                    dbConnection.Close();
                    dbConnection.Open();
                    try
                    {
                        command2.ExecuteNonQuery();
                        command3.ExecuteNonQuery();
                        command4.ExecuteNonQuery();
                        command5.ExecuteNonQuery();
                        command6.ExecuteNonQuery();
                        command8.ExecuteNonQuery();
                        comboBox2.Items.Add(newYear.ToString());
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }

            if (c1 == "Class 5")
            {
                newYear++;
            }

            for (int i = 0; i < ai2; i++)
            {
                MySqlCommand command2 = new MySqlCommand("UPDATE students SET year = @year, roll = @roll WHERE id = @id", dbConnection);
                command2.Parameters.Add("@roll", MySqlDbType.Int32).Value = 500 + i + 1;
                command2.Parameters.Add("@id", MySqlDbType.VarChar).Value = arrayID2[i];
                command2.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;

                String selectQuery3 = "UPDATE classes SET " + oldClassFormated + " = " + newYear + " WHERE id = '" + arrayID2[i] + "'";
                MySqlCommand command3 = new MySqlCommand(selectQuery3, dbConnection);

                MySqlCommand command4 = new MySqlCommand("INSERT INTO firstterm(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
                command4.Parameters.Add("@roll", MySqlDbType.Int32).Value = 500 + i + 1;
                command4.Parameters.Add("@id", MySqlDbType.VarChar).Value = arrayID2[i];
                command4.Parameters.Add("@class", MySqlDbType.VarChar).Value = c1;
                command4.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;

                MySqlCommand command5 = new MySqlCommand("INSERT INTO secondterm(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
                command5.Parameters.Add("@roll", MySqlDbType.Int32).Value = 500 + i + 1;
                command5.Parameters.Add("@id", MySqlDbType.VarChar).Value = arrayID2[i];
                command5.Parameters.Add("@class", MySqlDbType.VarChar).Value = c1;
                command5.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;

                MySqlCommand command6 = new MySqlCommand("INSERT INTO final(roll, id, class, year) VALUES (@roll, @id, @class, @year)", dbConnection);
                command6.Parameters.Add("@roll", MySqlDbType.Int32).Value = 500 + i + 1;
                command6.Parameters.Add("@id", MySqlDbType.VarChar).Value = arrayID2[i];
                command6.Parameters.Add("@class", MySqlDbType.VarChar).Value = c1;
                command6.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;

                MySqlCommand command7 = new MySqlCommand("SELECT * FROM years WHERE year = @year", dbConnection);
                command7.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;
                dbConnection.Open();
                MySqlDataReader mdr = command7.ExecuteReader();
                if (mdr.Read())
                {
                    dbConnection.Close();
                    dbConnection.Open();
                    try
                    {
                        command2.ExecuteNonQuery();
                        command3.ExecuteNonQuery();
                        command4.ExecuteNonQuery();
                        command5.ExecuteNonQuery();
                        command6.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
                else
                {
                    MySqlCommand command8 = new MySqlCommand("INSERT INTO years(year) VALUES (@year)", dbConnection);
                    command8.Parameters.Add("@year", MySqlDbType.Int32).Value = newYear;
                    dbConnection.Close();
                    dbConnection.Open();
                    try
                    {
                        command2.ExecuteNonQuery();
                        command3.ExecuteNonQuery();
                        command4.ExecuteNonQuery();
                        command5.ExecuteNonQuery();
                        command6.ExecuteNonQuery();
                        command8.ExecuteNonQuery();
                        comboBox2.Items.Add(newYear.ToString());
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }
        }

        private void Promote_Click(object sender, EventArgs e)
        {
            using (WaitForm wfFobj = new WaitForm(Calculate))
            {
                wfFobj.ShowDialog(this);
            }
            MessageBox.Show("Result processing is completed...");
            ViewData();
        }

        private void SortData1()
        {
            for(int i = 1; i < ai; i++)
            {
                int j = i;
                while(j > 0)
                {
                    if(arrayMarks[j] > arrayMarks[j - 1])
                    {
                        string temps;
                        int tempn;

                        temps = arrayID[j];
                        arrayID[j] = arrayID[j - 1];
                        arrayID[j - 1] = temps;

                        tempn = arrayMarks[j];
                        arrayMarks[j] = arrayMarks[j - 1];
                        arrayMarks[j - 1] = tempn;
                    }                    
                    j--;
                }
            }
        }

        private void SortData2()
        {
            for (int i = 1; i < ai2; i++)
            {
                int j = i;
                while (j > 0)
                {
                    if(arrayMarks2[j] > arrayMarks2[j - 1])
                    {
                        string temps;
                        int tempn;

                        temps = arrayID2[j];
                        arrayID2[j] = arrayID2[j - 1];
                        arrayID2[j - 1] = temps;

                        tempn = arrayMarks2[j];
                        arrayMarks2[j] = arrayMarks2[j - 1];
                        arrayMarks2[j - 1] = tempn;
                    }                   
                    j--;
                }
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(textBox2.Text) < 0 || Convert.ToInt32(textBox2.Text) > 100)
            {
                MessageBox.Show("Invalid Input for Bangla");
                textBox2.Focus();
            }
            else if (Convert.ToInt32(textBox3.Text) < 0 || Convert.ToInt32(textBox3.Text) > 100)
            {
                MessageBox.Show("Invalid input for English");
                textBox3.Focus();
            }
            else if (Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 100)
            {
                MessageBox.Show("Invalid input for Math");
                textBox4.Focus();
            }
            else if (Convert.ToInt32(textBox6.Text) < 0 || Convert.ToInt32(textBox6.Text) > 100)
            {
                MessageBox.Show("Invalid input for General Science");
                textBox6.Focus();
            }
            else if (Convert.ToInt32(textBox5.Text) < 0 || Convert.ToInt32(textBox5.Text) > 100)
            {
                MessageBox.Show("Invalid input for Social Science");
                textBox5.Focus();
            }
            else if (Convert.ToInt32(textBox7.Text) < 0 || Convert.ToInt32(textBox7.Text) > 100)
            {
                MessageBox.Show("Invalid input for Religious Education");
                textBox7.Focus();
            }
            else
            {
                String selectQuery;

                if (radioButton1.Checked)
                {
                    selectQuery = "UPDATE firstterm SET ban = @ban, eng = @eng, mat = @mat, gs = @gs, ss = @ss, rel = @rel" +
                                  " WHERE id = '" + id + "' and class = '" + comboBox1.Text + "' and year = " + comboBox2.Text;
                }
                else if (radioButton2.Checked)
                {
                    selectQuery = "UPDATE secondterm SET ban = @ban, eng = @eng, mat = @mat, gs = @gs, ss = @ss, rel = @rel" +
                                  " WHERE id = '" + id + "' and class = '" + comboBox1.Text + "' and year = " + comboBox2.Text;
                }
                else
                {
                    selectQuery = "UPDATE final SET ban = @ban, eng = @eng, mat = @mat, gs = @gs, ss = @ss, rel = @rel" +
                                  " WHERE id = '" + id + "' and class = '" + comboBox1.Text + "' and year = " + comboBox2.Text;
                }
                MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);

                command.Parameters.Add("@ban", MySqlDbType.Int32).Value = Convert.ToInt32(textBox2.Text);
                command.Parameters.Add("@eng", MySqlDbType.Int32).Value = Convert.ToInt32(textBox3.Text);
                command.Parameters.Add("@mat", MySqlDbType.Int32).Value = Convert.ToInt32(textBox4.Text);
                command.Parameters.Add("@ss", MySqlDbType.Int32).Value = Convert.ToInt32(textBox5.Text);
                command.Parameters.Add("@gs", MySqlDbType.Int32).Value = Convert.ToInt32(textBox6.Text);
                command.Parameters.Add("@rel", MySqlDbType.Int32).Value = Convert.ToInt32(textBox7.Text);

                dbConnection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Result updated successfully");
                }
                catch
                {
                    MessageBox.Show("Unable to update");
                }
                finally
                {
                    dbConnection.Close();
                }
                ViewData();
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a class first");
                comboBox1.Focus();
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox2.Focus();
            }
            else
            {
                promote.Hide();
                update.Hide();
                viewButton.Hide();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";

                tbname = "firstterm";
                selectQuery = "SELECT * FROM firstterm WHERE class = '" + comboBox1.Text + "' and year = " + comboBox2.Text + " ORDER BY roll ASC";
                ViewData();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a class first");
                comboBox1.Focus();
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox2.Focus();
            }
            else
            {
                update.Hide();
                viewButton.Hide();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";

                if(radioButton1.Checked)
                {
                    promote.Hide();
                    tbname = "firstterm";
                    selectQuery = "SELECT * FROM firstterm WHERE class = '" + comboBox1.Text + "' and year = " + comboBox2.Text + " ORDER BY roll ASC";
                    ViewData();
                }
                else if(radioButton2.Checked)
                {
                    promote.Hide();
                    tbname = "secondterm";
                    selectQuery = "SELECT * FROM secondterm WHERE class = '" + comboBox1.Text + "' and year = " + comboBox2.Text + " ORDER BY roll ASC";
                    ViewData();
                }
                else if(radioButton3.Checked)
                {
                    promote.Show();
                    tbname = "final";
                    selectQuery = "SELECT * FROM final WHERE class = '" + comboBox1.Text + "' and year = " + comboBox2.Text + " ORDER BY roll ASC";
                    ViewData();
                }
                else
                    MessageBox.Show("Please select a term");
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a class first");
                comboBox1.Focus();
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox2.Focus();
            }
            else
            {
                promote.Hide();
                update.Hide();
                viewButton.Hide();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";

                tbname = "secondterm";
                selectQuery = "SELECT * FROM secondterm WHERE class = '" + comboBox1.Text + "' and year = " + comboBox2.Text + " ORDER BY roll ASC";
                ViewData();
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a class first");
                comboBox1.Focus();
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Please select a year");
                comboBox2.Focus();
            }
            else
            {
                promote.Show();
                update.Hide();
                viewButton.Hide();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";

                tbname = "final";
                selectQuery = "SELECT * FROM final WHERE class = '" + comboBox1.Text + "' and year = " + comboBox2.Text + " ORDER BY roll ASC";
                ViewData();
            }
        }

        private void ViewData()
        {
            MySqlCommand command = new MySqlCommand(selectQuery, dbConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void DataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Class 0" || dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Class 1" || dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Class 2")
            {
                groupBox3.Hide();
            }
            else
            {
                groupBox3.Show();
            }

            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            classes = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            year = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
            update.Show();
            viewButton.Show();
        }
    }
}
