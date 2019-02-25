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

namespace PSMS
{
    public partial class Committee : Form
    {
        String teacher, selectQuery;
        int selectedId;
        Database db = new Database();
        MySqlConnection dbConnection;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;

        Thread t = new Thread(Formstart);

        public Committee(String teacher)
        {
            InitializeComponent();
            dbConnection = new MySqlConnection(db.DBinfo);
            this.teacher = teacher;

            if (teacher == "Head Teacher")
            {
                BtnInsert.Show();
            }
            else
            {
                BtnInsert.Hide();
            }

            ViewData();
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

        private String Check()
        {
            if(TxtName.Text == "")
            {
                MessageBox.Show("Please enter a name");
                TxtName.Focus();
                return "Error";
            }
            else if (TxtDesignation.Text == "")
            {
                MessageBox.Show("Please enter a designation");
                TxtDesignation.Focus();
                return "Error";
            }
            else if (TxtContact.Text == "")
            {
                MessageBox.Show("Please enter a contact no");
                TxtContact.Focus();
                return "Error";
            }
            else if (TxtAddress.Text == "")
            {
                MessageBox.Show("Please enter a address");
                TxtAddress.Focus();
                return "Error";
            }
            else
            {
                return "OK";
            }
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            if(Check() == "OK")
            {
                selectQuery = "INSERT INTO committee (name, designation, contact, address) VALUES ('" + TxtName.Text + "', '" + TxtDesignation.Text + "', '" + TxtContact.Text + "', '" + TxtAddress.Text + "')";
                command = new MySqlCommand(selectQuery, dbConnection);

                dbConnection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data Inserted");
                }
                else
                {
                    MessageBox.Show("Unable to insert");
                }
                dbConnection.Close();
                ViewData();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (Check() == "OK")
            {
                selectQuery = "UPDATE committee SET name = '" + TxtName.Text + "', designation = '" + TxtDesignation.Text + "', contact = '" + TxtContact.Text + "', address = '" + TxtAddress.Text + "' WHERE id = " + selectedId;
                command = new MySqlCommand(selectQuery, dbConnection);

                dbConnection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data Updated");
                }
                else
                {
                    MessageBox.Show("Unable to update");
                }
                dbConnection.Close();
                ViewData();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            selectQuery = "DELETE FROM committee WHERE id = " + selectedId;
            command = new MySqlCommand(selectQuery, dbConnection);

            dbConnection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Data Deleted");
            }
            else
            {
                MessageBox.Show("Unable to delete");
            }
            dbConnection.Close();
            ViewData();
        }

        private void ViewData()
        {
            selectQuery = "SELECT * FROM committee ORDER BY id ASC";
            command = new MySqlCommand(selectQuery, dbConnection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            TxtName.Text = "";
            TxtDesignation.Text = "";
            TxtContact.Text = "";
            TxtAddress.Text = "";
        }

        private void DataGridView1_Click(object sender, EventArgs e)
        {
            selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            TxtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtDesignation.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            TxtContact.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TxtAddress.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            if (teacher == "Head Teacher")
            {
                BtnUpdate.Show();
                BtnDelete.Show();
            }
            else
            {
                BtnUpdate.Hide();
                BtnDelete.Hide();
            }
        }
    }
}
